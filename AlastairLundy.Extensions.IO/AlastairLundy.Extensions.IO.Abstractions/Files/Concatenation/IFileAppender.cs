/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AlastairLundy.Resyslib.IO.Files;

namespace AlastairLundy.Extensions.IO.Abstractions.Files.Concatenation
{
    public interface IFileAppender
    {
        /// <summary>
        /// Appends the contents of a file to an existing list of strings.
        /// </summary>
        /// <param name="fileToBeAppended">The file to be appended.</param>
        void AppendFile(string fileToBeAppended);
        
        /// <summary>
        /// Appends the contents of a file to an existing list.
        /// </summary>
        /// <param name="fileToBeAppended">The file to be appended.</param>
        void AppendFile(FileModel fileToBeAppended);
    
        /// <summary>
        /// Asynchronously appends the contents of a file to an existing list of strings.
        /// </summary>
        /// <param name="fileToBeAppended">The file to be appended.</param>
        /// <param name="cancellationToken">The cancellation token to be used in case of cancellation.</param>
        Task AppendFileAsync(string fileToBeAppended, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Asynchronously appends the contents of a file to an existing list of strings.
        /// </summary>
        /// <param name="fileToBeAppended">The file to be appended.</param>
        /// <param name="cancellationToken">The cancellation token to be used in case of cancellation.</param>
        Task AppendFileAsync(FileModel fileToBeAppended, CancellationToken cancellationToken = default);
    
        /// <summary>
        /// Attempts to append the contents of a file to an existing list.
        /// </summary>
        /// <param name="fileToBeAppended">The file to have its contents appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
        /// <returns>True if the file was successfully appended; false otherwise.</returns>
        bool TryAppendFile(string fileToBeAppended);
        
        /// <summary>
        /// Append the contents of an ordered enumerable of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
        void AppendFiles(IOrderedEnumerable<string> filesToBeAppended);
        
        /// <summary>
        /// Append the contents of the IEnumerable of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
        void AppendFiles(IEnumerable<string> filesToBeAppended);
        
        /// <summary>
        /// Append the contents of the IEnumerable of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing appended content.</param>
        void AppendFiles(IEnumerable<FileModel> filesToBeAppended);
    
        /// <summary>
        /// Attempts to append the contents of files to an existing list.
        /// </summary>
        /// <param name="filesToBeAppended">The files to be appended to the existing file contents. If no existing file contents exists, this will become the contents appended to in the future.</param>
        /// <returns>True if the files were successfully appended; false otherwise.</returns>
        bool TryAppendFiles(IEnumerable<string> filesToBeAppended);

        /// <summary>
        /// Returns the appended contents as an IEnumerable.
        /// </summary>
        /// <returns>The list of appended strings as an enumerable.</returns>
        IEnumerable<string> ToEnumerable();
        
        /// <summary>
        /// Converts the appended file contents to a string.
        /// </summary>
        /// <returns>The appended file contents as a string.</returns>
        string ToString();

        /// <summary>
        /// Writes the appended strings to a file.
        /// </summary>
        /// <param name="filePath">The path to save the file to.</param>
        void WriteToFile(string filePath);
        
        /// <summary>
        /// Writes the appended strings to a file.
        /// </summary>
        /// <param name="file">The file to be saved to.</param>
        void WriteToFile(FileModel file);

    }
}