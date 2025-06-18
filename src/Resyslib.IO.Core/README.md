﻿# Resyslib.IO.Core
This package provides IO focussed abstractions that aim to make File Finding, File Appending, Directory Management, and other IO operations easier. 

[![NuGet](https://img.shields.io/nuget/v/AlastairLundy.Resyslib.IO.Core.svg)](https://www.nuget.org/packages/AlastairLundy.Resyslib.IO.Core/)
[![NuGet](https://img.shields.io/nuget/dt/AlastairLundy.Resyslib.IO.Core.svg)](https://www.nuget.org/packages/AlastairLundy.Resyslib.IO.Core/)

## How to install and use AlastairLundy.Resyslib.IO.Core
Resyslib.IO.Core can be installed via the .NET SDK CLI, Nuget via your IDE or code editor's package interface, or via the Nuget website.

| Nuget Link                                                                                        | .NET SDK CLI command                                  |
|---------------------------------------------------------------------------------------------------|-------------------------------------------------------|
| [AlastairLundy.Resyslib.IO.Core Nuget](https://nuget.org/packages/AlastairLundy.Resyslib.IO.Core) | ``dotnet add package AlastairLundy.Resyslib.IO.Core`` |

## Usage
To use the DI extension method call the IServiceCollection ``AddIOExtensions`` extension method.

## How to Build the code

### Requirements
Resyslib.IO.Core requires the latest .NET release SDK to be installed to target all supported TFM (Target Framework Moniker) build targets.

Currently, the required .NET SDK is .NET 9.

The current build targets include:
* .NET 8
* .NET 9
* .NET Standard 2.0
* .NET Standard 2.1

Any version of the .NET 9 SDK can be used, but using the latest version is preferred.

### Versioning new releases
IOExtensions aims to follow Semantic versioning with ```[Major].[Minor].[Build]``` for most circumstances and an optional ``.[Revision]`` when only a configuration change is made, or a new build of a preview release is made.

#### Pre-releases
Pre-release versions should have a suffix of -alpha, -beta, -rc, or -preview followed by a ``.`` and what pre-release version number they are. The number should be incremented by 1 after each release unless it only contains a configuration change, or another packaging, or build change. An example pre-release version may look like 1.1.0-alpha.2 , this version string would indicate it is the 2nd alpha pre-release version of 1.1.0 .

#### Stable Releases
Stable versions should follow semantic versioning and should only increment the Revision number if a release only contains configuration or build packaging changes, with no change in functionality, features, or even bug or security fixes.

Releases that only implement bug fixes should see the Build version incremented.

Releases that add new non-breaking changes should increment the Minor version. Minor breaking changes may be permitted in Minor version releases where doing so is necessary to maintain compatibility with an existing supported platform, or an existing piece of code that requires a breaking change to continue to function as intended.

Releases that add major breaking changes or significantly affect the API should increment the Major version. Major version releases should not be released with excessive frequency and should be released when there is a genuine need for the API to change significantly for the improvement of the project.


### Building for Testing
You can build for testing by building the project within your IDE or VS Code, or manually by entering the following command: ``dotnet build -c Debug``.

If you encounter any bugs or issues, try testing your code with breakpoints in the affected code where appropriate. Failing that, please [report the issue](https://github.com/alastairlundy/Resyslib.IO/issues/new/) if one doesn't already exist for the bug(s).

### Building for Release
Before building a release build, ensure you apply the relevant changes to the ``Resyslib.IO.Abstractions.csproj`` file:
* Update the Package Version variable
* Update the project file's Changelog

You should ensure the project builds under debug settings before producing a release build.

#### Producing Release Builds
To manually build for release, enter ``dotnet build -c Release /p:ContinuousIntegrationBuild=true`` for a release with [SourceLink](https://github.com/dotnet/sourcelink) enabled or just ``dotnet build -c Release`` for a build without SourceLink.

Builds should generally always include SourceLink and symbol packages if intended for wider distribution.

## How to Contribute to IOExtensions
Thank you in advance for considering contributing to IOExtensions.

Please see the [CONTRIBUTING.md file](https://github.com/alastairlundy/Resyslib.IO/blob/main/CONTRIBUTING.md) for code and localization contributions.

If you want to file a bug report or suggest a potential feature to add, please check out the [GitHub issues page](https://github.com/alastairlundy/Resyslib.IO/issues/) to see if a similar or identical issue is already open.
If there is not already a relevant issue filed, please [file one here](https://github.com/alastairlundy/Resyslib.IO/issues/new) and follow the respective guidance from the appropriate issue template.

Thanks.

## License

This library is licensed under the MPL 2.0 license.