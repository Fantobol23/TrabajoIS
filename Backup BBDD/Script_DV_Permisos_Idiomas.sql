/* =====================================================================================
   PROYECTO_ING - Script de base de datos para las consignas:
     1) Dígitos verificadores (DVH en las tablas + entidad DVV)
     2) Permisos con patrón Composite (permisos, composición, roles, asignaciones)
     3) Multi-idioma (idiomas, leyendas, traducciones)

   Es IDEMPOTENTE: se puede ejecutar varias veces. No borra datos existentes.
   Requisito: SQL Server 2016 SP1 o superior (usa CREATE OR ALTER).

   IMPORTANTE - primer arranque después de ejecutarlo:
     Las filas existentes quedan con dvh = NULL y no hay DVV, así que la aplicación
     va a mostrar la pantalla "ERROR DE INTEGRIDAD". Es lo esperado. Ingresar ahí con
     el usuario administrador (ver sección 6: se le asigna el rol Administrador al
     primer usuario de usersaccounts) y presionar "Recalcular dígitos verificadores".
   ===================================================================================== */

USE PROYECTO_ING_DB;
GO

/* =====================================================================================
   1. PERMISOS (COMPOSITE)
   ===================================================================================== */

IF OBJECT_ID('dbo.permissions', 'U') IS NULL
CREATE TABLE dbo.permissions (
    id_permission    INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_permissions PRIMARY KEY,
    permission_name  NVARCHAR(100)     NOT NULL CONSTRAINT UQ_permissions_name UNIQUE,
    description      NVARCHAR(255)     NULL,
    permission_type  VARCHAR(10)       NOT NULL CONSTRAINT CK_permissions_type CHECK (permission_type IN ('SIMPLE', 'COMPOSITE'))
);
GO

-- Relación recursiva N a N: un permiso COMPOSITE contiene permisos (simples o compuestos)
IF OBJECT_ID('dbo.permission_composition', 'U') IS NULL
CREATE TABLE dbo.permission_composition (
    id_composite_permission INT NOT NULL CONSTRAINT FK_pc_composite REFERENCES dbo.permissions(id_permission),
    id_child_permission     INT NOT NULL CONSTRAINT FK_pc_child     REFERENCES dbo.permissions(id_permission),
    CONSTRAINT PK_permission_composition PRIMARY KEY (id_composite_permission, id_child_permission),
    CONSTRAINT CK_pc_no_self CHECK (id_composite_permission <> id_child_permission)
);
GO

IF OBJECT_ID('dbo.roles', 'U') IS NULL
CREATE TABLE dbo.roles (
    id_role     INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_roles PRIMARY KEY,
    role_name   NVARCHAR(100)     NOT NULL CONSTRAINT UQ_roles_name UNIQUE,
    description NVARCHAR(255)     NULL
);
GO

IF OBJECT_ID('dbo.role_permissions', 'U') IS NULL
CREATE TABLE dbo.role_permissions (
    id_role       INT NOT NULL CONSTRAINT FK_rp_role       REFERENCES dbo.roles(id_role),
    id_permission INT NOT NULL CONSTRAINT FK_rp_permission REFERENCES dbo.permissions(id_permission),
    CONSTRAINT PK_role_permissions PRIMARY KEY (id_role, id_permission)
);
GO

-- id_user se crea con el MISMO tipo que usersaccounts.id_user (puede no ser INT)
IF OBJECT_ID('dbo.user_roles', 'U') IS NULL
BEGIN
    DECLARE @tipo NVARCHAR(200) = (
        SELECT TOP 1 QUOTENAME(ty.name) +
            CASE WHEN ty.name IN ('decimal', 'numeric') THEN '(' + CAST(c.precision AS VARCHAR(5)) + ',' + CAST(c.scale AS VARCHAR(5)) + ')'
                 WHEN ty.name IN ('varchar', 'char', 'varbinary', 'binary') THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length AS VARCHAR(5)) END + ')'
                 WHEN ty.name IN ('nvarchar', 'nchar') THEN '(' + CASE WHEN c.max_length = -1 THEN 'MAX' ELSE CAST(c.max_length / 2 AS VARCHAR(5)) END + ')'
                 ELSE '' END
        FROM sys.columns c
        INNER JOIN sys.types ty ON ty.user_type_id = c.user_type_id
        WHERE c.object_id = OBJECT_ID('dbo.usersaccounts') AND c.name = 'id_user');
    DECLARE @sql NVARCHAR(MAX) = N'CREATE TABLE dbo.user_roles (
        id_user ' + @tipo + N' NOT NULL CONSTRAINT FK_ur_user REFERENCES dbo.usersaccounts(id_user),
        id_role INT NOT NULL CONSTRAINT FK_ur_role REFERENCES dbo.roles(id_role),
        CONSTRAINT PK_user_roles PRIMARY KEY (id_user, id_role))';
    EXEC sp_executesql @sql;
END
GO

/* =====================================================================================
   2. DÍGITOS VERIFICADORES
      - DVH: columna "dvh" en cada tabla protegida.
      - DVV: entidad adicional "dvv" (una fila por tabla protegida).
   ===================================================================================== */

IF COL_LENGTH('dbo.usersaccounts', 'dvh') IS NULL
    ALTER TABLE dbo.usersaccounts ADD dvh BIGINT NULL;
GO
IF COL_LENGTH('dbo.user_roles', 'dvh') IS NULL
    ALTER TABLE dbo.user_roles ADD dvh BIGINT NULL;
GO
IF COL_LENGTH('dbo.role_permissions', 'dvh') IS NULL
    ALTER TABLE dbo.role_permissions ADD dvh BIGINT NULL;
GO
IF COL_LENGTH('dbo.permission_composition', 'dvh') IS NULL
    ALTER TABLE dbo.permission_composition ADD dvh BIGINT NULL;
GO

IF OBJECT_ID('dbo.dvv', 'U') IS NULL
CREATE TABLE dbo.dvv (
    table_name  VARCHAR(128) NOT NULL CONSTRAINT PK_dvv PRIMARY KEY,
    dvv_value   BIGINT       NOT NULL,
    last_update DATETIME     NOT NULL CONSTRAINT DF_dvv_last_update DEFAULT (GETDATE())
);
GO

/* =====================================================================================
   3. MULTI-IDIOMA
   ===================================================================================== */

IF OBJECT_ID('dbo.languages', 'U') IS NULL
CREATE TABLE dbo.languages (
    id_language   INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_languages PRIMARY KEY,
    language_name NVARCHAR(50)      NOT NULL CONSTRAINT UQ_languages_name UNIQUE,
    culture_code  VARCHAR(10)       NOT NULL,
    is_default    BIT               NOT NULL CONSTRAINT DF_languages_default DEFAULT (0)
);
GO

