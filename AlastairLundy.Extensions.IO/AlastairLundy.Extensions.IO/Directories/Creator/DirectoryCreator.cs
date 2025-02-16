/*
    IOExtensions 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

#if NETSTANDARD2_0 || NETSTANDARD2_1
using OperatingSystem = Polyfills.OperatingSystemPolyfill;
#endif

namespace AlastairLundy.Extensions.IO.Directories.Creator;

public class DirectoryCreator : IDirectoryCreator
{
    /// <summary>
    /// Attempts to create a new directory with the specified parameters.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to be created.</param>
    /// <param name="newDirectoryName"></param>
    /// <param name="unixFileMode">The file mode to use to create the directory. Only used on Unix based systems.</param>
    /// <param name="createParentPaths">Whether to create parent directory paths, if required, when creating the new directory.</param>
    /// <returns>true if the directory was successfully created; returns false otherwise.</returns>
#if NET8_0_OR_GREATER
    public bool TryCreateDirectory(string directoryPath, string newDirectoryName, UnixFileMode unixFileMode, bool createParentPaths)
#else
    public bool TryCreateDirectory(string directoryPath, string newDirectoryName, bool createParentPaths)
#endif
{
        try
        {
#if NET8_0_OR_GREATER
            CreateDirectory(directoryPath, newDirectoryName, unixFileMode, createParentPaths);
#else
            CreateDirectory(directoryPath, newDirectoryName, createParentPaths);
#endif
return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="directoryPath"></param>
    /// <param name="unixFileMode"></param>
    /// <returns></returns>
#if NET8_0_OR_GREATER
    public bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode)
#else
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

    /// <summary>
    /// Recursively creates the parent directory as needed.
    /// </summary>
    /// <param name="parentDirectory">The parent directory to be created.</param>
    /// <param name="unixFileMode"></param>

#if NET8_0_OR_GREATER
    public void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode)
#else
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
                    CreateDirectory(directory, directory, unixFileMode, false);
#else
                CreateDirectory(directory, directory, false);
#endif
            }
        }
}

    /// <summary>
    /// Creates a new directory with the specified parameters.
    /// </summary>
    /// <param name="directoryPath">The path of the directory to be created.</param>
    /// <param name="newDirectoryName"></param>
    /// <param name="unixFileMode">The file mode to use to create the directory. Only used on Unix based systems.</param>
    /// <param name="createParentPaths">Whether to create parent directory paths, if required, when creating the new directory.</param>
#if NET8_0_OR_GREATER
    public void CreateDirectory(string directoryPath, string newDirectoryName, UnixFileMode unixFileMode,
        bool createParentPaths)
#else
    public void CreateDirectory(string directoryPath, string newDirectoryName, bool createParentPaths)
#endif
    {
        if (createParentPaths)
        {
            if (directoryPath.EndsWith(newDirectoryName))
            {
                directoryPath = directoryPath.Remove(directoryPath.Length - newDirectoryName.Length,
                    newDirectoryName.Length);
            }
    
#if NET8_0_OR_GREATER
            CreateParentDirectory(directoryPath, unixFileMode);
#else
            CreateParentDirectory(directoryPath);
#endif
            
        }
        else
        {
            if (OperatingSystem.IsWindows())
            {
                Directory.CreateDirectory(directoryPath);
            }
            else
            {
#if NET8_0_OR_GREATER
                Directory.CreateDirectory(directoryPath, unixFileMode);
#else
                Directory.CreateDirectory(directoryPath);
#endif
            }
        }
    }
}