using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Entities.Permissions
{
    public interface IPermission
    {
        #region PROPIEDADES

        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }

        #endregion

        bool Contiene(string nombrePermiso);

        List<IPermission> ObtenerPermisosSimples();
    }
}
