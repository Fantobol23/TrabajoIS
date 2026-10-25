# Prepara y compila PROYECTO_ING. Deja el detalle en C:\PROYECTO_ING\_preparar_log.txt
$ErrorActionPreference = 'Continue'
$root = 'C:\PROYECTO_ING'
$log  = Join-Path $root '_preparar_log.txt'
"=== $(Get-Date) ===" | Out-File $log -Encoding utf8
function L($m) { $m | Out-File $log -Append -Encoding utf8; Write-Host $m }

# 1) Desbloquear archivos (Mark of the Web) y limpiar bin/obj
L '--- 1. Unblock-File y limpieza'
Get-ChildItem -Path $root -Recurse -File | Unblock-File
Get-ChildItem -Path $root -Directory | ForEach-Object {
    foreach ($d in 'bin','obj') { $p = Join-Path $_.FullName $d; if (Test-Path $p) { Remove-Item -Recurse -Force $p -ErrorAction SilentlyContinue } }
}
L 'OK'

# 2) Instancias de SQL Server
L '--- 2. Servicios SQL Server'
Get-Service | Where-Object { $_.Name -like 'MSSQL*' } | ForEach-Object { L ("{0} {1}" -f $_.Name, $_.Status) }

# 3) Ejecutar el script SQL (separado por GO)
L '--- 3. Script SQL'
$server = 'DESKTOP-DPJ33EP\SQLEXPRESS'
try {
    $cn = New-Object System.Data.SqlClient.SqlConnection "Data Source=$server;Initial Catalog=master;Integrated Security=True;Connect Timeout=10"
    $cn.add_InfoMessage({ param($s,$e) L ("SQL> " + $e.Message) })
    $cn.Open()
    $cmd = $cn.CreateCommand()
    $cmd.CommandText = "SELECT name FROM sys.databases WHERE name = 'PROYECTO_ING_DB'"
    $existe = $cmd.ExecuteScalar()
    L ("Base PROYECTO_ING_DB existe: " + [bool]$existe)
    if ($existe) {
        $sql = Get-Content (Join-Path $root 'BaseDeDatos\Script_DV_Permisos_Idiomas.sql') -Raw -Encoding UTF8
        $batches = [regex]::Split($sql, '(?im)^\s*GO\s*$')
        $n = 0; $err = 0
        foreach ($b in $batches) {
            if ($b.Trim().Length -eq 0) { continue }
            $n++
            $c = $cn.CreateCommand(); $c.CommandText = $b; $c.CommandTimeout = 120
            try { [void]$c.ExecuteNonQuery() } catch { $err++; L ("ERROR en lote $n : " + $_.Exception.Message); L ($b.Substring(0, [Math]::Min(300, $b.Length))) }
        }
        L "Lotes ejecutados: $n  errores: $err"
        $cmd.CommandText = "USE PROYECTO_ING_DB; SELECT (SELECT COUNT(*) FROM usersaccounts) u, (SELECT COUNT(*) FROM permissions) p, (SELECT COUNT(*) FROM user_roles) ur, (SELECT COUNT(*) FROM labels) l, (SELECT COUNT(*) FROM translations) t"
        $r = $cmd.ExecuteReader(); while ($r.Read()) { L ("usuarios={0} permisos={1} user_roles={2} leyendas={3} traducciones={4}" -f $r[0],$r[1],$r[2],$r[3],$r[4]) }; $r.Close()
        $cmd.CommandText = "USE PROYECTO_ING_DB; SELECT u.username, r.role_name FROM user_roles ur JOIN usersaccounts u ON u.id_user = ur.id_user JOIN roles r ON r.id_role = ur.id_role"
        $r = $cmd.ExecuteReader(); while ($r.Read()) { L ("Administrador asignado a: {0} ({1})" -f $r[0],$r[1]) }; $r.Close()
    } else {
        L 'La base PROYECTO_ING_DB no existe en esta instancia.'
        $cmd.CommandText = "SELECT name FROM sys.databases"; $r = $cmd.ExecuteReader(); while ($r.Read()) { L ("  base: " + $r[0]) }; $r.Close()
    }
    $cn.Close()
} catch { L ("ERROR SQL: " + $_.Exception.Message) }

# 4) Compilar con MSBuild
L '--- 4. Compilacion'
$vswhere = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"
$msbuild = $null
if (Test-Path $vswhere) { $msbuild = & $vswhere -latest -prerelease -products * -requires Microsoft.Component.MSBuild -find 'MSBuild\**\Bin\MSBuild.exe' | Select-Object -First 1 }
L ("MSBuild: " + $msbuild)
if ($msbuild) {
    $out = & $msbuild (Join-Path $root 'UI_Presentation\UI_Presentation.csproj') /t:Rebuild /p:Configuration=Debug /nologo /v:minimal 2>&1
    $out | ForEach-Object { L $_ }
    L ("ExitCode: " + $LASTEXITCODE)
    $exe = Join-Path $root 'UI_Presentation\bin\Debug\UI_Presentation.exe'
    L ("EXE generado: " + (Test-Path $exe))
}
L '=== FIN ==='
