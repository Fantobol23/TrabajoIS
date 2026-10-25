using System;
using System.Collections.Generic;
using System.Linq;

namespace BE_Entities.Integrity
{
    /// <summary>
    /// Describe una tabla protegida con dígitos verificadores.
    /// Es la "ficha" que usa el mecanismo genérico: cualquier entidad nueva se protege
    /// registrando una instancia de esta clase (no hace falta escribir código nuevo).
    /// </summary>
    public class TablaVerificable
    {
        #region CONSTRUCTOR

        public TablaVerificable(string nombreTabla, string[] columnasClave, string[] columnasDatos, string columnaDVH = "dvh")
        {
            if (string.IsNullOrWhiteSpace(nombreTabla))
                throw new ArgumentException("La tabla debe tener nombre.");
            if (columnasClave == null || columnasClave.Length == 0)
                throw new ArgumentException("La tabla debe tener al menos una columna clave.");
            if (columnasDatos == null || columnasDatos.Length == 0)
                throw new ArgumentException("La tabla debe tener al menos una columna de datos.");

            nombre = nombreTabla;
            this.columnasClave = columnasClave;
            this.columnasDatos = columnasDatos;
            this.columnaDVH = columnaDVH;
        }

        #endregion

        #region PROPIEDADES

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
        }

        private string[] columnasClave;

        /// <summary>Columnas que identifican al registro (PK). Se usan para ordenar y para informar errores.</summary>
        public string[] ColumnasClave
        {
            get { return columnasClave; }
        }

        private string[] columnasDatos;

        /// <summary>
        /// Columnas que participan del cálculo del DVH, EN ORDEN. La posición de cada
        /// columna en este arreglo es la "posición del atributo" que pondera el algoritmo.
        /// </summary>
        public string[] ColumnasDatos
        {
            get { return columnasDatos; }
        }

        private string columnaDVH;

        public string ColumnaDVH
        {
            get { return columnaDVH; }
        }

        /// <summary>Todas las columnas que hay que leer (clave + datos + dvh), sin repetir.</summary>
        public IEnumerable<string> ColumnasALeer
        {
            get { return columnasClave.Concat(columnasDatos).Concat(new[] { columnaDVH }).Distinct(StringComparer.OrdinalIgnoreCase); }
        }

        #endregion

        public override string ToString()
        {
            return nombre;
        }
    }
}
