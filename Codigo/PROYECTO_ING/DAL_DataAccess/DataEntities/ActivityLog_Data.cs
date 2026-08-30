using BE_Entities.ActivityLog;
using BE_Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_DataAccess.DataEntities
{
    public class ActivityLog_Data : Mapper<ActivityLog>
    {
        public ActivityLog_Data()
        {
            acceso = new AccessDB();
        }

        public override int Insertar(ActivityLog act_log)
        {
            int res;

            try
            {
                List<SqlParameter> parametros = new List<SqlParameter>();

                parametros.Add(acceso.CrearParametro("@username", act_log.UserAccount.Username));
                parametros.Add(acceso.CrearParametro("@id_activity_type", act_log.TipoLog));
                parametros.Add(acceso.CrearParametro("@date_activity", act_log.FechaLog));

                acceso.AbrirConexionBD();
                res = acceso.EscribirBD("SP_ActivityLog_Insertar", parametros);
                acceso.CerrarConexionBD();

                return res;
            }
            catch (Exception)
            {
                return res = -1;
                throw new NotImplementedException();
            }
            finally
            {
                GC.Collect();
            }
        }

        public override int Editar(ActivityLog obj)
        {
            throw new NotImplementedException();
        }

        public override int Borrar(ActivityLog obj)
        {
            throw new NotImplementedException();
        }

        public override List<ActivityLog> ListarTodo()
        {
            try
            {
                List<ActivityLog> actividades = new List<ActivityLog>();

                acceso.AbrirConexionBD();
                DataTable tb = acceso.LeerBD("SP_ActivityLog_ObtenerTodos");

                foreach (DataRow row in tb.Rows)
                {
                    ActivityLog actividad = new ActivityLog();

                    actividad.IdLog = int.Parse(row["id_activity_log"].ToString());
                    actividad.FechaLog = DateTime.Parse(row["date_activity"].ToString());
                    actividad.UserAccount = new UserAccount();
                    actividad.UserAccount.Username = row["username"].ToString();
                    actividad.TipoLog = (ActivityType)int.Parse(row["id_activity_type"].ToString());

                    actividades.Add(actividad);
                }
                return actividades;
            }
            catch
            {
                throw new NotImplementedException();
            }
            finally
            {
                acceso.CerrarConexionBD();
                GC.Collect();
            }
        }
    }
}
