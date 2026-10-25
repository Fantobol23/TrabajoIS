using BE_Entities.Languages;
using DAL_DataAccess.DataEntities;
using SERVICES.Languages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL_Business.Language_Services
{
    /// <summary>
    /// Casos de uso de multi-idioma: ABM de idiomas, carga de traducciones y cambio
    /// dinámico de idioma. Lee de la BD (DAL) y le entrega el diccionario al
    /// GestorIdioma (Subject), que se encarga de notificar a los observadores.
    /// </summary>
    public class Idioma_Services
    {
        private readonly Idioma_Data _idiomaData;

        // claves ya registradas en la BD durante esta ejecución (evita ir a la BD en cada formulario)
        private static readonly HashSet<string> _clavesConocidas = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private static bool _clavesCargadas;

        public Idioma_Services()
        {
            _idiomaData = new Idioma_Data();
        }

        #region IDIOMA ACTIVO

        /// <summary>Al iniciar la aplicación: activa el idioma por defecto (si la BD lo tiene).</summary>
        public void InicializarIdioma()
        {
            var idioma = ListarIdiomas().FirstOrDefault(i => i.EsPorDefecto) ?? new Idioma(0, "Español", "es-AR", true);
            CambiarIdioma(idioma);
        }

        /// <summary>Carga las traducciones del idioma y notifica a todos los formularios abiertos.</summary>
        public void CambiarIdioma(Idioma idioma)
        {
            if (idioma == null) throw new ArgumentNullException(nameof(idioma));

            var textos = idioma.Id > 0
                ? _idiomaData.ObtenerTraducciones(idioma.Id)
                : new Dictionary<string, string>();

            GestorIdioma.Instancia.CambiarIdioma(idioma, textos);
        }

        public Idioma IdiomaActual
        {
            get { return GestorIdioma.Instancia.IdiomaActual; }
        }

        #endregion

        #region ABM DE IDIOMAS

        public List<Idioma> ListarIdiomas()
        {
            return _idiomaData.ListarTodo();
        }

        public Idioma CrearIdioma(string nombre, string codigo)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El idioma debe tener un nombre.");
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("El idioma debe tener un código (por ejemplo en-US).");

            if (ListarIdiomas().Any(i => string.Equals(i.Nombre, nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Ya existe un idioma con ese nombre.");

            var idioma = new Idioma(0, nombre.Trim(), codigo.Trim());

            if (_idiomaData.Insertar(idioma) < 0 || idioma.Id <= 0)
                throw new Exception("No se pudo guardar el idioma.");

            return idioma;
        }

        public void EditarIdioma(Idioma idioma)
        {
            if (_idiomaData.Editar(idioma) < 0)
                throw new Exception("No se pudo modificar el idioma.");
        }

        public void BorrarIdioma(Idioma idioma)
        {
            if (idioma.EsPorDefecto)
                throw new InvalidOperationException("No se puede borrar el idioma por defecto.");

            if (IdiomaActual != null && IdiomaActual.Id == idioma.Id)
                throw new InvalidOperationException("No se puede borrar el idioma que está en uso.");

            if (_idiomaData.Borrar(idioma) < 0)
                throw new Exception("No se pudo borrar el idioma.");
        }

        #endregion

        #region LEYENDAS Y TRADUCCIONES

        /// <summary>
        /// Registra en la BD las leyendas que todavía no existen. Lo llama cada formulario
        /// al abrirse, así el sistema "descubre" solo las leyendas a traducir y quedan
        /// disponibles para cualquier idioma nuevo.
        /// </summary>
        public void RegistrarEtiquetas(IDictionary<string, string> claveTextoOriginal)
        {
            if (claveTextoOriginal == null) return;

            if (!_clavesCargadas)
            {
                foreach (var e in _idiomaData.ListarEtiquetas())
                    _clavesConocidas.Add(e.Clave);
                _clavesCargadas = true;
            }

            foreach (var par in claveTextoOriginal)
            {
                if (string.IsNullOrWhiteSpace(par.Key) || _clavesConocidas.Contains(par.Key))
                    continue;

                // aunque falle (p. ej. BD sin la tabla) no se reintenta en cada formulario
                _idiomaData.RegistrarEtiqueta(par.Key, par.Value);
                _clavesConocidas.Add(par.Key);
            }
        }

        public List<Etiqueta> ListarEtiquetas()
        {
            return _idiomaData.ListarEtiquetas();
        }

        /// <summary>Traducciones de un idioma (clave → texto).</summary>
        public Dictionary<string, string> ObtenerTraducciones(Idioma idioma)
        {
            return _idiomaData.ObtenerTraducciones(idioma.Id);
        }

        /// <summary>Guarda las traducciones editadas y, si es el idioma activo, refresca la UI al instante.</summary>
        public void GuardarTraducciones(Idioma idioma, IDictionary<string, string> traducciones)
        {
            if (idioma == null || idioma.Id <= 0)
                throw new ArgumentException("Debe seleccionar un idioma guardado.");

            foreach (var par in traducciones)
            {
                if (_idiomaData.GuardarTraduccion(idioma.Id, par.Key, par.Value) < 0)
                    throw new Exception("No se pudo guardar la traducción de '" + par.Key + "'.");
            }

            if (IdiomaActual != null && IdiomaActual.Id == idioma.Id)
                CambiarIdioma(idioma);
        }

        #endregion
    }
}
