# Gecko.Ncore.Client
*This is the .NET version of the NCore client library. If you are looking for the Java version, check out  the [Gecko.NCore.Client.Java](http://github.com/GeckoInformasjonssystemerAS/gecko.ncore.client.java)* 

This is the .NET based client for integrating with ePhorte Integration Services (EIS). It is intended to be a helpful tool for integrating parties to more easily perform the functions that the EIS offers.

## Installation

The library can included as a nuget package from Myget at http://www.myget.org/F/geckopublic/api/v2/package. 

The corresponding symbol files can found at http://nuget.gw.symbolsource.org/MyGet/geckopublic.

We support .NET version 3.5, 4.0 and 4.5.

## The source

This solution contains the projects that compose the client proxies used to work 
with the ephorte® nCore Integration Services.

### 01 - .NET 3.5
This is the .NET 3.5 edition of the contracts. All source files are linked
to their respective locations in the .NET 4.5 structure. If there are
code changes neede for specific frameworks, these changes can be handled
with #ifdef directives or ConditionalAttribute decorations.

### 02 - .NET 4.0
This is the .NET 4.0 edition of the contracts. All source files are linked
to their respective locations in the .NET 4.5 structure. If there are
code changes needed for specific frameworks, these changes can be handled
with #ifdef directives or ConditionalAttribute decorations.

### 03 - .NET 4.5
This is the .NET 4.5 edition of the contracts. If there are
code changes needed for specific frameworks, these changes can be handled
with #ifdef directives or ConditionalAttribute decorations.
    
### 04 - NUSPEC
This structure represents the packages that are handled by the 
nuget.msbuild file. The packages generated consist of the dlls from
.net 3.5, 4.0 and 4.5.

The result contains a release package for use in other dependent projects
as well as symbol packages for debugging assitance.