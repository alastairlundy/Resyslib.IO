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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using AlastairLundy.Extensions.IO.Abstractions.Files;
using AlastairLundy.Extensions.IO.Abstractions.Files.Concatenation;
using AlastairLundy.Extensions.IO.Internal.Localizations;

using AlastairLundy.Resyslib.IO.Files;

// ReSharper disable RedundantIfElseBlock

namespace AlastairLundy.Extensions.IO.Files.Concatenation
{
    /// <summary>
    /// A class to append the contents of files.
    /// </summary>
    public class FileAppender : IFileAppender
    {
        private readonly StringBuilder _appendedFileContents;
        
        private readonly IFileFinder _fileFinder;
    
        /// <summary>
        /// Instantiates the FileAppender's internal String builder.
        /// </summary>
        /// <param name="fileFinder">The file finder instance to be used.</param>
        public FileAppender(IFileFinder fileFinder)
        {
            _fileFinder = fileFinder;
            _appendedFileContents = new StringBuilder();
        }
    
        /// <summary>
        /// Asynchronously appends the contents of a file to an existing list of strings.
        /// </summary>
        /// <param name="fileToBeAppended">The file to be appended.</param>
        /// <param name="cancellationToken">The cancellation token to be used in case of cancellation.</param>
        /// <exception cref="Exception">Thrown if the file appending fails.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
        public async Task AppendFileAsync(FileModel fileToBeAppended, CancellationToken cancellationToken = default)
        {
            if (_fileFinder.IsAFile(fileToBeAppended.FilePath) || File.Exists(fileToBeAppended.FilePath))
            {
                try
                {
#if NET6_0_OR_GREATER
                string[] lines = await File.ReadAllLinesAsync(fileToBeAppended.FilePath, cancellationToken);
#else
                    string[] lines = await Task.FromResult(File.ReadAllLines(fileToBeAppended.FilePath));
#endif
                
                    foreach (string line in lines)
                    {
                        _appendedFileContents.AppendLine(line);
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message, exception);
                }
            }
            else
            {
                throw new FileNotFoundException(Resources.Exceptions_IO_FileNotFound, fileToBeAppended.FileName);
            }
        }

        /// <summary>
        /// Attempts to append the contents of a file to an existing list.
        /// </summary>
        /// <param name="fileToBeAppended">The file to have its contents appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
        /// <returns>True if the file was successfully appended; false otherwise.</returns>
        public bool TryAppendFile(string fileToBeAppended)
        {
            try
            {
                AppendFile(fileToBeAppended);

                return true;
            }
            catch
            {
                return false;
            }
        }
    
        /// <summary>
        /// Appends the contents of a file to an existing list.
        /// </summary>
        /// <param name="fileToBeAppended">The file to be appended.</param>
        /// <exception cref="Exception">Thrown if the file appending fails.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
        public void AppendFile(FileModel fileToBeAppended)
        {
            if (_fileFinder.IsAFile(fileToBeAppended.FilePath) || File.Exists(fileToBeAppended.FilePath))
            {
                try
                {
                    string[] lines = File.ReadAllLines(fileToBeAppended.FilePath);

                    foreach (string line in lines)
                    {
                        _appendedFileContents.AppendLine(line);
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message, exception);
                }
            }
            else
            {
                throw new FileNotFoundException(Resources.Exceptions_IO_FileNotFound, fileToBeAppended.FileName);
            }
        }

        /// <summary>
        /// Asynchronously appends the contents of a file to an existing list of strings.
        /// </summary>
        /// <param name="fileToBeAppended">The file to be appended.</param>
        /// <param name="cancellationToken">The cancellation token to be used in case of cancellation.</param>
        /// <exception cref="Exception">Thrown if the file appending fails.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
        public async Task AppendFileAsync(string fileToBeAppended, CancellationToken cancellationToken = default)
        {
            if (_fileFinder.IsAFile(fileToBeAppended) || File.Exists(fileToBeAppended))
            {
                try
                {
#if NET6_0_OR_GREATER
                string[] lines = await File.ReadAllLinesAsync(fileToBeAppended, cancellationToken);
#else
                    string[] lines = await Task.FromResult(File.ReadAllLines(fileToBeAppended));
#endif
                
                    foreach (string line in lines)
                    {
                        _appendedFileContents.AppendLine(line);
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message, exception);
                }
            }
            else
            {
                throw new FileNotFoundException(Resources.Exceptions_IO_FileNotFound, fileToBeAppended);
            }
        }