-- Leyenda: clave única (Formulario.Control) y su texto en el idioma por defecto
IF OBJECT_ID('dbo.labels', 'U') IS NULL
CREATE TABLE dbo.labels (
    id_label     INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_labels PRIMARY KEY,
    label_key    NVARCHAR(200)     NOT NULL CONSTRAINT UQ_labels_key UNIQUE,
    default_text NVARCHAR(1000)    NULL
);
GO

IF OBJECT_ID('dbo.translations', 'U') IS NULL
CREATE TABLE dbo.translations (
    id_language     INT            NOT NULL CONSTRAINT FK_tr_language REFERENCES dbo.languages(id_language),
    id_label        INT            NOT NULL CONSTRAINT FK_tr_label    REFERENCES dbo.labels(id_label),
    translated_text NVARCHAR(1000) NOT NULL,
    CONSTRAINT PK_translations PRIMARY KEY (id_language, id_label)
);
GO

/* =====================================================================================
   4. STORED PROCEDURES - PERMISOS / ROLES (contratos usados por Permission_Data y Role_Data)
   ===================================================================================== */

CREATE OR ALTER PROCEDURE dbo.SP_Permissions_Insertar
    @permission_name NVARCHAR(100),
    @description     NVARCHAR(255),
    @permission_type VARCHAR(10),
    @id_permission   INT OUTPUT
AS
BEGIN
    INSERT INTO dbo.permissions (permission_name, description, permission_type)
    VALUES (@permission_name, @description, @permission_type);

    SET @id_permission = SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Permissions_ObtenerTodos
AS
BEGIN
    SELECT id_permission, permission_name, description, permission_type
    FROM dbo.permissions
    ORDER BY permission_name;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Permissions_Editar
    @id_permission   INT,
    @permission_name NVARCHAR(100),
    @description     NVARCHAR(255)
AS
BEGIN
    UPDATE dbo.permissions
    SET permission_name = @permission_name, description = @description
    WHERE id_permission = @id_permission;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Permissions_Borrar
    @id_permission INT
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRAN;
        DELETE FROM dbo.permission_composition WHERE id_composite_permission = @id_permission OR id_child_permission = @id_permission;
        DELETE FROM dbo.role_permissions       WHERE id_permission = @id_permission;
        DELETE FROM dbo.permissions            WHERE id_permission = @id_permission;
    COMMIT;
END
GO

-- Hijos directos de un compuesto (la recursión la hace la DAL / la UI)
CREATE OR ALTER PROCEDURE dbo.SP_PermissionComposition_ObtenerHijos
    @id_composite_permission INT
AS
BEGIN
    SELECT p.id_permission, p.permission_name, p.description, p.permission_type
    FROM dbo.permission_composition pc
    INNER JOIN dbo.permissions p ON p.id_permission = pc.id_child_permission
    WHERE pc.id_composite_permission = @id_composite_permission
    ORDER BY p.permission_name;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_PermissionComposition_Agregar
    @id_composite_permission INT,
    @id_child_permission     INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.permission_composition
                   WHERE id_composite_permission = @id_composite_permission AND id_child_permission = @id_child_permission)
        INSERT INTO dbo.permission_composition (id_composite_permission, id_child_permission)
        VALUES (@id_composite_permission, @id_child_permission);
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_PermissionComposition_Quitar
    @id_composite_permission INT,
    @id_child_permission     INT
AS
BEGIN
    DELETE FROM dbo.permission_composition
    WHERE id_composite_permission = @id_composite_permission AND id_child_permission = @id_child_permission;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Roles_Insertar
    @role_name   NVARCHAR(100),
    @description NVARCHAR(255),
    @id_role     INT OUTPUT
AS
BEGIN
    INSERT INTO dbo.roles (role_name, description) VALUES (@role_name, @description);
    SET @id_role = SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Roles_ObtenerTodos
AS
BEGIN
    SELECT id_role, role_name, description FROM dbo.roles ORDER BY role_name;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Roles_ObtenerPorId
    @id_role INT
AS
BEGIN
    SELECT id_role, role_name, description FROM dbo.roles WHERE id_role = @id_role;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Roles_Editar
    @id_role     INT,
    @role_name   NVARCHAR(100),
    @description NVARCHAR(255)
AS
BEGIN
    UPDATE dbo.roles SET role_name = @role_name, description = @description WHERE id_role = @id_role;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Roles_Borrar
    @id_role INT
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRAN;
        DELETE FROM dbo.role_permissions WHERE id_role = @id_role;
        DELETE FROM dbo.user_roles       WHERE id_role = @id_role;
        DELETE FROM dbo.roles            WHERE id_role = @id_role;
    COMMIT;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_RolePermissions_ObtenerPorRol
    @id_role INT
AS
BEGIN
    SELECT id_permission FROM dbo.role_permissions WHERE id_role = @id_role;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_RolePermissions_Asignar
    @id_role       INT,
    @id_permission INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.role_permissions WHERE id_role = @id_role AND id_permission = @id_permission)
        INSERT INTO dbo.role_permissions (id_role, id_permission) VALUES (@id_role, @id_permission);
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_RolePermissions_Quitar
    @id_role       INT,
    @id_permission INT
AS
BEGIN
    DELETE FROM dbo.role_permissions WHERE id_role = @id_role AND id_permission = @id_permission;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_UserRoles_ObtenerPorUsuario
    @id_user INT
AS
BEGIN
    SELECT r.id_role, r.role_name, r.description
    FROM dbo.user_roles ur
    INNER JOIN dbo.roles r ON r.id_role = ur.id_role
    WHERE ur.id_user = @id_user
    ORDER BY r.role_name;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_UserRoles_Asignar
    @id_user INT,
    @id_role INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.user_roles WHERE id_user = @id_user AND id_role = @id_role)
        INSERT INTO dbo.user_roles (id_user, id_role) VALUES (@id_user, @id_role);
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_UserRoles_Quitar
    @id_user INT,
    @id_role INT
AS
BEGIN
    DELETE FROM dbo.user_roles WHERE id_user = @id_user AND id_role = @id_role;
END
GO

/* =====================================================================================
   5. STORED PROCEDURES - DVV E IDIOMAS
   ===================================================================================== */

CREATE OR ALTER PROCEDURE dbo.SP_DVV_Obtener
    @table_name VARCHAR(128)
AS
BEGIN
    SELECT table_name, dvv_value, last_update FROM dbo.dvv WHERE table_name = @table_name;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_DVV_Guardar
    @table_name VARCHAR(128),
    @dvv_value  BIGINT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM dbo.dvv WHERE table_name = @table_name)
        UPDATE dbo.dvv SET dvv_value = @dvv_value, last_update = GETDATE() WHERE table_name = @table_name;
    ELSE
        INSERT INTO dbo.dvv (table_name, dvv_value, last_update) VALUES (@table_name, @dvv_value, GETDATE());
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Languages_ObtenerTodos
AS
BEGIN
    SELECT id_language, language_name, culture_code, is_default
    FROM dbo.languages
    ORDER BY is_default DESC, language_name;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Languages_Insertar
    @language_name NVARCHAR(50),
    @culture_code  VARCHAR(10),
    @id_language   INT OUTPUT
