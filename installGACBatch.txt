@echo off
setlocal
Echo changing path to SDK Tools
cd "C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools"
Echo Installing dll in GAC
GACUTIL /i "C:\Users\fkhalil\Documents\Visual Studio 2015\Projects\E33FKHALILHW0\SecureCommunicationComponent\bin\Debug\SecureCommunicationComponent.dll"
if %ERRORLEVEL% neq 0 goto ERR


