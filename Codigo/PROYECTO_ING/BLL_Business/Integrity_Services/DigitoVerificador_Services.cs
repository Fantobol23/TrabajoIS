using BE_Entities.Integrity;
using DAL_DataAccess.DataEntities;
using SERVICES.Integrity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BLL_Business.Integrity_Services
{
    /// <summary>
    /// Lógica de dígitos verificadores, genérica para cualquier TablaVerificable:
    ///  - Verificar la integridad de todas las tablas (al iniciar la aplicación).
    ///  - Mantener DVH/DVV al día cuando el SISTEMA modifica datos (Ejecutar).
    ///  - Restaurar (recalcular) los dígitos cuando el administrador lo decide.
    /// </summary>
    public class DigitoVerificador_Services
    {
        private readonly DigitoVerificador_Data _dvData;
        private readonly DigitoVerificador_Service _algoritmo;

        public DigitoVerificador_Services()
        {
            _dvData = new DigitoVerificador_Data();
            _algoritmo = new DigitoVerificador_Service();
        }

        #region VERIFICACIÓN

        /// <summary>Verifica todas las tablas registradas y devuelve la lista de inconsistencias.</summary>
        public ResultadoIntegridad VerificarIntegridad()
        {
            var resultado = new ResultadoIntegridad();

            foreach (var tabla in TablasVerificables.Todas)
            {
                VerificarTabla(tabla, resultado);
            }

            return resultado;
        }

        public ResultadoIntegridad VerificarTabla(string nombreTabla)
        {
            var resultado = new ResultadoIntegridad();
            VerificarTabla(TablasVerificables.Obtener(nombreTabla), resultado);
            return resultado;
        }

        private void VerificarTabla(TablaVerificable tabla, ResultadoIntegridad resultado)
        {
            DataTable registros = _dvData.ObtenerRegistros(tabla);
            var dvhsGuardados = new List<long>();

            // 1) VERIFICACIÓN HORIZONTAL: registro por registro
            foreach (DataRow fila in registros.Rows)
            {
                long dvhCalculado = CalcularDVH(tabla, fila);
                object valorGuardado = fila[tabla.ColumnaDVH];

                if (valorGuardado == DBNull.Value)
                {
                    resultado.Errores.Add(new ErrorIntegridad(tabla.Nombre, TipoErrorIntegridad.DVH_Invalido,
                        DescribirClave(tabla, fila), "El registro no tiene DVH (alta por fuera del sistema o DV nunca generado)."));
                    continue;
                }

                long dvhGuardado = Convert.ToInt64(valorGuardado);
                dvhsGuardados.Add(dvhGuardado);

                if (dvhGuardado != dvhCalculado)
                {
                    resultado.Errores.Add(new ErrorIntegridad(tabla.Nombre, TipoErrorIntegridad.DVH_Invalido,
                        DescribirClave(tabla, fila), "El contenido del registro fue modificado por fuera del sistema."));
                }
            }

            // 2) VERIFICACIÓN VERTICAL: la tabla completa contra el DVV guardado
            long? dvvGuardado = _dvData.ObtenerDVV(tabla.Nombre);

            if (dvvGuardado == null)
            {
                resultado.Errores.Add(new ErrorIntegridad(tabla.Nombre, TipoErrorIntegridad.DVV_Inexistente,
                    string.Empty, "No existe el dígito verificador vertical de la tabla."));
                return;
            }

            long dvvCalculado = _algoritmo.CalcularDVV(dvhsGuardados);

            if (dvvCalculado != dvvGuardado.Value)
            {
                resultado.Errores.Add(new ErrorIntegridad(tabla.Nombre, TipoErrorIntegridad.DVV_Invalido,
                    string.Empty, "Se agregaron o eliminaron registros por fuera del sistema (" + registros.Rows.Count + " registros actuales)."));
            }
        }

        #endregion

        #region MANTENIMIENTO EN OPERACIONES DEL SISTEMA

        /// <summary>
        /// Ejecuta una operación de escritura (alta, modificación o baja) y deja los
        /// dígitos verificadores consistentes:
        ///   - antes: guarda la suma de los DVH de los registros alcanzados;
        ///   - ejecuta la operación;
        ///   - después: recalcula el DVH de los registros que quedaron y
        ///     ajusta el DVV = DVV - Σ DVH anteriores + Σ DVH nuevos.
        /// El DVV se ajusta (no se recalcula de cero) para no "tapar" una alteración externa
        /// previa que todavía no fue tratada por el administrador.
        /// </summary>
        public T Ejecutar<T>(Func<T> operacion, params AlcanceDV[] alcances)
        {
            if (operacion == null) throw new ArgumentNullException(nameof(operacion));

            var sumasAnteriores = new Dictionary<AlcanceDV, long>();
            foreach (var alcance in alcances)
            {
                sumasAnteriores[alcance] = SumarDVHGuardados(TablasVerificables.Obtener(alcance.Tabla), alcance.Filtro);
            }

            T resultado = operacion();

            foreach (var alcance in alcances)
            {
                var tabla = TablasVerificables.Obtener(alcance.Tabla);
                long sumaNuevos = RecalcularDVHRegistros(tabla, alcance.Filtro);

                long? dvvActual = _dvData.ObtenerDVV(tabla.Nombre);

                long dvvNuevo = dvvActual.HasValue
                    ? _algoritmo.AjustarDVV(dvvActual.Value, sumasAnteriores[alcance], sumaNuevos)
                    : CalcularDVVDesdeGuardados(tabla); // primera vez: no había DVV

                _dvData.GuardarDVV(tabla.Nombre, dvvNuevo);
            }

            return resultado;
        }

        public void Ejecutar(Action operacion, params AlcanceDV[] alcances)
        {
            Ejecutar<int>(() => { operacion(); return 0; }, alcances);
        }

        #endregion

        #region RESTAURACIÓN

        /// <summary>
        /// Recalcula DVH de TODOS los registros y el DVV de la tabla. Equivale a
        /// "aceptar como válido el contenido actual". Sólo debe hacerlo el administrador
        /// después de revisar (o corregir/restaurar) los datos alterados.
        /// </summary>
        public void RecalcularTabla(string nombreTabla)
        {
            var tabla = TablasVerificables.Obtener(nombreTabla);
            long suma = RecalcularDVHRegistros(tabla, null);
            _dvData.GuardarDVV(tabla.Nombre, suma);
        }

        public void RecalcularTodo()
        {
            foreach (var tabla in TablasVerificables.Todas)
            {
                RecalcularTabla(tabla.Nombre);
            }
        }

        #endregion

        #region AUXILIARES

        /// <summary>Arma la lista de valores en el orden de los atributos y aplica el algoritmo.</summary>
        private long CalcularDVH(TablaVerificable tabla, DataRow fila)
        {
            var valores = tabla.ColumnasDatos.Select(c => fila[c]).ToList();
            return _algoritmo.CalcularDVH(valores);
        }

        /// <summary>Recalcula y guarda el DVH de los registros que cumplen el filtro. Devuelve la suma de los nuevos DVH.</summary>
        private long RecalcularDVHRegistros(TablaVerificable tabla, IDictionary<string, object> filtro)
        {
            DataTable registros = _dvData.ObtenerRegistros(tabla, filtro);
            long suma = 0;

            foreach (DataRow fila in registros.Rows)
            {
                long dvh = CalcularDVH(tabla, fila);
                var clave = tabla.ColumnasClave.ToDictionary(c => c, c => fila[c]);

                _dvData.GuardarDVH(tabla, clave, dvh);
                suma = _algoritmo.Sumar(suma, dvh);
            }

            return suma;
        }

        private long SumarDVHGuardados(TablaVerificable tabla, IDictionary<string, object> filtro)
        {
            DataTable registros = _dvData.ObtenerRegistros(tabla, filtro);

            return _algoritmo.CalcularDVV(registros.Rows.Cast<DataRow>()
                .Where(f => f[tabla.ColumnaDVH] != DBNull.Value)
                .Select(f => Convert.ToInt64(f[tabla.ColumnaDVH])));
        }

        private long CalcularDVVDesdeGuardados(TablaVerificable tabla)
        {
            return SumarDVHGuardados(tabla, null);
        }

        private static string DescribirClave(TablaVerificable tabla, DataRow fila)
        {
            return string.Join(", ", tabla.ColumnasClave.Select(c => c + "=" + fila[c]));
        }

        #endregion
    }
}
