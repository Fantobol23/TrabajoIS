using BE_Entities;
using BE_Entities.ActivityLog;
using BE_Entities.Users;
using DAL_DataAccess;
using DAL_DataAccess.DataEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_Business.ActivityLog_Services
{
    public class ActivityLog_Services
    {
        ActivityLog_Data _activityRepository;

        #region FUNCIONES/METODOS

        public void GuardarActividad(ActivityLog activity)
        {
            try
            {
                _activityRepository = new ActivityLog_Data();
                _activityRepository.Insertar(activity);
            }
            catch (Exception ex)
            {
                throw new Exception("BLL_ActivityLog_Services: GuardarActividad() / " + ex.Message);
            }
        }

        public List<ActivityLog> ObtenerTodos()
        {
            try
            {
                _activityRepository = new ActivityLog_Data();

                List<ActivityLog> actividades = _activityRepository.ListarTodo();

                return actividades;
            }
            catch
            {
                throw new Exception("Error al traer la información de la base de datos");
            }
        }
        #endregion
    }
}