AS
BEGIN
    INSERT INTO dbo.languages (language_name, culture_code, is_default) VALUES (@language_name, @culture_code, 0);
    SET @id_language = SCOPE_IDENTITY();
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Languages_Editar
    @id_language   INT,
    @language_name NVARCHAR(50),
    @culture_code  VARCHAR(10)
AS
BEGIN
    UPDATE dbo.languages SET language_name = @language_name, culture_code = @culture_code WHERE id_language = @id_language;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Languages_Borrar
    @id_language INT
AS
BEGIN
    SET XACT_ABORT ON;
    IF EXISTS (SELECT 1 FROM dbo.languages WHERE id_language = @id_language AND is_default = 1)
    BEGIN
        RAISERROR('No se puede borrar el idioma por defecto.', 16, 1);
        RETURN;
    END

    BEGIN TRAN;
        DELETE FROM dbo.translations WHERE id_language = @id_language;
        DELETE FROM dbo.languages    WHERE id_language = @id_language;
    COMMIT;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Labels_ObtenerTodas
AS
BEGIN
    SELECT id_label, label_key, default_text FROM dbo.labels ORDER BY label_key;
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Labels_Registrar
    @label_key    NVARCHAR(200),
    @default_text NVARCHAR(1000)
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM dbo.labels WHERE label_key = @label_key)
        INSERT INTO dbo.labels (label_key, default_text) VALUES (@label_key, @default_text);
END
GO

CREATE OR ALTER PROCEDURE dbo.SP_Translations_ObtenerPorIdioma
    @id_language INT
AS
BEGIN
    SELECT l.label_key, t.translated_text
    FROM dbo.translations t
    INNER JOIN dbo.labels l ON l.id_label = t.id_label
    WHERE t.id_language = @id_language;
END
GO

-- Inserta/actualiza; si el texto viene vacío borra la traducción (vuelve al texto original)
CREATE OR ALTER PROCEDURE dbo.SP_Translations_Guardar
    @id_language     INT,
    @label_key       NVARCHAR(200),
    @translated_text NVARCHAR(1000)
AS
BEGIN
    DECLARE @id_label INT = (SELECT id_label FROM dbo.labels WHERE label_key = @label_key);
    IF @id_label IS NULL RETURN;

    IF LTRIM(RTRIM(ISNULL(@translated_text, ''))) = ''
        DELETE FROM dbo.translations WHERE id_language = @id_language AND id_label = @id_label;
    ELSE IF EXISTS (SELECT 1 FROM dbo.translations WHERE id_language = @id_language AND id_label = @id_label)
        UPDATE dbo.translations SET translated_text = @translated_text WHERE id_language = @id_language AND id_label = @id_label;
    ELSE
        INSERT INTO dbo.translations (id_language, id_label, translated_text) VALUES (@id_language, @id_label, @translated_text);
END
GO

/* =====================================================================================
   6. DATOS INICIALES
   ===================================================================================== */

-- 6.1 Permisos simples
MERGE dbo.permissions AS destino
USING (VALUES
    (N'USR_ALTA',                 N'Alta de usuarios',                        'SIMPLE'),
    (N'USR_MODIFICAR',            N'Modificación de usuarios',                'SIMPLE'),
    (N'USR_ESTADO',               N'Habilitar / deshabilitar usuarios',       'SIMPLE'),
    (N'USR_LISTAR',               N'Ver lista de usuarios',                   'SIMPLE'),
    (N'BIT_VER',                  N'Ver bitácora de actividades',             'SIMPLE'),
    (N'SEG_PERMISOS_GESTIONAR',   N'Administrar permisos, roles y asignaciones', 'SIMPLE'),
    (N'SEG_IDIOMAS_GESTIONAR',    N'Administrar idiomas y leyendas',          'SIMPLE'),
    (N'SEG_INTEGRIDAD_RESTAURAR', N'Verificar y restaurar dígitos verificadores', 'SIMPLE'),
    -- compuestos
    (N'GRP_USUARIOS',             N'Gestión de usuarios (grupo)',             'COMPOSITE'),
    (N'GRP_SEGURIDAD',            N'Seguridad (grupo)',                       'COMPOSITE'),
    (N'GRP_ADMIN_TOTAL',          N'Administración total (grupo de grupos)',  'COMPOSITE')
) AS origen (permission_name, description, permission_type)
ON destino.permission_name = origen.permission_name
WHEN NOT MATCHED THEN
    INSERT (permission_name, description, permission_type) VALUES (origen.permission_name, origen.description, origen.permission_type);
GO

-- 6.2 Árbol: GRP_ADMIN_TOTAL ⊃ { GRP_USUARIOS ⊃ {USR_*}, GRP_SEGURIDAD ⊃ {SEG_*}, BIT_VER }
INSERT INTO dbo.permission_composition (id_composite_permission, id_child_permission)
SELECT padre.id_permission, hijo.id_permission
FROM (VALUES
    (N'GRP_USUARIOS', N'USR_ALTA'), (N'GRP_USUARIOS', N'USR_MODIFICAR'), (N'GRP_USUARIOS', N'USR_ESTADO'), (N'GRP_USUARIOS', N'USR_LISTAR'),
    (N'GRP_SEGURIDAD', N'SEG_PERMISOS_GESTIONAR'), (N'GRP_SEGURIDAD', N'SEG_IDIOMAS_GESTIONAR'), (N'GRP_SEGURIDAD', N'SEG_INTEGRIDAD_RESTAURAR'),
    (N'GRP_ADMIN_TOTAL', N'GRP_USUARIOS'), (N'GRP_ADMIN_TOTAL', N'GRP_SEGURIDAD'), (N'GRP_ADMIN_TOTAL', N'BIT_VER')
) AS arbol (padre, hijo)
INNER JOIN dbo.permissions padre ON padre.permission_name = arbol.padre
INNER JOIN dbo.permissions hijo  ON hijo.permission_name  = arbol.hijo
WHERE NOT EXISTS (SELECT 1 FROM dbo.permission_composition pc
                  WHERE pc.id_composite_permission = padre.id_permission AND pc.id_child_permission = hijo.id_permission);
GO

-- 6.3 Rol Administrador con el grupo total
IF NOT EXISTS (SELECT 1 FROM dbo.roles WHERE role_name = N'Administrador')
    INSERT INTO dbo.roles (role_name, description) VALUES (N'Administrador', N'Acceso total al sistema');
