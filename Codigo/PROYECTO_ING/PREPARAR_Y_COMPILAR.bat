@echo off
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0preparar_y_compilar.ps1"
echo.
echo Listo. Detalle en C:\PROYECTO_ING\_preparar_log.txt
timeout /t 8
