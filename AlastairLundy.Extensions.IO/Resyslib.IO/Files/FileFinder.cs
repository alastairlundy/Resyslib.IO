/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.IO;

using AlastairLundy.Resyslib.IO.Core.Files;


// ReSharper disable UseIndexFromEndExpression

namespace AlastairLundy.Resyslib.IO.Files
{
    /// <summary>
    /// 
    /// </summary>
    public class FileFinder : IFileFinder
    {
        /// <summary>
        /// Determines whether a string is the name of a file.
        /// </summary>
        /// <param name="filePath">The string to be searched.</param>
        /// <returns>True if the string is a file; false otherwise.</returns>
        public bool IsAFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return true;
                }
            
                if (filePath.Length > 1)
                {
                    if (filePath.Length - 4 >= 0 && filePath.Length - 4 < filePath.Length)
                    {
                        // Uses new .NET 6 and newer ^ Index
#if NET6_0_OR_GREATER
                    if (filePath[^4].Equals('.'))
#else
                        if (filePath[filePath.Length - 4].Equals('.'))
#endif
                        {
                            return true;
                        }
                    }
                    if (filePath.Length - 3 >= 0 && filePath.Length - 3 < filePath.Length)
                    {
                        // Uses new .NET 6 and newer ^ Index
                        if (filePath[filePath.Length - 3].Equals('.') || filePath[filePath.Length - 2].Equals('.'))
                        {
                            return true;
                        }
                    }

                    if (filePath.Length - 2 >= 0 && filePath.Length - 2 < filePath.Length)
                    {
                        // Uses new .NET 6 and newer ^ Index
                        if (filePath[filePath.Length - 2].Equals('.'))
                        {
                            return true;
                        }
                    }
                }
            
                return File.Exists(filePath);
            }
            catch
            {
                return false;
            }
        }
    }
}