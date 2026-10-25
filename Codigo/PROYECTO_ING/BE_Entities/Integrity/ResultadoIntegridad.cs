using System.Collections.Generic;
using System.Linq;

namespace BE_Entities.Integrity
{
    public class ResultadoIntegridad
    {
        public ResultadoIntegridad()
        {
            errores = new List<ErrorIntegridad>();
        }

        private List<ErrorIntegridad> errores;

        public List<ErrorIntegridad> Errores
        {
            get { return errores; }
        }

        public bool EsValida
        {
            get { return errores.Count == 0; }
        }

        /// <summary>Tablas que tienen al menos un error.</summary>
        public List<string> TablasConError
        {
            get { return errores.Select(e => e.Tabla).Distinct().ToList(); }
        }
    }
}
