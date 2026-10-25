namespace BE_Entities.Integrity
{
    public class ErrorIntegridad
    {
        #region CONSTRUCTOR

        public ErrorIntegridad() { }

        public ErrorIntegridad(string tabla, TipoErrorIntegridad tipo, string registro, string detalle)
        {
            this.tabla = tabla;
            this.tipo = tipo;
            this.registro = registro;
            this.detalle = detalle;
        }

        #endregion

        #region PROPIEDADES

        private string tabla;

        public string Tabla
        {
            get { return tabla; }
            set { tabla = value; }
        }

        private TipoErrorIntegridad tipo;

        public TipoErrorIntegridad Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private string registro;

        /// <summary>Clave del registro afectado (vacío si el error es de toda la tabla).</summary>
        public string Registro
        {
            get { return registro; }
            set { registro = value; }
        }

        private string detalle;

        public string Detalle
        {
            get { return detalle; }
            set { detalle = value; }
        }

        #endregion

        public override string ToString()
        {
            return $"[{tabla}] {tipo} {registro} {detalle}".Trim();
        }
    }
}
