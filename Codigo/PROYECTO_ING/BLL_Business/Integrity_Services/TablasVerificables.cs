using BE_Entities.Integrity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL_Business.Integrity_Services
{
    /// <summary>
    /// Registro de las entidades protegidas con dígitos verificadores.
    /// Para proteger una entidad nueva alcanza con: 1) agregarle la columna dvh BIGINT,
    /// 2) registrarla acá y 3) envolver sus altas/bajas/modificaciones con
    /// DigitoVerificador_Services.Ejecutar(...).
    ///
    /// El estado de un UserAccount sale de VARIAS tablas: usersaccounts (datos y clave),
    /// user_roles (qué roles tiene), role_permissions y permission_composition (qué le
    /// otorga cada rol). Por eso se protegen las cuatro: el objeto sólo se considera íntegro
    /// si TODAS las tablas de las que se arma son íntegras.
    /// </summary>
    public static class TablasVerificables
    {
        public const string USUARIOS = "usersaccounts";
        public const string USUARIO_ROLES = "user_roles";
        public const string ROL_PERMISOS = "role_permissions";
        public const string COMPOSICION_PERMISOS = "permission_composition";

        private static readonly List<TablaVerificable> _tablas = new List<TablaVerificable>
        {
            // Entidad de negocio más sensible: credenciales y estado de las cuentas.
            new TablaVerificable(USUARIOS,
                new[] { "id_user" },
                new[] { "id_user", "username", "userpassword", "firstname", "lastname", "email_opt_1", "email_opt_2", "id_user_status" }),

            new TablaVerificable(USUARIO_ROLES,
                new[] { "id_user", "id_role" },
                new[] { "id_user", "id_role" }),

            new TablaVerificable(ROL_PERMISOS,
                new[] { "id_role", "id_permission" },
                new[] { "id_role", "id_permission" }),

            new TablaVerificable(COMPOSICION_PERMISOS,
                new[] { "id_composite_permission", "id_child_permission" },
                new[] { "id_composite_permission", "id_child_permission" })
        };

        public static IReadOnlyList<TablaVerificable> Todas
        {
            get { return _tablas.AsReadOnly(); }
        }

        public static TablaVerificable Obtener(string nombreTabla)
        {
            var tabla = _tablas.FirstOrDefault(t => string.Equals(t.Nombre, nombreTabla, StringComparison.OrdinalIgnoreCase));

            if (tabla == null)
                throw new ArgumentException("La tabla '" + nombreTabla + "' no está registrada como verificable.");

            return tabla;
        }

        public static void Registrar(TablaVerificable tabla)
        {
            if (tabla == null) throw new ArgumentNullException(nameof(tabla));

            _tablas.RemoveAll(t => string.Equals(t.Nombre, tabla.Nombre, StringComparison.OrdinalIgnoreCase));
            _tablas.Add(tabla);
        }
    }
}
