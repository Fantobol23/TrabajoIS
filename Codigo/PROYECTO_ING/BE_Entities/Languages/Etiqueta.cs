namespace BE_Entities.Languages
{
    /// <summary>
    /// Una leyenda de la interfaz (texto de un botón, label, menú, título, mensaje...).
    /// La Clave la identifica en todos los idiomas, p. ej. "FrmLogin.btnLogin".
    /// </summary>
    public class Etiqueta
    {
        #region CONSTRUCTOR

        public Etiqueta() { }

        public Etiqueta(int id, string clave, string textoOriginal)
        {
            this.id = id;
            this.clave = clave;
            this.textoOriginal = textoOriginal;
        }

        #endregion

        #region PROPIEDADES

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string clave;

        public string Clave
        {
            get { return clave; }
            set { clave = value; }
        }

        private string textoOriginal;

        /// <summary>Texto en el idioma por defecto (el que figura en el diseño del formulario).</summary>
        public string TextoOriginal
        {
            get { return textoOriginal; }
            set { textoOriginal = value; }
        }

        #endregion

        public override string ToString()
        {
            return clave;
        }
    }
}