GO

INSERT INTO dbo.role_permissions (id_role, id_permission)
SELECT r.id_role, p.id_permission
FROM dbo.roles r CROSS JOIN dbo.permissions p
WHERE r.role_name = N'Administrador' AND p.permission_name = N'GRP_ADMIN_TOTAL'
  AND NOT EXISTS (SELECT 1 FROM dbo.role_permissions rp WHERE rp.id_role = r.id_role AND rp.id_permission = p.id_permission);
GO

-- 6.4 El primer usuario creado pasa a ser Administrador (si todavía nadie tiene ese rol).
--     Para elegir otro usuario, cambiar el WHERE, por ejemplo: WHERE u.username = 'ciro'
INSERT INTO dbo.user_roles (id_user, id_role)
SELECT TOP 1 u.id_user, r.id_role
FROM dbo.usersaccounts u CROSS JOIN dbo.roles r
WHERE r.role_name = N'Administrador'
  AND NOT EXISTS (SELECT 1 FROM dbo.user_roles ur WHERE ur.id_role = r.id_role)
ORDER BY u.id_user;
GO

-- 6.5 Idiomas
IF NOT EXISTS (SELECT 1 FROM dbo.languages WHERE is_default = 1)
    INSERT INTO dbo.languages (language_name, culture_code, is_default) VALUES (N'Español', 'es-AR', 1);
IF NOT EXISTS (SELECT 1 FROM dbo.languages WHERE language_name = N'English')
    INSERT INTO dbo.languages (language_name, culture_code, is_default) VALUES (N'English', 'en-US', 0);
GO

