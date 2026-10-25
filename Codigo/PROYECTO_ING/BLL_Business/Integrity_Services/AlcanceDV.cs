using System.Collections.Generic;

namespace BLL_Business.Integrity_Services
{
    /// <summary>
    /// Indica qué registros de qué tabla toca una operación (tabla + filtro columna=valor).
    /// Con esto el servicio sabe qué DVH recalcular y cómo ajustar el DVV.
    /// </summary>
    public class AlcanceDV
    {
        public AlcanceDV(string tabla, IDictionary<string, object> filtro)
        {
            Tabla = tabla;
            Filtro = filtro;
        }

        public AlcanceDV(string tabla, string columna, object valor)
            : this(tabla, new Dictionary<string, object> { { columna, valor } })
        {
        }

        public string Tabla { get; private set; }

        public IDictionary<string, object> Filtro { get; private set; }
    }
}
