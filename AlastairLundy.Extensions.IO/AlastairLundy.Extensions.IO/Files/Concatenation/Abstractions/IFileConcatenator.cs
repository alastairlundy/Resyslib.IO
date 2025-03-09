/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;

namespace AlastairLundy.Extensions.IO.Files.Concatenation.Abstractions
{
    public interface IFileConcatenator
    {
        /// <summary>
        /// Concatenated the contents of files in the style of the Unix Cat command.
        /// </summary>
        /// <param name="files">The files to be concatenated.</param>
        /// <returns>The concatenated files as an IEnumerable of strings.</returns>
        IEnumerable<string> ConcatenateFilesToEnumerable(IEnumerable<string> files);

        /// <summary>
        /// Concatenates the contents of specified files and saves it to a new file.
        /// </summary>
        /// <param name="filePath">The path to save the new file to.</param>
        /// <param name="newFileName">The name of the new file to be created.</param>
        /// <param name="files">The files to be concatenated.</param>
        /// <exception cref="Exception">Thrown if an exception occurs when trying to save the file.</exception>
        void ConcatenateFilesToNewFile(string filePath, string newFileName, IEnumerable<string> files);
    }
}