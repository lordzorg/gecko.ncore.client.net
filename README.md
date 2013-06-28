# Gecko.Ncore.Client

This solution contains the projects that compose the client proxies used to work 
with the ephorte® nCore Integration Services.

00 - IMPORTANT
	You are reading it

01 - .NET 3.5
	This is the .NET 3.5 edition of the contracts. All source files are linked
	to their respective locations in the .NET 4.5 structure. If there are
	code changes neede for specific frameworks, these changes can be handled
	with #ifdef directives or ConditionalAttribute decorations.

02 - .NET 4.0
	This is the .NET 4.0 edition of the contracts. All source files are linked
	to their respective locations in the .NET 4.5 structure. If there are
	code changes needed for specific frameworks, these changes can be handled
	with #ifdef directives or ConditionalAttribute decorations.

03 - .NET 4.5
	This is the .NET 4.5 edition of the contracts. If there are
	code changes needed for specific frameworks, these changes can be handled
	with #ifdef directives or ConditionalAttribute decorations.
	
04 - NUSPEC
	This structure represents the packages that are handled by the 
	nuget.msbuild file. The packages generated consist of the dlls from
	.net 3.5, 4.0 and 4.5.

	The result contains a release package for use in other dependent projects
	as well as symbol packages for debugging assitance.

	Release packages are deployed to http://www.myget.org/F/geckopublic/api/v2/package
	Symbol packages are deployed to http://nuget.gw.symbolsource.org/MyGet/geckopublic


When updating proxies do the following:
1. Update the nuget package dependencies
2. Update the affected services references
3. Increase the semver numbers in the respective AssemblyInfo.cs files
4. Publish to Myget
