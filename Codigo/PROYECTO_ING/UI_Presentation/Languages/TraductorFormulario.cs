using BE_Entities.Languages;
using BLL_Business.Language_Services;
using SERVICES.Languages;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UI_Presentation.Languages
{
    /// <summary>
    /// OBSERVER CONCRETO. Hay uno por formulario abierto: se suscribe al GestorIdioma
    /// cuando el formulario se crea y se desuscribe cuando se cierra.
    ///
    /// Al registrarse recorre RECURSIVAMENTE el formulario (controles, contenedores,
    /// menús y submenús) y arma la clave de cada leyenda como "Formulario.Control".
    /// Cuando el Subject notifica un cambio de idioma, vuelve a recorrer y reemplaza
    /// cada texto por la traducción. Los formularios no tienen que implementar nada:
    /// sólo llamar a TraductorFormulario.Registrar(this).
    /// </summary>
    public class TraductorFormulario : IIdiomaObserver
    {
        public const string TAG_NO_TRADUCIR = "NoTraducir";
        private const string SUFIJO_TITULO = "_Titulo";

        private readonly Form _formulario;

        // clave → texto original (idioma por defecto, el del diseñador)
        private readonly Dictionary<string, string> _originales = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // clave → último texto que puso el traductor (para no pisar textos que cambió el código, p. ej. datos del usuario)
        private readonly Dictionary<string, string> _aplicados = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        #region REGISTRO

        public static TraductorFormulario Registrar(Form formulario)
        {
            var traductor = new TraductorFormulario(formulario);
            return traductor;
        }

        private TraductorFormulario(Form formulario)
        {
            _formulario = formulario ?? throw new ArgumentNullException(nameof(formulario));

            CapturarOriginales();
            RegistrarEtiquetasEnBD();

            GestorIdioma.Instancia.Suscribir(this);
            _formulario.FormClosed += (s, e) => GestorIdioma.Instancia.Desuscribir(this);

            ActualizarIdioma(GestorIdioma.Instancia.IdiomaActual);
        }

        /// <summary>Llamar si el formulario agrega controles o menús por código después de registrarse.</summary>
        public void Refrescar()
        {
            CapturarOriginales();
            RegistrarEtiquetasEnBD();
            ActualizarIdioma(GestorIdioma.Instancia.IdiomaActual);
        }

        private void CapturarOriginales()
        {
            Recorrer((clave, textoActual, asignar) =>
            {
                if (!_originales.ContainsKey(clave))
                {
                    _originales[clave] = textoActual;
                    _aplicados[clave] = textoActual;
                }
            });
        }

        private void RegistrarEtiquetasEnBD()
        {
            try
            {
                new Idioma_Services().RegistrarEtiquetas(_originales);
            }
            catch
            {
                // sin BD de idiomas el sistema sigue funcionando en el idioma por defecto
            }
        }

        #endregion

        #region OBSERVER

        public void ActualizarIdioma(Idioma nuevoIdioma)
        {
            if (_formulario.IsDisposed)
                return;

            Recorrer((clave, textoActual, asignar) =>
            {
                string original;
                if (!_originales.TryGetValue(clave, out original))
                    return;

                // si el código del formulario cambió el texto (p. ej. muestra datos), no se pisa
                string ultimoAplicado;
                if (_aplicados.TryGetValue(clave, out ultimoAplicado) && textoActual != ultimoAplicado)
                    return;

                string traducido = GestorIdioma.Instancia.Traducir(clave, original);
                asignar(traducido);
                _aplicados[clave] = traducido;
            });
        }

        #endregion

        #region RECORRIDO RECURSIVO

        /// <summary>
        /// Recorre título, controles (recursivo) y menús (recursivo). Por cada leyenda
        /// llama a "accion" con: clave, texto actual y una función para asignarle un texto nuevo.
        /// </summary>
        private void Recorrer(Action<string, string, Action<string>> accion)
        {
            string nombreForm = string.IsNullOrEmpty(_formulario.Name) ? _formulario.GetType().Name : _formulario.Name;

            if (!string.IsNullOrWhiteSpace(_formulario.Text))
                accion(nombreForm + "." + SUFIJO_TITULO, _formulario.Text, t => _formulario.Text = t);

            RecorrerControles(nombreForm, _formulario.Controls, accion);
        }

        private void RecorrerControles(string nombreForm, Control.ControlCollection controles, Action<string, string, Action<string>> accion)
        {
            foreach (Control control in controles)
            {
                if (EsNoTraducible(control.Tag))
                    continue;

                if (control is ToolStrip barra) // MenuStrip, StatusStrip, ToolStrip
                {
                    RecorrerItems(nombreForm, barra.Items, accion);
                    continue;
                }

                if (EsControlConLeyenda(control) && !string.IsNullOrWhiteSpace(control.Text) && !string.IsNullOrEmpty(control.Name))
                {
                    Control c = control;
                    accion(nombreForm + "." + control.Name, control.Text, t => c.Text = t);
                }

                if (control is DataGridView grilla)
                {
                    foreach (DataGridViewColumn columna in grilla.Columns)
                    {
                        if (columna.DataPropertyName == columna.Name && grilla.AutoGenerateColumns && grilla.DataSource != null)
                            continue; // columnas autogeneradas: las maneja el propio formulario

                        DataGridViewColumn col = columna;
                        if (!string.IsNullOrWhiteSpace(col.HeaderText) && !string.IsNullOrEmpty(col.Name))
                            accion(nombreForm + "." + col.Name, col.HeaderText, t => col.HeaderText = t);
                    }
                }

                if (control.HasChildren)
                    RecorrerControles(nombreForm, control.Controls, accion);
            }
        }

        private void RecorrerItems(string nombreForm, ToolStripItemCollection items, Action<string, string, Action<string>> accion)
        {
            foreach (ToolStripItem item in items)
            {
                if (EsNoTraducible(item.Tag))
                    continue;

                if (!string.IsNullOrWhiteSpace(item.Text) && !string.IsNullOrEmpty(item.Name))
                {
                    ToolStripItem it = item;
                    accion(nombreForm + "." + item.Name, item.Text, t => it.Text = t);
                }

                if (item is ToolStripDropDownItem desplegable && desplegable.HasDropDownItems)
                    RecorrerItems(nombreForm, desplegable.DropDownItems, accion);
            }
        }

        /// <summary>Sólo controles cuyo Text es una leyenda (no TextBox/ComboBox, que muestran datos).</summary>
        private static bool EsControlConLeyenda(Control control)
        {
            return control is Label
                || control is ButtonBase   // Button, CheckBox, RadioButton
                || control is GroupBox
                || control is TabPage;
        }

        private static bool EsNoTraducible(object tag)
        {
            return tag is string s && s == TAG_NO_TRADUCIR;
        }

        #endregion
    }
}