-- 6.6 Leyendas (clave = Formulario.Control) con su texto en español
MERGE dbo.labels AS destino
USING (VALUES
    (N'FrmActivityLogList.lblTitulo', N'LISTA DE ACTIVIDADES'),
    (N'FrmActivityLogList.chkFiltrarFecha', N'Filtrar por fecha'),
    (N'FrmActivityLogList.lblDesde', N'Desde'),
    (N'FrmActivityLogList.lblHasta', N'Hasta'),
    (N'FrmActivityLogList.MsgRangoInvalido', N'La fecha Desde no puede ser posterior a Hasta.'),
    (N'FrmIdiomas.MsgConfirmarBorrado', N'¿Eliminar el idioma ''{0}'' y todas sus traducciones?'),
    (N'FrmIdiomas.MsgGuardado', N'Traducciones guardadas.'),
    (N'FrmIdiomas._Titulo', N'IDIOMAS'),
    (N'FrmIdiomas.btnAgregarIdioma', N'Agregar idioma'),
    (N'FrmIdiomas.btnCerrar', N'Cerrar'),
    (N'FrmIdiomas.btnEliminarIdioma', N'Eliminar idioma'),
    (N'FrmIdiomas.btnGuardarTraducciones', N'Guardar traducciones'),
    (N'FrmIdiomas.btnUsarIdioma', N'Usar este idioma'),
    (N'FrmIdiomas.colClave', N'Clave'),
    (N'FrmIdiomas.colOriginal', N'Texto original'),
    (N'FrmIdiomas.colTraduccion', N'Traducción'),
    (N'FrmIdiomas.grpIdiomas', N'Idiomas'),
    (N'FrmIdiomas.grpTraducciones', N'Leyendas del idioma seleccionado'),
    (N'FrmIdiomas.lblAyudaTraduccion', N'Idioma por defecto: si la traducción queda vacía se usa el texto original.'),
    (N'FrmIdiomas.lblCodigoIdioma', N'Código'),
    (N'FrmIdiomas.lblFiltro', N'Buscar:'),
    (N'FrmIdiomas.lblNombreIdioma', N'Nombre'),
    (N'FrmIdiomas.lblNuevoIdioma', N'Nuevo idioma'),
    (N'FrmIdiomas.lblTitulo', N'IDIOMAS Y LEYENDAS'),
    (N'FrmIntegridad.MsgCantidad', N'{0} inconsistencia(s) en: {1}'),
    (N'FrmIntegridad.MsgConfirmar', N'Se recalcularán los dígitos verificadores de las tablas con error y el contenido ACTUAL quedará como válido. Hágalo sólo después de revisar o restaurar los datos. ¿Continuar?'),
    (N'FrmIntegridad.MsgIntegra', N'La base de datos está íntegra. Puede continuar.'),
    (N'FrmIntegridad.MsgSinPermiso', N'Usuario o contraseña incorrectos, o el usuario no tiene el permiso para restaurar la integridad.'),
    (N'FrmIntegridad.TipoDVH', N'DVH (registro alterado)'),
    (N'FrmIntegridad.TipoDVV', N'DVV (registros agregados/eliminados)'),
    (N'FrmIntegridad.TipoDVVInexistente', N'DVV inexistente'),
    (N'FrmIntegridad._Titulo', N'VERIFICACIÓN DE INTEGRIDAD'),
    (N'FrmIntegridad.btnRecalcular', N'Recalcular dígitos verificadores'),
    (N'FrmIntegridad.btnReverificar', N'Volver a verificar'),
    (N'FrmIntegridad.btnSalir', N'Salir'),
    (N'FrmIntegridad.colDetalle', N'Detalle'),
    (N'FrmIntegridad.colRegistro', N'Registro'),
    (N'FrmIntegridad.colTabla', N'Tabla'),
    (N'FrmIntegridad.colTipo', N'Tipo de error'),
    (N'FrmIntegridad.grpAdministrador', N'Restauración (sólo administrador)'),
    (N'FrmIntegridad.lblAyuda', N'Opciones: 1) Restaurar el último backup de la base (SQL Server) y volver a verificar. 2) Corregir a mano los registros informados y volver a verificar. 3) Si los cambios son legítimos, recalcular los dígitos verificadores (el contenido actual queda como válido).'),
    (N'FrmIntegridad.lblMensaje', N'Los dígitos verificadores detectaron datos modificados, agregados o eliminados por fuera del sistema. Informe al administrador. No se permite el ingreso hasta resolverlo.'),
    (N'FrmIntegridad.lblPassword', N'Contraseña'),
    (N'FrmIntegridad.lblTitulo', N'ERROR DE INTEGRIDAD EN LA BASE DE DATOS'),
    (N'FrmIntegridad.lblUsuario', N'Usuario'),
    (N'FrmLogin.MsgCompletarCampos', N'Debe completar los campos.'),
    (N'FrmLogin.MsgCredencialesIncorrectas', N'Credenciales incorrectas'),
    (N'FrmLogin.MsgNoIniciaSesion', N'No fue posible iniciar la sesión.'),
    (N'FrmLogin._Titulo', N'LOGIN'),
    (N'FrmLogin.btnLogin', N'Iniciar sesión'),
    (N'FrmLogin.llblCambiarPass', N'¿Olvidó su contraseña?'),
    (N'FrmLogin.llblRegister', N'¿No posee un usuario? Regístrese'),
    (N'FrmMain.MsgIntegridadOk', N'La base de datos está íntegra.'),
    (N'FrmMain._Titulo', N'PRINCIPAL'),
    (N'FrmMain.administraciónToolStripMenuItem', N'Administración'),
    (N'FrmMain.altaToolStripMenuItem', N'Agregar nuevo'),
    (N'FrmMain.archivoToolStripMenuItem', N'Archivo'),
    (N'FrmMain.bitácoraToolStripMenuItem', N'Bitácora'),
    (N'FrmMain.cambiarContraseñaToolStripMenuItem', N'Cambiar contraseña'),
    (N'FrmMain.cambioestadoToolStripMenuItem', N'Cambiar estado'),
    (N'FrmMain.cuentasDeUsuarioToolStripMenuItem', N'Usuarios'),
    (N'FrmMain.idiomaToolStripMenuItem', N'Idioma'),
    (N'FrmMain.idiomasToolStripMenuItem', N'Idiomas y leyendas'),
    (N'FrmMain.integridadToolStripMenuItem', N'Verificar integridad de la BD'),
    (N'FrmMain.modificacionToolStripMenuItem', N'Modificación'),
    (N'FrmMain.modificarDatosToolStripMenuItem', N'Modificar datos'),
    (N'FrmMain.perfilToolStripMenuItem', N'Perfil'),
    (N'FrmMain.permisosToolStripMenuItem', N'Permisos y roles'),
    (N'FrmMain.salirToolStripMenuItem', N'Salir'),
    (N'FrmMain.seguridadToolStripMenuItem', N'Seguridad'),
    (N'FrmMain.toolStripStatusLabel1', N'USUARIO ACTIVO:'),
    (N'FrmMain.verActividadesToolStripMenuItem', N'Ver actividades'),
    (N'FrmMain.verInformaciónToolStripMenuItem', N'Ver información'),
    (N'FrmMain.verListaCompletaToolStripMenuItem', N'Ver lista completa'),
    (N'FrmPermisos.MsgConfirmarBorrado', N'¿Eliminar el permiso ''{0}''? Se quitará de todos los grupos y roles.'),
    (N'FrmPermisos.MsgConfirmarBorradoRol', N'¿Eliminar el rol ''{0}''? Se quitará a todos los usuarios.'),
    (N'FrmPermisos.MsgNoGuardado', N'No se pudo guardar en la base de datos.'),
    (N'FrmPermisos.MsgPermisoExiste', N'Ya existe un permiso con ese nombre.'),
    (N'FrmPermisos.MsgRolExiste', N'Ya existe un rol con ese nombre.'),
    (N'FrmPermisos.MsgSeleccioneCompuesto', N'Seleccione en el árbol un permiso compuesto (en azul).'),
    (N'FrmPermisos.MsgSeleccioneHijo', N'Seleccione el permiso a agregar.'),
    (N'FrmPermisos.MsgSeleccioneNodoHijo', N'Seleccione un permiso que esté DENTRO de un grupo.'),
    (N'FrmPermisos.MsgSeleccionePermiso', N'Seleccione un permiso del árbol.'),
    (N'FrmPermisos.MsgSeleccionePermisoRol', N'Seleccione un permiso del rol.'),
    (N'FrmPermisos.MsgSeleccioneRol', N'Seleccione un rol.'),
    (N'FrmPermisos.MsgSeleccioneRolUsuario', N'Seleccione el rol del usuario a quitar.'),
    (N'FrmPermisos.MsgSeleccioneUsuarioRol', N'Seleccione un usuario y un rol.'),
    (N'FrmPermisos._Titulo', N'ADMINISTRACIÓN DE PERMISOS'),
    (N'FrmPermisos.btnAgregarHijo', N'Agregar al grupo'),
    (N'FrmPermisos.btnAsignarPermisoARol', N'Asignar permiso seleccionado en el árbol'),
    (N'FrmPermisos.btnAsignarRol', N'Asignar rol'),
    (N'FrmPermisos.btnCerrar', N'Cerrar'),
    (N'FrmPermisos.btnCrearPermiso', N'Crear permiso'),
    (N'FrmPermisos.btnCrearRol', N'Crear rol'),
    (N'FrmPermisos.btnEliminarPermiso', N'Eliminar seleccionado'),
    (N'FrmPermisos.btnEliminarRol', N'Eliminar rol'),
    (N'FrmPermisos.btnQuitarHijo', N'Quitar del grupo'),
    (N'FrmPermisos.btnQuitarPermisoDeRol', N'Quitar permiso del rol'),
    (N'FrmPermisos.btnQuitarRol', N'Quitar rol seleccionado'),
    (N'FrmPermisos.chkAgregarAlSeleccionado', N'Agregar dentro del grupo seleccionado'),
    (N'FrmPermisos.grpPermisos', N'Árbol de permisos'),
    (N'FrmPermisos.grpRoles', N'Roles'),
    (N'FrmPermisos.grpUsuarios', N'Usuarios'),
    (N'FrmPermisos.lblDescripcionPermiso', N'Descripción'),
    (N'FrmPermisos.lblDescripcionRol', N'Descripción'),
    (N'FrmPermisos.lblNombrePermiso', N'Nombre'),
    (N'FrmPermisos.lblNombreRol', N'Nombre'),
    (N'FrmPermisos.lblPermisoHijo', N'Permiso a agregar al grupo seleccionado:'),
    (N'FrmPermisos.lblPermisosRol', N'Permisos del rol seleccionado:'),
    (N'FrmPermisos.lblRolAsignar', N'Rol'),
    (N'FrmPermisos.lblRolesUsuario', N'Roles y permisos efectivos:'),
    (N'FrmPermisos.lblTitulo', N'PERMISOS, ROLES Y USUARIOS (COMPOSITE)'),
    (N'FrmPermisos.lblUsuario', N'Usuario'),
    (N'FrmPermisos.rbCompuesto', N'Compuesto (grupo)'),
    (N'FrmPermisos.rbSimple', N'Simple'),
    (N'FrmProfileEdit._Titulo', N'PERFIL - Modificación de datos'),
    (N'FrmProfileEdit.btnCancelar', N'Cancelar'),
    (N'FrmProfileEdit.btnGuardar', N'Guardar'),
    (N'FrmProfileEdit.lblEmailOpt1', N'Email 1:'),
    (N'FrmProfileEdit.lblEmailOpt2', N'Email 2:'),
    (N'FrmProfileEdit.lblFirstName', N'Nombre/s:'),
    (N'FrmProfileEdit.lblLastname', N'Apellido/s:'),
    (N'FrmProfileInfo._Titulo', N'PERFIL - Información'),
    (N'FrmProfileInfo.btnCerrar', N'Cerrar'),
    (N'FrmProfileInfo.lblEmailOpt1', N'Email 1:'),
    (N'FrmProfileInfo.lblEmailOpt2', N'Email 2:'),
    (N'FrmProfileInfo.lblFirstName', N'Nombre/s:'),
    (N'FrmProfileInfo.lblLastname', N'Apellido/s:'),
    (N'FrmProfileInfo.lblUsername', N'Username:'),
    (N'FrmProfilePassword._Titulo', N'PERFIL - Cambiar contraseña'),
    (N'FrmProfilePassword.btnCancelar', N'Cancelar'),
    (N'FrmProfilePassword.btnGuardar', N'Guardar'),
    (N'FrmProfilePassword.label1', N'* Debe utilizar al menos una mayúscula, símbolos y número para mayor la seguridad'),
    (N'FrmProfilePassword.lblEmailOpt1', N'Repita la nueva contraseña:'),
    (N'FrmProfilePassword.lblFirstName', N'Ingrese la actual contraseña:'),
    (N'FrmProfilePassword.lblLastname', N'Ingrese la nueva contraseña:'),
    (N'FrmProfilePassword.lblValidacionActualPass', N'* validación'),
    (N'FrmProfilePassword.lblValidacionNewPass1', N'* validación'),
    (N'FrmProfilePassword.lblValidacionNewPass2', N'* validación'),
    (N'FrmRegister._Titulo', N'REGISTRO DE USUARIO'),
    (N'FrmRegister.btnCancel', N'Cancelar'),
    (N'FrmRegister.btnCreateUser', N'Crear usuario'),
    (N'FrmRegister.label1', N'* campos obligatorios'),
    (N'FrmRegister.lblEmailOpt1', N'Email 1 *'),
    (N'FrmRegister.lblEmailOpt2', N'Email 2'),
    (N'FrmRegister.lblFirstName', N'Nombre *'),
    (N'FrmRegister.lblLastname', N'Apellido *'),
    (N'FrmRegister.lblPassword', N'Password *'),
    (N'FrmRegister.lblUsername', N'Username *'),
    (N'FrmUsersCreate.btnCreateUser', N'Crear usuario'),
    (N'FrmUsersCreate.label1', N'* campos obligatorios'),
    (N'FrmUsersCreate.label2', N'ALTA DE USUARIO'),
    (N'FrmUsersCreate.lblEmailOpt1', N'Email 1 *'),
    (N'FrmUsersCreate.lblEmailOpt2', N'Email 2'),
    (N'FrmUsersCreate.lblFirstName', N'Nombre *'),
    (N'FrmUsersCreate.lblLastname', N'Apellido *'),
    (N'FrmUsersCreate.lblPassword', N'Password *'),
    (N'FrmUsersCreate.lblUsername', N'Username *'),
    (N'FrmUsersEdit.btnConfirmChanges', N'Confirmar cambios'),
    (N'FrmUsersEdit.label1', N'Password'),
    (N'FrmUsersEdit.lblEmailOpt1', N'Email'),
    (N'FrmUsersEdit.lblEmailOpt2', N'Email opcional'),
    (N'FrmUsersEdit.lblFirstname', N'Nombre/s'),
    (N'FrmUsersEdit.lblFormTitle', N'MODIFICACIÓN DE USUARIOS'),
    (N'FrmUsersEdit.lblLastname', N'Apellido/s'),
    (N'FrmUsersEdit.lblSearch', N'Buscar:'),
    (N'FrmUsersEdit.lblState', N'Estado'),
    (N'FrmUsersEdit.lblUsername', N'Username'),
    (N'FrmUsersEdit.lblValNewEmailOpt1', N'* obligatorio'),
    (N'FrmUsersEdit.lblValNewEmailOpt2', N'* opcional'),
    (N'FrmUsersEdit.lblValNewFirstname', N'* obligatorio'),
    (N'FrmUsersEdit.lblValNewLastname', N'* obligatorio'),
    (N'FrmUsersEdit.lblValNewPass', N'* obligatorio'),
    (N'FrmUsersEdit.lblValNewUsername', N'* obligatorio'),
    (N'FrmUsersList.lblFormTitle', N'LISTA COMPLETA DE USUARIOS'),
    (N'FrmUsersState.btnDeshabilitar', N'Deshabilitar'),
    (N'FrmUsersState.btnHabilitar', N'Habilitar'),
    (N'FrmUsersState.label2', N'ESTADOS DE USUARIOS'),
    (N'FrmUsersState.lblFiltros', N'Filtrar por:')
) AS origen (label_key, default_text)
ON destino.label_key = origen.label_key
WHEN NOT MATCHED THEN INSERT (label_key, default_text) VALUES (origen.label_key, origen.default_text);
GO