        /// <summary>
        /// Appends the contents of a file to an existing list of strings.
        /// </summary>
        /// <param name="fileToBeAppended">The file to be appended.</param>
        /// <exception cref="Exception">Thrown if the file appending fails.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
        public void AppendFile(string fileToBeAppended)
        {
            if (_fileFinder.IsAFile(fileToBeAppended) || File.Exists(fileToBeAppended))
            {
                try
                {
                    string[] lines = File.ReadAllLines(fileToBeAppended);

                    foreach (string line in lines)
                    {
                        _appendedFileContents.AppendLine(line);
                    }
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message, exception);
                }
            }
            else
            {
                throw new FileNotFoundException(Resources.Exceptions_IO_FileNotFound, fileToBeAppended);
            }
        }

        /// <summary>
        /// Append the contents of an ordered enumerable of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
        public void AppendFiles(IOrderedEnumerable<string> filesToBeAppended)
        {
            AppendFiles(filesToBeAppended.ToArray());
        }

        /// <summary>
        /// Append the contents of the IEnumerable of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
        public void AppendFiles(IEnumerable<string> filesToBeAppended)
        {
            foreach (string file in filesToBeAppended)
            {
                AppendFile(file);
            }
        }
    
        /// <summary>
        /// Append the contents of the IEnumerable of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
        public void AppendFiles(IEnumerable<FileModel> filesToBeAppended)
        {
            foreach (FileModel file in filesToBeAppended)
            {
                AppendFile(file);
            }
        }

        /// <summary>
        /// Attempts to append the contents of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
        /// <returns>True if the files were successfully appended; false otherwise.</returns>
        public bool TryAppendFiles(IEnumerable<string> filesToBeAppended)
        {
            try
            {
                AppendFiles(filesToBeAppended);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the appended contents as an IEnumerable.
        /// </summary>
        /// <returns>The list of appended strings as an enumerable.</returns>
        public IEnumerable<string> ToEnumerable()
        {
#if NET6_0_OR_GREATER
        return _appendedFileContents.ToString().Split(Environment.NewLine);
#else
            return _appendedFileContents.ToString().Split(Convert.ToChar(Environment.NewLine));
#endif
        }

        /// <summary>
        /// Converts the appended file contents to a string.
        /// </summary>
        /// <returns>The appended file contents as a string.</returns>
        public override string ToString()
        {
            return _appendedFileContents.ToString();
        }

        /// <summary>
        /// Writes the appended strings to a file.
        /// </summary>
        /// <param name="file">The file to be saved to.</param>
        /// <exception cref="ArgumentException">Thrown if an invalid file path is provided.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
        /// <exception cref="Exception">Thrown if an exception occurs when attempting to write the file.</exception>
        public void WriteToFile(FileModel file)
        {
            WriteToFile(file.FilePath);
        }

        /// <summary>
        /// Writes the appended strings to a file.
        /// </summary>
        /// <param name="filePath">The path to save the file to.</param>
        /// <exception cref="ArgumentException">Thrown if an invalid file path is provided.</exception>
        /// <exception cref="FileNotFoundException">Thrown if the file specified is not found.</exception>
        /// <exception cref="Exception">Thrown if an exception occurs when attempting to write the file.</exception>
        public void WriteToFile(string filePath)
        {
            if (_fileFinder.IsAFile(filePath))
            {
                try
                {
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    File.WriteAllLines(filePath, ToEnumerable());
                }
                catch (FileNotFoundException exception)
                {
                    throw new FileNotFoundException(exception.Message, filePath, exception);
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.Message, exception);
                }
            }
            else
            {
                throw new ArgumentException(Resources.Exceptions_IO_NoFileProfiled, filePath);
            }
        }
    }
}