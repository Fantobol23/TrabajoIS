using BE_Entities.Integrity;
using BE_Entities.Users;
using BLL_Business.Integrity_Services;
using BLL_Business.Users_Services;
using System;
using System.Linq;
using System.Windows.Forms;
using UI_Presentation.Languages;

namespace UI_Presentation.Forms.F_Security
{
    /// <summary>
    /// Se muestra al iniciar, ANTES del login, si la verificación de dígitos verificadores
    /// encontró errores. Informa al administrador y le permite restaurar los dígitos.
    /// DialogResult.OK = la base quedó íntegra y se puede continuar al login.
    /// </summary>
    public partial class FrmIntegridad : Form
    {
        public const string PERMISO_RESTAURAR = "SEG_INTEGRIDAD_RESTAURAR";

        private ResultadoIntegridad _resultado;
        private readonly DigitoVerificador_Services _dvServices = new DigitoVerificador_Services();

        public FrmIntegridad(ResultadoIntegridad resultado)
        {
            InitializeComponent();
            TraductorFormulario.Registrar(this);
            _resultado = resultado;
        }

        #region LOAD/CARGA INICIAL

        private void FrmIntegridad_Load(object sender, EventArgs e)
        {
            MostrarResultado();
        }

        #endregion

        #region BOTONES

        private void btnRecalcular_Click(object sender, EventArgs e)
        {
            try
            {
                var usuarios = new UserAccount_Services();
                UserAccount admin = usuarios.ValidarCredencialesConPermiso(txtUsuario.Text.Trim(), txtPassword.Text, PERMISO_RESTAURAR);

                if (admin == null)
                {
                    MessageBox.Show(Traductor.T("FrmIntegridad.MsgSinPermiso", "Usuario o contraseña incorrectos, o el usuario no tiene el permiso para restaurar la integridad."),
                        "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirmacion = MessageBox.Show(
                    Traductor.T("FrmIntegridad.MsgConfirmar", "Se recalcularán los dígitos verificadores de las tablas con error y el contenido ACTUAL quedará como válido. Hágalo sólo después de revisar o restaurar los datos. ¿Continuar?"),
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirmacion != DialogResult.Yes)
                    return;

                foreach (string tabla in _resultado.TablasConError)
                {
                    _dvServices.RecalcularTabla(tabla);
                }

                Reverificar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                txtPassword.Text = string.Empty;
            }
        }

        private void btnReverificar_Click(object sender, EventArgs e)
        {
            try
            {
                Reverificar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        #endregion

        #region FUNCIONES/METODOS

        private void Reverificar()
        {
            _resultado = _dvServices.VerificarIntegridad();

            if (_resultado.EsValida)
            {
                MessageBox.Show(Traductor.T("FrmIntegridad.MsgIntegra", "La base de datos está íntegra. Puede continuar."),
                    "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
                return;
            }

            MostrarResultado();
        }

        private void MostrarResultado()
        {
            dgvErrores.Rows.Clear();

            foreach (var error in _resultado.Errores)
            {
                dgvErrores.Rows.Add(error.Tabla, DescribirTipo(error.Tipo), error.Registro, error.Detalle);
            }

            lblCantidad.Text = string.Format(Traductor.T("FrmIntegridad.MsgCantidad", "{0} inconsistencia(s) en: {1}"),
                _resultado.Errores.Count, string.Join(", ", _resultado.TablasConError));
        }

        private static string DescribirTipo(TipoErrorIntegridad tipo)
        {
            switch (tipo)
            {
                case TipoErrorIntegridad.DVH_Invalido: return Traductor.T("FrmIntegridad.TipoDVH", "DVH (registro alterado)");
                case TipoErrorIntegridad.DVV_Invalido: return Traductor.T("FrmIntegridad.TipoDVV", "DVV (registros agregados/eliminados)");
                default: return Traductor.T("FrmIntegridad.TipoDVVInexistente", "DVV inexistente");
            }
        }

        #endregion
    }
}