-- 6.7 Traducciones al inglés
INSERT INTO dbo.translations (id_language, id_label, translated_text)
SELECT lang.id_language, lbl.id_label, en.texto
FROM (VALUES
    (N'FrmActivityLogList.lblTitulo', N'ACTIVITY LIST'),
    (N'FrmActivityLogList.chkFiltrarFecha', N'Filter by date'),
    (N'FrmActivityLogList.lblDesde', N'From'),
    (N'FrmActivityLogList.lblHasta', N'To'),
    (N'FrmActivityLogList.MsgRangoInvalido', N'The From date cannot be later than To.'),
    (N'FrmIdiomas.MsgConfirmarBorrado', N'Delete the language ''{0}'' and all its translations?'),
    (N'FrmIdiomas.MsgGuardado', N'Translations saved.'),
    (N'FrmIdiomas._Titulo', N'LANGUAGES'),
    (N'FrmIdiomas.btnAgregarIdioma', N'Add language'),
    (N'FrmIdiomas.btnCerrar', N'Close'),
    (N'FrmIdiomas.btnEliminarIdioma', N'Delete language'),
    (N'FrmIdiomas.btnGuardarTraducciones', N'Save translations'),
    (N'FrmIdiomas.btnUsarIdioma', N'Use this language'),
    (N'FrmIdiomas.colClave', N'Key'),
    (N'FrmIdiomas.colOriginal', N'Original text'),
    (N'FrmIdiomas.colTraduccion', N'Translation'),
    (N'FrmIdiomas.grpIdiomas', N'Languages'),
    (N'FrmIdiomas.grpTraducciones', N'Captions of the selected language'),
    (N'FrmIdiomas.lblAyudaTraduccion', N'Default language: if the translation is empty the original text is used.'),
    (N'FrmIdiomas.lblCodigoIdioma', N'Code'),
    (N'FrmIdiomas.lblFiltro', N'Search:'),
    (N'FrmIdiomas.lblNombreIdioma', N'Name'),
    (N'FrmIdiomas.lblNuevoIdioma', N'New language'),
    (N'FrmIdiomas.lblTitulo', N'LANGUAGES AND CAPTIONS'),
    (N'FrmIntegridad.MsgCantidad', N'{0} inconsistency(ies) in: {1}'),
    (N'FrmIntegridad.MsgConfirmar', N'Check digits of the tables with errors will be recalculated and the CURRENT content will become valid. Do it only after reviewing or restoring the data. Continue?'),
    (N'FrmIntegridad.MsgIntegra', N'The database is consistent. You may continue.'),
    (N'FrmIntegridad.MsgSinPermiso', N'Wrong username or password, or the user is not allowed to restore integrity.'),
    (N'FrmIntegridad.TipoDVH', N'Row check digit (row altered)'),
    (N'FrmIntegridad.TipoDVV', N'Column check digit (rows added/removed)'),
    (N'FrmIntegridad.TipoDVVInexistente', N'Column check digit missing'),
    (N'FrmIntegridad._Titulo', N'INTEGRITY CHECK'),
    (N'FrmIntegridad.btnRecalcular', N'Recalculate check digits'),
    (N'FrmIntegridad.btnReverificar', N'Check again'),
    (N'FrmIntegridad.btnSalir', N'Exit'),
    (N'FrmIntegridad.colDetalle', N'Detail'),
    (N'FrmIntegridad.colRegistro', N'Row'),
    (N'FrmIntegridad.colTabla', N'Table'),
    (N'FrmIntegridad.colTipo', N'Error type'),
    (N'FrmIntegridad.grpAdministrador', N'Restore (administrator only)'),
    (N'FrmIntegridad.lblAyuda', N'Options: 1) Restore the last database backup (SQL Server) and check again. 2) Fix the reported rows manually and check again. 3) If the changes are legitimate, recalculate the check digits (current content becomes valid).'),
    (N'FrmIntegridad.lblMensaje', N'The check digits detected data modified, added or deleted outside the system. Notify the administrator. Access is not allowed until it is solved.'),
    (N'FrmIntegridad.lblPassword', N'Password'),
    (N'FrmIntegridad.lblTitulo', N'DATABASE INTEGRITY ERROR'),
    (N'FrmIntegridad.lblUsuario', N'Username'),
    (N'FrmLogin.MsgCompletarCampos', N'You must fill in all fields.'),
    (N'FrmLogin.MsgCredencialesIncorrectas', N'Wrong credentials'),
    (N'FrmLogin.MsgNoIniciaSesion', N'Could not log in.'),
    (N'FrmLogin._Titulo', N'LOGIN'),
    (N'FrmLogin.btnLogin', N'Log in'),
    (N'FrmLogin.llblCambiarPass', N'Forgot your password?'),
    (N'FrmLogin.llblRegister', N'Don''t have a user? Sign up'),
    (N'FrmMain.MsgIntegridadOk', N'The database is consistent.'),
    (N'FrmMain._Titulo', N'MAIN'),
    (N'FrmMain.administraciónToolStripMenuItem', N'Administration'),
    (N'FrmMain.altaToolStripMenuItem', N'Add new'),
    (N'FrmMain.archivoToolStripMenuItem', N'File'),
    (N'FrmMain.bitácoraToolStripMenuItem', N'Activity log'),
    (N'FrmMain.cambiarContraseñaToolStripMenuItem', N'Change password'),
    (N'FrmMain.cambioestadoToolStripMenuItem', N'Change status'),
    (N'FrmMain.cuentasDeUsuarioToolStripMenuItem', N'Users'),
    (N'FrmMain.idiomaToolStripMenuItem', N'Language'),
    (N'FrmMain.idiomasToolStripMenuItem', N'Languages and captions'),
    (N'FrmMain.integridadToolStripMenuItem', N'Check database integrity'),
    (N'FrmMain.modificacionToolStripMenuItem', N'Edit'),
    (N'FrmMain.modificarDatosToolStripMenuItem', N'Edit details'),
    (N'FrmMain.perfilToolStripMenuItem', N'Profile'),
    (N'FrmMain.permisosToolStripMenuItem', N'Permissions and roles'),
    (N'FrmMain.salirToolStripMenuItem', N'Exit'),
    (N'FrmMain.seguridadToolStripMenuItem', N'Security'),
    (N'FrmMain.toolStripStatusLabel1', N'ACTIVE USER:'),
    (N'FrmMain.verActividadesToolStripMenuItem', N'View activities'),
    (N'FrmMain.verInformaciónToolStripMenuItem', N'View information'),
    (N'FrmMain.verListaCompletaToolStripMenuItem', N'View full list'),
    (N'FrmPermisos.MsgConfirmarBorrado', N'Delete the permission ''{0}''? It will be removed from every group and role.'),
    (N'FrmPermisos.MsgConfirmarBorradoRol', N'Delete the role ''{0}''? It will be removed from every user.'),
    (N'FrmPermisos.MsgNoGuardado', N'Could not save to the database.'),
    (N'FrmPermisos.MsgPermisoExiste', N'A permission with that name already exists.'),
    (N'FrmPermisos.MsgRolExiste', N'A role with that name already exists.'),
    (N'FrmPermisos.MsgSeleccioneCompuesto', N'Select a composite permission (in blue) in the tree.'),
    (N'FrmPermisos.MsgSeleccioneHijo', N'Select the permission to add.'),
    (N'FrmPermisos.MsgSeleccioneNodoHijo', N'Select a permission that is INSIDE a group.'),
    (N'FrmPermisos.MsgSeleccionePermiso', N'Select a permission in the tree.'),
    (N'FrmPermisos.MsgSeleccionePermisoRol', N'Select a permission of the role.'),
    (N'FrmPermisos.MsgSeleccioneRol', N'Select a role.'),
    (N'FrmPermisos.MsgSeleccioneRolUsuario', N'Select the user''s role to remove.'),
    (N'FrmPermisos.MsgSeleccioneUsuarioRol', N'Select a user and a role.'),
    (N'FrmPermisos._Titulo', N'PERMISSION MANAGEMENT'),
    (N'FrmPermisos.btnAgregarHijo', N'Add to group'),
    (N'FrmPermisos.btnAsignarPermisoARol', N'Assign permission selected in the tree'),
    (N'FrmPermisos.btnAsignarRol', N'Assign role'),
    (N'FrmPermisos.btnCerrar', N'Close'),
    (N'FrmPermisos.btnCrearPermiso', N'Create permission'),
    (N'FrmPermisos.btnCrearRol', N'Create role'),
    (N'FrmPermisos.btnEliminarPermiso', N'Delete selected'),
    (N'FrmPermisos.btnEliminarRol', N'Delete role'),
    (N'FrmPermisos.btnQuitarHijo', N'Remove from group'),
    (N'FrmPermisos.btnQuitarPermisoDeRol', N'Remove permission from role'),
    (N'FrmPermisos.btnQuitarRol', N'Remove selected role'),
    (N'FrmPermisos.chkAgregarAlSeleccionado', N'Add inside the selected group'),
    (N'FrmPermisos.grpPermisos', N'Permission tree'),
    (N'FrmPermisos.grpRoles', N'Roles'),
    (N'FrmPermisos.grpUsuarios', N'Users'),
    (N'FrmPermisos.lblDescripcionPermiso', N'Description'),
    (N'FrmPermisos.lblDescripcionRol', N'Description'),
    (N'FrmPermisos.lblNombrePermiso', N'Name'),
    (N'FrmPermisos.lblNombreRol', N'Name'),
    (N'FrmPermisos.lblPermisoHijo', N'Permission to add to the selected group:'),
    (N'FrmPermisos.lblPermisosRol', N'Permissions of the selected role:'),
    (N'FrmPermisos.lblRolAsignar', N'Role'),
    (N'FrmPermisos.lblRolesUsuario', N'Roles and effective permissions:'),
    (N'FrmPermisos.lblTitulo', N'PERMISSIONS, ROLES AND USERS (COMPOSITE)'),
    (N'FrmPermisos.lblUsuario', N'User'),
    (N'FrmPermisos.rbCompuesto', N'Composite (group)'),
    (N'FrmPermisos.rbSimple', N'Simple'),
    (N'FrmProfileEdit._Titulo', N'PROFILE - Edit details'),
    (N'FrmProfileEdit.btnCancelar', N'Cancel'),
    (N'FrmProfileEdit.btnGuardar', N'Save'),
    (N'FrmProfileEdit.lblEmailOpt1', N'Email 1:'),
    (N'FrmProfileEdit.lblEmailOpt2', N'Email 2:'),
    (N'FrmProfileEdit.lblFirstName', N'First name(s):'),
    (N'FrmProfileEdit.lblLastname', N'Last name(s):'),
    (N'FrmProfileInfo._Titulo', N'PROFILE - Information'),
    (N'FrmProfileInfo.btnCerrar', N'Close'),
    (N'FrmProfileInfo.lblEmailOpt1', N'Email 1:'),
    (N'FrmProfileInfo.lblEmailOpt2', N'Email 2:'),
    (N'FrmProfileInfo.lblFirstName', N'First name(s):'),
    (N'FrmProfileInfo.lblLastname', N'Last name(s):'),
    (N'FrmProfileInfo.lblUsername', N'Username:'),
    (N'FrmProfilePassword._Titulo', N'PROFILE - Change password'),
    (N'FrmProfilePassword.btnCancelar', N'Cancel'),
    (N'FrmProfilePassword.btnGuardar', N'Save'),
    (N'FrmProfilePassword.label1', N'* Use at least one uppercase letter, symbols and numbers for better security'),
    (N'FrmProfilePassword.lblEmailOpt1', N'Repeat the new password:'),
    (N'FrmProfilePassword.lblFirstName', N'Enter the current password:'),
    (N'FrmProfilePassword.lblLastname', N'Enter the new password:'),
    (N'FrmProfilePassword.lblValidacionActualPass', N'* validation'),
    (N'FrmProfilePassword.lblValidacionNewPass1', N'* validation'),
    (N'FrmProfilePassword.lblValidacionNewPass2', N'* validation'),
    (N'FrmRegister._Titulo', N'USER SIGN UP'),
    (N'FrmRegister.btnCancel', N'Cancel'),
    (N'FrmRegister.btnCreateUser', N'Create user'),
    (N'FrmRegister.label1', N'* required fields'),
    (N'FrmRegister.lblEmailOpt1', N'Email 1 *'),
    (N'FrmRegister.lblEmailOpt2', N'Email 2'),
    (N'FrmRegister.lblFirstName', N'First name *'),
    (N'FrmRegister.lblLastname', N'Last name *'),
    (N'FrmRegister.lblPassword', N'Password *'),
    (N'FrmRegister.lblUsername', N'Username *'),
    (N'FrmUsersCreate.btnCreateUser', N'Create user'),
    (N'FrmUsersCreate.label1', N'* required fields'),
    (N'FrmUsersCreate.label2', N'NEW USER'),
    (N'FrmUsersCreate.lblEmailOpt1', N'Email 1 *'),
    (N'FrmUsersCreate.lblEmailOpt2', N'Email 2'),
    (N'FrmUsersCreate.lblFirstName', N'First name *'),
    (N'FrmUsersCreate.lblLastname', N'Last name *'),
    (N'FrmUsersCreate.lblPassword', N'Password *'),
    (N'FrmUsersCreate.lblUsername', N'Username *'),
    (N'FrmUsersEdit.btnConfirmChanges', N'Confirm changes'),
    (N'FrmUsersEdit.label1', N'Password'),
    (N'FrmUsersEdit.lblEmailOpt1', N'Email'),
    (N'FrmUsersEdit.lblEmailOpt2', N'Optional email'),
    (N'FrmUsersEdit.lblFirstname', N'First name(s)'),
    (N'FrmUsersEdit.lblFormTitle', N'EDIT USERS'),
    (N'FrmUsersEdit.lblLastname', N'Last name(s)'),
    (N'FrmUsersEdit.lblSearch', N'Search:'),
    (N'FrmUsersEdit.lblState', N'Status'),
    (N'FrmUsersEdit.lblUsername', N'Username'),
    (N'FrmUsersEdit.lblValNewEmailOpt1', N'* required'),
    (N'FrmUsersEdit.lblValNewEmailOpt2', N'* optional'),
    (N'FrmUsersEdit.lblValNewFirstname', N'* required'),
    (N'FrmUsersEdit.lblValNewLastname', N'* required'),
    (N'FrmUsersEdit.lblValNewPass', N'* required'),
    (N'FrmUsersEdit.lblValNewUsername', N'* required'),
    (N'FrmUsersList.lblFormTitle', N'FULL USER LIST'),
    (N'FrmUsersState.btnDeshabilitar', N'Disable'),
    (N'FrmUsersState.btnHabilitar', N'Enable'),
    (N'FrmUsersState.label2', N'USER STATUS'),
    (N'FrmUsersState.lblFiltros', N'Filter by:')
) AS en (label_key, texto)
INNER JOIN dbo.labels lbl     ON lbl.label_key = en.label_key
INNER JOIN dbo.languages lang ON lang.language_name = N'English'
WHERE NOT EXISTS (SELECT 1 FROM dbo.translations t WHERE t.id_language = lang.id_language AND t.id_label = lbl.id_label);
GO

PRINT 'Script ejecutado correctamente. Al iniciar la aplicación, recalcular los dígitos verificadores desde la pantalla de integridad.';
GO
