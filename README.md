# AlastairLundy.Extensions.IO
 Classes, Interfaces, and extension methods that make working with Files and Directories easier. 

[![NuGet](https://img.shields.io/nuget/v/AlastairLundy.Extensions.IO.svg)](https://www.nuget.org/packages/AlastairLundy.Extensions.IO/)
[![NuGet](https://img.shields.io/nuget/dt/AlastairLundy.Extensions.IO.svg)](https://www.nuget.org/packages/AlastairLundy.Extensions.IO/)

## Features
* Support for creating, deleting (including recursively), or finding parent or recursive directories
* Support for determining if a path is a file path
* Support for checking if a directory is empty
* Support for appending files
* Support for concatenating files
* Support for parsing and converting Unix File Permission notations

## Support 
This can be added to any .NET Standard 2.0, .NET Standard 2.1, or .NET 8 compatible Application or Library.

### Compatibility 

| IO Extensions Version series | .NET Targets supported                                     | 
|------------------------------|------------------------------------------------------------|
| 2.x                          | .NET Standard 2.0, .NET Standard 2.1, and .NET 8 and newer |
| 1.x                          | .NET 8 and newer                                           |

## License

This library is licensed under the MPL 2.0 license.

## Acknowledgements
This project would like to thank the following projects for their work:
* [Polyfill](https://github.com/SimonCropp/Polyfill) for simplifying .NET Standard 2.0 & 2.1 support

For more information, please see the [THIRD_PARTY_NOTICES file](https://github.com/alastairlundy/IOExtensions/blob/main/THIRD_PARTY_NOTICES.txt).
