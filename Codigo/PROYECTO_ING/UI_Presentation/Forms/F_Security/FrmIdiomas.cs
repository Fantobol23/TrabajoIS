using BE_Entities.Languages;
using BLL_Business.Language_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UI_Presentation.Languages;

namespace UI_Presentation.Forms.F_Security
{
    /// <summary>
    /// ABM de idiomas y de sus leyendas. Desde acá se incorpora un idioma nuevo y se
    /// cargan sus traducciones; al guardar o al elegir "Usar este idioma", el cambio
    /// se ve en el momento en TODOS los formularios abiertos (Observer).
    /// </summary>
    public partial class FrmIdiomas : Form
    {
        private readonly Idioma_Services _idiomas = new Idioma_Services();

        public FrmIdiomas()
        {
            InitializeComponent();
            TraductorFormulario.Registrar(this);
        }

        #region LOAD/CARGA INICIAL

        private void FrmIdiomas_Load(object sender, EventArgs e)
        {
            try
            {
                CargarIdiomas();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        #endregion

        #region IDIOMAS

        private void CargarIdiomas(int idSeleccionar = 0)
        {
            var idiomas = _idiomas.ListarIdiomas();

            lstIdiomas.DataSource = null;
            lstIdiomas.DataSource = idiomas;
            lstIdiomas.DisplayMember = "Nombre";

            if (idSeleccionar > 0)
                lstIdiomas.SelectedItem = idiomas.FirstOrDefault(i => i.Id == idSeleccionar);
        }

        private void lstIdiomas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CargarTraducciones();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnAgregarIdioma_Click(object sender, EventArgs e)
        {
            try
            {
                Idioma nuevo = _idiomas.CrearIdioma(txtNombreIdioma.Text, txtCodigoIdioma.Text);
                txtNombreIdioma.Clear();
                txtCodigoIdioma.Clear();
                CargarIdiomas(nuevo.Id);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnEliminarIdioma_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(lstIdiomas.SelectedItem is Idioma idioma))
                    return;

                if (MessageBox.Show(string.Format(Traductor.T("FrmIdiomas.MsgConfirmarBorrado", "¿Eliminar el idioma '{0}' y todas sus traducciones?"), idioma.Nombre),
                        "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;

                _idiomas.BorrarIdioma(idioma);
                CargarIdiomas();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnUsarIdioma_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstIdiomas.SelectedItem is Idioma idioma)
                    _idiomas.CambiarIdioma(idioma); // notifica a todos los formularios abiertos
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        #endregion

        #region TRADUCCIONES

        private void CargarTraducciones()
        {
            dgvTraducciones.Rows.Clear();

            if (!(lstIdiomas.SelectedItem is Idioma idioma))
                return;

            Dictionary<string, string> traducciones = idioma.Id > 0
                ? _idiomas.ObtenerTraducciones(idioma)
                : new Dictionary<string, string>();

            string filtro = txtFiltro.Text.Trim();

            foreach (Etiqueta etiqueta in _idiomas.ListarEtiquetas().OrderBy(et => et.Clave))
            {
                if (filtro.Length > 0
                    && etiqueta.Clave.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) < 0
                    && (etiqueta.TextoOriginal ?? string.Empty).IndexOf(filtro, StringComparison.OrdinalIgnoreCase) < 0)
                    continue;

                string texto;
                traducciones.TryGetValue(etiqueta.Clave, out texto);
                dgvTraducciones.Rows.Add(etiqueta.Clave, etiqueta.TextoOriginal, texto ?? string.Empty);
            }

            // el idioma por defecto usa los textos originales: se puede editar igual (para corregir leyendas)
            lblAyudaTraduccion.Visible = idioma.EsPorDefecto;
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CargarTraducciones();
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        private void btnGuardarTraducciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(lstIdiomas.SelectedItem is Idioma idioma))
                    return;

                dgvTraducciones.EndEdit();

                var traducciones = new Dictionary<string, string>();
                foreach (DataGridViewRow fila in dgvTraducciones.Rows)
                {
                    string clave = Convert.ToString(fila.Cells[colClave.Index].Value);
                    string texto = Convert.ToString(fila.Cells[colTraduccion.Index].Value);
                    traducciones[clave] = texto?.Trim() ?? string.Empty;
                }

                _idiomas.GuardarTraducciones(idioma, traducciones);

                MessageBox.Show(Traductor.T("FrmIdiomas.MsgGuardado", "Traducciones guardadas."), "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MostrarError(ex);
            }
        }

        #endregion

        #region FUNCIONES/METODOS

        private static void MostrarError(Exception ex)
        {
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion
    }
}
