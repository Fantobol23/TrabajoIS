using BE_Entities.Languages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SERVICES.Languages
{
    /// <summary>
    /// SUBJECT CONCRETO (Singleton). Guarda el idioma activo y su diccionario
    /// clave → texto. No conoce Windows Forms ni la base de datos: el diccionario se lo
    /// entrega la BLL y los observadores deciden cómo mostrarlo. Por eso el modelo es
    /// reutilizable y no está acoplado a la UI.
    /// </summary>
    public class GestorIdioma : IIdiomaSubject
    {
        #region SINGLETON

        private static GestorIdioma _instancia;
        private static readonly object _lock = new object();

        public static GestorIdioma Instancia
        {
            get
            {
                lock (_lock)
                {
                    if (_instancia == null)
                        _instancia = new GestorIdioma();
                    return _instancia;
                }
            }
        }

        private GestorIdioma()
        {
            observadores = new List<IIdiomaObserver>();
            traducciones = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region PROPIEDADES

        private readonly List<IIdiomaObserver> observadores;
        private Dictionary<string, string> traducciones;

        private Idioma idiomaActual;

        public Idioma IdiomaActual
        {
            get { return idiomaActual; }
        }

        public int CantidadObservadores
        {
            get { return observadores.Count; }
        }

        #endregion

        #region OBSERVER

        public void Suscribir(IIdiomaObserver observador)
        {
            if (observador != null && !observadores.Contains(observador))
                observadores.Add(observador);
        }

        public void Desuscribir(IIdiomaObserver observador)
        {
            observadores.Remove(observador);
        }

        public void Notificar()
        {
            // copia: un observador puede desuscribirse mientras se notifica
            foreach (var observador in observadores.ToList())
            {
                observador.ActualizarIdioma(idiomaActual);
            }
        }

        #endregion

        #region IDIOMA

        /// <summary>Cambia el idioma activo y avisa a todos los observadores.</summary>
        public void CambiarIdioma(Idioma idioma, IDictionary<string, string> textos)
        {
            idiomaActual = idioma ?? throw new ArgumentNullException(nameof(idioma));
            traducciones = new Dictionary<string, string>(textos ?? new Dictionary<string, string>(), StringComparer.OrdinalIgnoreCase);
            Notificar();
        }

        /// <summary>
        /// Devuelve el texto de la clave en el idioma activo. Si no hay traducción,
        /// devuelve el texto original (idioma por defecto), así nunca queda una leyenda vacía.
        /// </summary>
        public string Traducir(string clave, string textoPorDefecto)
        {
            if (!string.IsNullOrEmpty(clave) && traducciones.TryGetValue(clave, out string texto) && !string.IsNullOrEmpty(texto))
                return texto;

            return textoPorDefecto;
        }

        public bool TieneTraduccion(string clave)
        {
            return clave != null && traducciones.ContainsKey(clave);
        }

        #endregion
    }
}
