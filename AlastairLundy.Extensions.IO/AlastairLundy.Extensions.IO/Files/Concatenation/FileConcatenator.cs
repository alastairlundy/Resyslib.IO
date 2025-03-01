/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.IO;
using AlastairLundy.Extensions.IO.Files.Concatenation.Abstractions;

// ReSharper disable UnusedType.Global

namespace AlastairLundy.Extensions.IO.Files.Concatenation;

/// <summary>
/// Syntactic sugar around the FileAppender class.
/// </summary>
public class FileConcatenator : IFileConcatenator
{
    private readonly IFileAppender _fileAppender;
    
    /// <summary>
    /// Instantiates the File Concatenator with the specified IFileAppender.
    /// </summary>
    /// <param name="fileAppender">The IFileAppender instance to be used.</param>
    public FileConcatenator(IFileAppender fileAppender)
    {
        _fileAppender = fileAppender;
    }
        
    /// <summary>
    /// Concatenated the contents of files in the style of the Unix Cat command.
    /// </summary>
    /// <param name="files">The files to be concatenated.</param>
    /// <returns>The concatenated files as an IEnumerable of strings.</returns>
    public IEnumerable<string> ConcatenateFilesToEnumerable(IEnumerable<string> files)
    {
        _fileAppender.AppendFiles(files);

        return _fileAppender.ToEnumerable();
    }
        
    /// <summary>
    /// Concatenates the contents of specified files and saves it to a new file.
    /// </summary>
    /// <param name="filePath">The path to save the new file to.</param>
    /// <param name="newFileName">The name of the new file to be created.</param>
    /// <param name="files">The files to be concatenated.</param>
    /// <exception cref="Exception">Thrown if an exception occurs when trying to save the file.</exception>
    public void ConcatenateFilesToNewFile(string filePath, string newFileName, IEnumerable<string> files)
    {
        string newFile = $"{filePath}{Path.DirectorySeparatorChar}{newFileName}";

        if (filePath.Contains(newFileName) == false)
        {
            File.WriteAllLines(newFile, ConcatenateFilesToEnumerable(files));
        }
        else
        {
            File.WriteAllLines(newFileName, ConcatenateFilesToEnumerable(files));
        }     
    }
}