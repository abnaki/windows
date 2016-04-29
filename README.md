
Abnaki Way Components

Small framework for .NET Windows applications, providing useful
necessities and conventions.  C# and Visual Studio 2013 projects.

Users are expected to build Abnaki's source code.  Perhaps reference Abnaki's
project file(s) within your solution.  No binaries here.  Not on NuGet.

Such a set of libraries, however small, saves you developer(s) from application-specific code bloat, or consulting the Internet for answers to previously solved problems.  Improvements in these components will benefit all applications developed upon them.

The structure is:

Library = source code divided according to gross functionality and the .NET framework references they possess.   Rather than one big component that references many .NET assemblies, there will be several components (each one has only the required references).    For example, data-related functionality will be in a different component from application UI functionality.   A component may reference more basic Abnaki components.

Examples = Minimal demo application(s) illustrating how a little additional code gets leverage out of the components.
