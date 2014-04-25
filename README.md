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

## Updating contract how-to
In this how-to we will give an example of how to update the contract for Gecko.NCore.Client.ObjectModel.V3.En. Everything should work the same for the other projects.

- This assumes that you already have updates the contract nuget packages with your changes.

1. Update all contract nuget packages in the Gecko.NCore.Host project from the private myget repository. Currently configured to be: https://www.myget.org/F/geckoprivate/. 
This project will self-host the wcf endpoints using the included contracts. We can than in turn generate new client proxies based on it. 
Simplest way to do this is to run the following command in the 'Package Manager Console' using the Myget internal package source: update-package -ProjectName gecko.ncore.Host
2. Open the Gecko.NCore.Client.ObjectModel.V3.En project.
3. Expand Service References.
4. Right-click ObjectModel.V3.En and click 'Update service reference' from the dropdown. Do this for all .net versions(3.5, 4.0, 4.5)
This should update the generated client.

Tip: If Visual Studio complain that port 8888 is in use it may be that you have Fidler open. If it isn't Fiddler then consult this article to find which process is the offending one: http://www.techrepublic.com/blog/the-enterprise-cloud/see-what-process-is-using-a-tcp-port-in-windows-server-2008/.

5. Verify that the contract changes are present in the updated client proxy. 
This can typically be done by inspecting the generated 'Reference.cs' file. 
Click 'Show all files' while standing in the project to make it visible below the service reference.
6. Increment version number according to semver.
7. Run all tests!
8. Check in changes.
9. Create and push new nuget packages. See the gecko.ncore.client.net.nuget on github for how to do this.

NB! When updating objectmodel.V3.En update both Ephorte.ServiceModel.Client.ObjectModel.V3.En and Gecko.Ncore.Client.ObjectModel.V3.En
