using System;
using System.Collections.Generic;
using System.Globalization;

namespace SERVICES.Integrity
{
    /// <summary>
    /// Algoritmo de cálculo de dígitos verificadores. No conoce tablas ni base de datos:
    /// recibe valores y devuelve números, por eso sirve para cualquier entidad.
    ///
    /// DVH (horizontal, uno por registro):
    ///     DVH = ( Σ_i  P(i) · Σ_j  j · c(i,j) )  mod M
    ///       i      = posición del atributo dentro de la entidad (1..n)
    ///       P(i)   = i-ésimo número primo (2, 3, 5, 7, 11, ...) → peso del atributo
    ///       j      = posición del carácter dentro del valor (1..largo)
    ///       c(i,j) = código Unicode del carácter j del atributo i
    ///       M      = 1.000.000.007 (primo grande, evita desbordes)
    ///   Además se suma P(i) · (largo + 1) · 7 (marca de longitud), para que pasar caracteres
    ///   de un campo al siguiente ("ciro"+"abc" → "ciroa"+"bc") también cambie el DVH.
    ///   NULL y "" se consideran iguales (ambos se normalizan a texto vacío).
    ///   Como participan la posición del carácter Y la del atributo, intercambiar dos
    ///   caracteres o dos campos cambia el resultado. Como la PK es un atributo más,
    ///   mover datos de un registro a otro también lo cambia.
    ///
    /// DVV (vertical, uno por tabla):
    ///     DVV = ( Σ_k DVH_k ) mod M
    ///   Si se agrega o se quita un registro por fuera del sistema, la suma deja de coincidir.
    /// </summary>
    public class DigitoVerificador_Service
    {
        public const long MODULO = 1000000007L;

        private static readonly int[] PRIMOS = GenerarPrimos(64);

        #region DVH

        /// <summary>Calcula el DVH de un registro a partir de sus valores en el orden de los atributos.</summary>
        public long CalcularDVH(IList<object> valoresOrdenados)
        {
            if (valoresOrdenados == null)
                throw new ArgumentNullException(nameof(valoresOrdenados));

            long dvh = 0;

            for (int i = 0; i < valoresOrdenados.Count; i++)
            {
                long pesoAtributo = Peso(i + 1);
                string texto = Normalizar(valoresOrdenados[i]);

                long sumaAtributo = 0;
                for (int j = 0; j < texto.Length; j++)
                {
                    long posicionCaracter = j + 1;
                    sumaAtributo = (sumaAtributo + posicionCaracter * texto[j]) % MODULO;
                }

                // marca de longitud: evita que un corrimiento de caracteres entre campos se compense
                long marcaLongitud = (texto.Length + 1) * 7L;

                dvh = (dvh + pesoAtributo * ((sumaAtributo + marcaLongitud) % MODULO)) % MODULO;
            }

            return dvh;
        }

        #endregion

        #region DVV

        public long CalcularDVV(IEnumerable<long> dvhs)
        {
            long dvv = 0;

            foreach (long dvh in dvhs)
            {
                dvv = Sumar(dvv, dvh);
            }

            return dvv;
        }

        /// <summary>DVV nuevo sin recorrer toda la tabla: DVV - (DVH viejos) + (DVH nuevos).</summary>
        public long AjustarDVV(long dvvActual, long sumaDVHAnteriores, long sumaDVHNuevos)
        {
            return Sumar(Restar(dvvActual, sumaDVHAnteriores), sumaDVHNuevos);
        }

        #endregion

        #region AUXILIARES

        public long Sumar(long a, long b)
        {
            return ((a % MODULO) + (b % MODULO)) % MODULO;
        }

        public long Restar(long a, long b)
        {
            long r = ((a % MODULO) - (b % MODULO)) % MODULO;
            return r < 0 ? r + MODULO : r;
        }

        /// <summary>Peso del atributo i (1-based): el i-ésimo primo.</summary>
        private static long Peso(int posicionAtributo)
        {
            if (posicionAtributo <= PRIMOS.Length)
                return PRIMOS[posicionAtributo - 1];

            return 2L * posicionAtributo + 1; // por si una entidad tuviera más de 64 atributos
        }

        /// <summary>
        /// Convierte cualquier valor a un texto estable (independiente de la configuración
        /// regional de la PC), para que el DVH dé lo mismo en cualquier equipo.
        /// </summary>
        public static string Normalizar(object valor)
        {
            if (valor == null || valor is DBNull)
                return string.Empty;

            if (valor is DateTime fecha)
                return fecha.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            if (valor is bool b)
                return b ? "1" : "0";

            if (valor is IFormattable formateable)
                return formateable.ToString(null, CultureInfo.InvariantCulture);

            return valor.ToString();
        }

        private static int[] GenerarPrimos(int cantidad)
        {
            var primos = new List<int>();
            int candidato = 2;

            while (primos.Count < cantidad)
            {
                bool esPrimo = true;
                foreach (int p in primos)
                {
                    if (p * p > candidato) break;
                    if (candidato % p == 0) { esPrimo = false; break; }
                }
                if (esPrimo) primos.Add(candidato);
                candidato++;
            }

            return primos.ToArray();
        }

        #endregion
    }
}
