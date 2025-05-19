/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace AlastairLundy.Extensions.IO.Abstractions.Directories
{
    /// <summary>
    /// Defines an interface for deleting directories recursively,
    /// and recursively returning the contents of directories.
    /// </summary>
    public interface IRecursiveDirectoryManager
    {
        /// <summary>
        /// Checks if a specified directory is empty of subdirectories.
        /// </summary>
        /// <param name="directory">The path to the directory to check.</param>
        /// <returns>True if the directory is empty, false otherwise.</returns>
        bool AreSubdirectoriesEmpty(string directory);

        /// <summary>
        /// Retrieves recursive the contents of a directory, including files and subdirectories.
        /// </summary>
        /// <param name="directory">The path to the directory to retrieve contents from.</param>
        /// <returns>A tuple containing two lists: files and directories.</returns>
        (IEnumerable<string> files, IEnumerable<string> directories) GetRecursiveDirectoryContents(string directory);

        /// <summary>
        /// Recursively retrieves the contents of a directory, including files, subdirectories, and empty directories.
        /// </summary>
        /// <param name="directory">The path to the directory to retrieve contents from.</param>
        /// <param name="includeEmptyDirectories">A flag indicating whether to include empty directories
        /// in the results.
        /// </param>
        /// <returns>A tuple containing three lists: files, directories, and empty directories.</returns>
        (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) GetRecursiveDirectoryContents(string directory, bool includeEmptyDirectories);

        /// <summary>
        /// Retrieves a list of empty subdirectories within a specified directory.
        /// </summary>
        /// <param name="directory">The path to the directory to search for empty subdirectories.</param>
        /// <returns>A collection of paths to empty directories.</returns>
        IEnumerable<string> GetRecursiveEmptyDirectories(string directory);

        /// <summary>
        /// Recursively searches for and returns all files within a specified directory.
        /// </summary>
        /// <param name="directory">The path to the directory to search for files.</param>
        /// <returns>A collection of paths to files found in the directory or its subdirectories.</returns>
        IEnumerable<string> GetFilesRecursively(string directory);
        
        /// <summary>
        /// Recursively searches for and returns all folders (subdirectories) within a specified directory.
        /// </summary>
        /// <param name="directory">The path to the directory to search for folders.</param>
        /// <returns>A collection of paths to folders found in the directory or its subdirectories.</returns>
        IEnumerable<string> GetFolderRecursively(string directory);
    
        /// <summary>
        /// Recursively deletes a specified directory and its contents.
        /// </summary>
        /// <param name="directory">The path to the directory to be deleted.</param>
        /// <param name="deleteEmptyDirectory">A flag indicating whether to delete empty directories
        /// (i.e. directories with no files or subdirectories).</param>
        void DeleteDirectoryRecursively(string directory, bool deleteEmptyDirectory);
    
        /// <summary>
        /// Attempts to recursively delete a specified directory and its contents.
        /// If the deletion fails for any reason, the method returns false without deleting anything.
        /// </summary>
        /// <param name="directory">The path to the directory to be deleted.</param>
        /// <param name="deleteEmptyDirectories">A flag indicating whether to delete empty directories
        /// (i.e. directories with no files or subdirectories).</param>
        /// <returns>True if the deletion is successful, false otherwise.</returns>
        bool TryDeleteDirectoryRecursively(string directory, bool deleteEmptyDirectories);
    }
}