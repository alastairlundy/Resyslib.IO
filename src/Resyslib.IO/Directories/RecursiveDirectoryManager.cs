/*
    AlastairLundy.Resyslib.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.IO;
using System.Linq;

using AlastairLundy.DotExtensions.IO.Directories;
using AlastairLundy.Resyslib.IO.Core.Directories;
using AlastairLundy.Resyslib.IO.Internal.Localizations;

// ReSharper disable RedundantBoolCompare

namespace AlastairLundy.Resyslib.IO.Directories;

/// <summary>
/// 
/// </summary>
public class RecursiveDirectoryManager : IRecursiveDirectoryManager
{
    /// <summary>
    /// Determines whether subdirectories of a directory are empty.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>True if all subdirectories in a directory are empty; false otherwise.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public bool AreSubdirectoriesEmpty(string directory)
    {
        if (!Directory.Exists(directory))
        {
            throw new DirectoryNotFoundException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", directory));
        }
        
        string[] subDirectories = Directory.GetDirectories(directory);
                
        bool[] allowRecursiveEmptyDirectoryDeletion = new bool[subDirectories.Length];

        for (int i = 0; i < subDirectories.Length; i++)
        {
            string dir = subDirectories[i];
                    
            DirectoryInfo directoryInfo = new DirectoryInfo(dir);
            
            if (directoryInfo.IsDirectoryEmpty())
            {
                allowRecursiveEmptyDirectoryDeletion[i] = true;
            }
            else
            {
                allowRecursiveEmptyDirectoryDeletion[i] = false;
            }
        }

        return allowRecursiveEmptyDirectoryDeletion.All(x => x == true);
    }
    
    /// <summary>
    /// Gets the directories and files within a parent directory.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <returns>The directories and files within a parent directory.</returns>
    public (IEnumerable<string> files, IEnumerable<string> directories) GetRecursiveDirectoryContents(string directory)
    {
        var output = GetRecursiveDirectoryContents(directory, true);
        return (output.files, output.directories);
    }

    /// <summary>
    /// Gets the directories and files within a parent directory.
    /// </summary>
    /// <param name="directory">The directory to be searched.</param>
    /// <param name="includeEmptyDirectories">Whether to include empty directories or not.</param>
    /// <returns>The directories and files within a parent directory.</returns>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist.</exception>
    public (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories)
        GetRecursiveDirectoryContents(string directory,
            bool includeEmptyDirectories)
    {
        List<string> files = new List<string>();
        List<string> directories = new List<string>();
        List<string> emptyDirectories = new List<string>();
        
        if (Directory.Exists(directory))
        {
            if (Directory.GetDirectories(directory).Length > 0)
            {
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    if (Directory.GetFiles(subDirectory).Length > 0)
                    {
                        foreach (string file in Directory.GetFiles(subDirectory))
                        {
                            files.Add(file);
                        }
                    }

                    int numberOfFiles = Directory.GetFiles(subDirectory).Length;

                    DirectoryInfo directoryInfo = new DirectoryInfo(subDirectory);
                    
                    if (numberOfFiles > 0)
                    {
                        directories.Add(subDirectory);
                    }
                    else if (includeEmptyDirectories == true && directoryInfo.IsDirectoryEmpty())
                    {
                        emptyDirectories.Add(subDirectory);
                    }
                }
            }
            else
            {
                if (includeEmptyDirectories)
                {
                    emptyDirectories.Add(directory);
                }
            }

            return (files, directories, emptyDirectories);
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", directory));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public IEnumerable<string> GetRecursiveEmptyDirectories(string directory)
    {
        return GetRecursiveDirectoryContents(directory, true).emptyDirectories;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public IEnumerable<string> GetFilesRecursively(string directory)
    {
        return GetRecursiveDirectoryContents(directory, false).files;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public IEnumerable<string> GetFolderRecursively(string directory)
    {
        return GetRecursiveDirectoryContents(directory, false).directories;
    }


    /// <summary>
    /// Attempts to delete a directory recursively by deleting subfolders and files before deleting the directory.
    /// </summary>
    /// <param name="directory">The parent directory to be deleted.</param>
    /// <param name="deleteEmptyDirectories">Whether to delete empty subfolders or not.</param>
    /// <returns>True, if the directory was recursively deleted successfully, returns false otherwise.</returns>
    public bool TryDeleteDirectoryRecursively(string directory, bool deleteEmptyDirectories)
    {
        try
        {
            DeleteDirectoryRecursively(directory, deleteEmptyDirectories);
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    /// <summary>
    /// Deletes a directory recursively by deleting one directory at a time.
    /// </summary>
    /// <param name="directory">The directory to be recursively deleted.</param>
    /// <param name="deleteEmptyDirectory">Whether to delete empty directories or not.</param>
    /// <exception cref="DirectoryNotFoundException">Thrown if the directory does not exist or could not be located.</exception>
    public void DeleteDirectoryRecursively(string directory, bool deleteEmptyDirectory)
    {
        if (Directory.Exists(directory))
        {
            if (Directory.GetDirectories(directory).Length > 0)
            {
                foreach (string subDirectory in Directory.GetDirectories(directory))
                {
                    if (Directory.GetFiles(subDirectory).Length > 0)
                    {
                        foreach (string file in Directory.GetFiles(subDirectory))
                        {
                            File.Delete(file);
                        }
                    }

                    int numberOfFiles = Directory.GetFiles(directory).Length;

                    if (deleteEmptyDirectory == true && numberOfFiles == 0 || numberOfFiles > 0)
                    {
                        Directory.Delete(subDirectory);
                    }
                }
            }
            else
            {
                if (deleteEmptyDirectory)
                {
                    Directory.Delete(directory);
                }
            }
        }

        throw new DirectoryNotFoundException(Resources.Exceptions_IO_DirectoryNotFound.Replace("{x}", directory));
    }
}