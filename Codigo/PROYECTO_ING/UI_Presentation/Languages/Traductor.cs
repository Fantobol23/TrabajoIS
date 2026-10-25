using BLL_Business.Language_Services;
using SERVICES.Languages;
using System.Collections.Generic;

namespace UI_Presentation.Languages
{
    /// <summary>
    /// Atajo para traducir textos armados por código (mensajes de MessageBox, títulos dinámicos).
    /// Si la clave no existe todavía, la registra con su texto original para que se pueda
    /// traducir desde la pantalla de idiomas.
    /// </summary>
    public static class Traductor
    {
        public static string T(string clave, string textoOriginal)
        {
            try
            {
                new Idioma_Services().RegistrarEtiquetas(new Dictionary<string, string> { { clave, textoOriginal } });
            }
            catch
            {
            }

            return GestorIdioma.Instancia.Traducir(clave, textoOriginal);
        }
    }
}
