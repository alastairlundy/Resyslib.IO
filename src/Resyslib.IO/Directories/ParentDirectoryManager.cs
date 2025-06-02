/*
    AlastairLundy.Resyslib.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using AlastairLundy.DotExtensions.IO.Directories;

using AlastairLundy.Resyslib.IO.Core.Directories;
using AlastairLundy.Resyslib.IO.Internal.Localizations;

namespace AlastairLundy.Resyslib.IO.Directories
{
    /// <summary>
    /// 
    /// </summary>
    public class ParentDirectoryManager : IParentDirectoryManager
    {
    
#if NET8_0_OR_GREATER 
        /// <summary>
        /// Attempts to create a parent directory with the specified Unix file mode at the specified location.
        /// If successful, returns true; otherwise, returns false.
        /// </summary>
        /// <param name="directoryPath">The path where the parent directory should be created.</param>
        /// <param name="unixFileMode">The desired Unix file mode for the new directory.</param>
        /// <returns>True if creation was successful; false otherwise.</returns> 
public bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode)
#else
        /// <summary>
        /// Attempts to create a parent directory at the specified location.
        /// If successful, returns true; otherwise, returns false.
        /// </summary>
        /// <param name="directoryPath">The path where the parent directory should be created.</param>
        /// <returns>True if creation was successful; false otherwise.</returns>
        public bool TryCreateParentDirectory(string directoryPath)
#endif
        {
            try
            {
#if NET8_0_OR_GREATER
            CreateParentDirectory(directoryPath, unixFileMode);
#else
                CreateParentDirectory(directoryPath);
#endif

                return true;
            }
            catch
            {
                return false;
            }
        }



#if NET8_0_OR_GREATER
        /// <summary>
        /// Creates a parent directory with the specified Unix file mode at the specified location without checking for existence.
        /// </summary>
        /// <param name="parentDirectory">The path where the parent directory should be created.</param>
        /// <param name="unixFileMode">The desired Unix file mode for the new directory.</param>
    public void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode)
#else
        /// <summary>
        /// Creates a parent directory at the specified location without checking for existence.
        /// </summary>
        /// <param name="parentDirectory">The path where the parent directory should be created.</param>
        public void CreateParentDirectory(string parentDirectory)
#endif
        {
            string[] directories = parentDirectory.Split(Path.DirectorySeparatorChar);

            List<string> directoriesToCreate = new List<string>();

            for (int i = 0; i < directories.Length; i++)
            {
                StringBuilder stringBuilder = new StringBuilder();

                for (int j = 0; j < i; j++)
                {
                    stringBuilder.Append(directories[i][j]);
                }

                directoriesToCreate.Add(stringBuilder.ToString());
            }

            foreach (string directory in directoriesToCreate)
            {
                if (Directory.Exists(directory) == false)
                {
#if NET8_0_OR_GREATER
                if (OperatingSystem.IsWindows())
                {
                    Directory.CreateDirectory(directory);
                }
                else
                {
                    Directory.CreateDirectory(directory, unixFileMode);
                }
#else
                    Directory.CreateDirectory(directory);
#endif
                }
            }
        }
    
        /// <summary>
        /// Deletes a parent directory of a directory.
        /// </summary>
        /// <param name="directory">The directory to get the parent directory of.</param>
        /// <param name="deleteEmptyDirectory">Whether to delete the parent directory if is empty or not.</param>
        /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
        public void DeleteParentDirectory(string directory, bool deleteEmptyDirectory)
        {
            if (Directory.Exists(directory))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
            
                if (directoryInfo.IsDirectoryEmpty() && deleteEmptyDirectory || directoryInfo.IsDirectoryEmpty() == false)
                {
                    string? parentDirectory = Directory.GetParent(directory)?.FullName;

                    try
                    {
                        if (parentDirectory == null)
                        {
                            throw new NullReferenceException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", directory));        
                        }

                        Directory.Delete(parentDirectory);
                    }
                    catch(Exception ex)
                    {
                        throw new Exception(ex.Message, ex);
                    }
                }
            }

            throw new DirectoryNotFoundException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", directory));
        }
    }
}