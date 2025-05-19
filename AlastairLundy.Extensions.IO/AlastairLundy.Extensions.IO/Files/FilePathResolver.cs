/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.Collections.Generic;
using System.IO;
using System.Linq;

using AlastairLundy.Extensions.IO.Files.Abstractions;

namespace AlastairLundy.Extensions.IO.Files
{
    public class FilePathResolver : IFilePathResolver
    {
        /// <summary>
        /// Resolves the file path to a specific file.
        /// </summary>
        /// <param name="inputFilePath">The input file path to resolve.</param>
        /// <param name="resolvedFilePath">The resolved file path.</param>
        /// <exception cref="FileNotFoundException">Thrown if the input file path is null or empty.</exception>
        public void ResolveFilePath(string inputFilePath, out string resolvedFilePath)
        {
            int recursionNumber = 0;
        
            string newPath = string.Join(string.Empty,
                inputFilePath.Where(x => Path.GetInvalidPathChars()
                                             .Contains(x) ==
                                         false &&
                                         Path.GetInvalidFileNameChars()
                                             .Contains(x) ==
                                         false)); 
            
            while (recursionNumber < 3)
            {
#if NET6_0_OR_GREATER || NETSTANDARD2_1
                if (Path.IsPathFullyQualified(newPath))
#else
            if(Path.IsPathRooted(newPath))
#endif
                {
                    resolvedFilePath = newPath;
                    return;
                }

#if NET6_0_OR_GREATER
                if (Path.Exists(Path.GetFullPath(inputFilePath)) == false)
#else
            if (File.Exists(Path.GetFullPath(inputFilePath)) == false)
#endif
                {
                    string[] directoryComponents = Path.GetFullPath(inputFilePath).Split(Path.DirectorySeparatorChar);

#if NET8_0_OR_GREATER || NETSTANDARD2_1
                    string lastDirectory = Directory.Exists(directoryComponents.Last())
                        ? directoryComponents.Last()
                        : directoryComponents.SkipLast(1).Last();
#else
                string lastDirectory = Directory.Exists(directoryComponents.Last())
                    ? directoryComponents.Last()
                    : directoryComponents[directoryComponents.Length - 1];
#endif
                
                    string targetFileName = Path.GetFileName(newPath);
                
                    if (Directory.Exists(Path.GetFullPath(lastDirectory)))
                    {
                        IEnumerable<string> files = Directory.EnumerateFiles(Path.GetFullPath(inputFilePath));

                        foreach (string file in files)
                        {
                            FileInfo fileInfo = new(file);

                            if (fileInfo.FullName.Equals(Path.GetFullPath(lastDirectory)) ||
                                fileInfo.Name.Equals(targetFileName)){
                                resolvedFilePath = fileInfo.FullName;
                                return;
                            }
                        }
                    }
                
                    recursionNumber += 1;
                }
                else
                {
                    resolvedFilePath = Path.GetFullPath(newPath);
                    return;
                }
            }
        
            resolvedFilePath = newPath;
        }
    }
}