/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.IO;

namespace AlastairLundy.Resyslib.IO.Core.Directories
{
    /// <summary>
    /// Defines an interface for creating and deleting parent directories.
    /// </summary>
    public interface IParentDirectoryManager
    {
        
#if NET8_0_OR_GREATER
        /// <summary>
        /// Attempts to create a parent directory with the specified Unix file mode at the specified location.
        /// If successful, returns true; otherwise, returns false.
        /// </summary>
        /// <param name="directoryPath">The path where the parent directory should be created.</param>
        /// <param name="unixFileMode">The desired Unix file mode for the new directory.</param>
        /// <returns>True if creation was successful; false otherwise.</returns>
        bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode);
        
        /// <summary>
        /// Creates a parent directory with the specified Unix file mode at the specified location without checking for existence.
        /// </summary>
        /// <param name="parentDirectory">The path where the parent directory should be created.</param>
        /// <param name="unixFileMode">The desired Unix file mode for the new directory.</param>
        void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode);
#else
        
        /// <summary>
        /// Attempts to create a parent directory at the specified location.
        /// If successful, returns true; otherwise, returns false.
        /// </summary>
        /// <param name="directoryPath">The path where the parent directory should be created.</param>
        /// <returns>True if creation was successful; false otherwise.</returns>
        bool TryCreateParentDirectory(string directoryPath);
        
        /// <summary>
        /// Creates a parent directory at the specified location without checking for existence.
        /// </summary>
        /// <param name="parentDirectory">The path where the parent directory should be created.</param>
        void CreateParentDirectory(string parentDirectory);
#endif
    
        /// <summary>
        /// Deletes a directory and all its contents, including subdirectories.
        /// </summary>
        /// <remarks> If deleteEmptyDirectory is true, empty directories will also be deleted.
        ///</remarks>
        /// <param name="directory">The path to the directory to delete.</param>
        /// <param name="deleteEmptyDirectory">Whether to also delete empty directories.</param>
        void DeleteParentDirectory(string directory, bool deleteEmptyDirectory);

    }
}