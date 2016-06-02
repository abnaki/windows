# Abnaki Way Components

Small framework for Windows applications developed in C#, .NET 4.5, Visual Studio 2013.    Provides useful
necessities and conventions.   Follows MVVM as far as MVVM makes design sense.

Users are expected to build Abnaki's source code.  Perhaps reference Abnaki's project file(s) within your solution.  No binaries here.  Not on NuGet.  No keyfiles.

These libraries, however small, save you developer(s) from application-specific code bloat, or consulting the Internet for answers to previously solved problems.  Any code necessary to bootstrap the creation of new applications is boiled down to the essence.  Improvements in these components will benefit all applications developed upon them.   

The structure is:

Library = source code divided according to gross functionality and the .NET framework references they possess.   Rather than one big assembly that references many .NET assemblies, there will be several assemblies (each one has only the required references).    For example, data-related functionality will be separate from application UI functionality.   An assembly may reference more basic Abnaki assemblies.

Examples = Minimal demo application(s) illustrating how a little additional code gets leverage out of the components.
