namespace BE_Entities.Languages
{
    public class Idioma
    {
        #region CONSTRUCTOR

        public Idioma() { }

        public Idioma(int id, string nombre, string codigo, bool esPorDefecto = false)
        {
            this.id = id;
            this.nombre = nombre;
            this.codigo = codigo;
            this.esPorDefecto = esPorDefecto;
        }

        #endregion

        #region PROPIEDADES

        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string codigo;

        /// <summary>Código de cultura, por ejemplo "es-AR" o "en-US".</summary>
        public string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private bool esPorDefecto;

        /// <summary>
        /// El idioma por defecto es el idioma "base" de las leyendas: sus textos son los
        /// que están escritos en los formularios, por eso no necesita traducciones cargadas.
        /// </summary>
        public bool EsPorDefecto
        {
            get { return esPorDefecto; }
            set { esPorDefecto = value; }
        }

        #endregion

        public override string ToString()
        {
            return nombre ?? "Idioma sin nombre";
        }
    }
}
