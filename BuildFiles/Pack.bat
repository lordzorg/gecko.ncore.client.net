@echo off
echo ***********************************************************************
echo NOTE:
echo It is expected that this file is started from a developer command prompt

echo Cleaning project
msbuild /verbosity:quiet /p:Configuration=Release /t:Clean ..\Gecko.NCore.Client.sln

echo Starting release build
msbuild /verbosity:quiet /p:Configuration=Release ..\Gecko.NCore.Client.sln

echo ***********************************************************************
msbuild /verbosity:quiet Pack.msbuild
echo Output folder for packages: T:\Projects\NCoreServices\Client\2013.01\nupkg

echo Cleaning project
msbuild /verbosity:quiet /p:Configuration=Release /t:Clean ..\Gecko.NCore.Client.sln