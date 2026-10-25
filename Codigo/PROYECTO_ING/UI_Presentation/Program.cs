using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_Entities.Integrity;
using BLL_Business.Integrity_Services;
using BLL_Business.Language_Services;
using UI_Presentation.Forms.F_Security;

namespace UI_Presentation
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 1) Idioma por defecto (si la BD todavía no tiene las tablas de idiomas, sigue en español)
            try
            {
                new Idioma_Services().InicializarIdioma();
            }
            catch
            {
            }

            // 2) Verificación de integridad ANTES de dar acceso a la ventana de login
            ResultadoIntegridad resultado;
            try
            {
                resultado = new DigitoVerificador_Services().VerificarIntegridad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo verificar la integridad de la base de datos. Verifique la conexión (App.config) " +
                                "y que se haya ejecutado el script de la base.\n\n" + ex.Message,
                                "PROYECTO_ING", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!resultado.EsValida)
            {
                using (var frmIntegridad = new FrmIntegridad(resultado))
                {
                    if (frmIntegridad.ShowDialog() != DialogResult.OK)
                        return; // el administrador no restauró: no se permite el ingreso
                }
            }

            // 3) Recién ahora, el login
            Application.Run(new FrmLogin());
        }
    }
}
