@ECHO OFF
cd /d "%~dp0"
==================
==================
ECHO BUILDING...
======================================
ECHO COMPILE VIEW...
dotnet publish ".\XmlContentTranslator\XmlContentTranslator.csproj" -c Release --framework net8.0-windows --self-contained false --output ./output/XmlContentTranslator
==================



