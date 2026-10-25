namespace BE_Entities.Languages
{
    public class Traduccion
    {
        #region CONSTRUCTOR

        public Traduccion() { }

        public Traduccion(Idioma idioma, Etiqueta etiqueta, string texto)
        {
            this.idioma = idioma;
            this.etiqueta = etiqueta;
            this.texto = texto;
        }

        #endregion

        #region PROPIEDADES

        private Idioma idioma;

        public Idioma Idioma
        {
            get { return idioma; }
            set { idioma = value; }
        }

        private Etiqueta etiqueta;

        public Etiqueta Etiqueta
        {
            get { return etiqueta; }
            set { etiqueta = value; }
        }

        private string texto;

        public string Texto
        {
            get { return texto; }
            set { texto = value; }
        }

        #endregion
    }
}
