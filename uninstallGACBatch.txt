@echo off
setlocal
Echo changing path to SDK Tools
cd C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.6 Tools
Echo Un-Installing dll from GAC
GACUTIL /uf SecureCommunicationComponent

