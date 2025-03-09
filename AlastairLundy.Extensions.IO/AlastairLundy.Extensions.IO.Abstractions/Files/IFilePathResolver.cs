/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.IO;

namespace AlastairLundy.Extensions.IO.Abstractions.Files
{
    public interface IFilePathResolver
    {
        /// <summary>
        /// Resolves the file path to a specific file.
        /// </summary>
        /// <param name="inputFilePath">The input file path to resolve.</param>
        /// <param name="outputFilePath">The resolved file path.</param>
        /// <exception cref="FileNotFoundException">Thrown if the input file path is null or empty.</exception>
        void ResolveFilePath(string inputFilePath, out string outputFilePath);
    }
}