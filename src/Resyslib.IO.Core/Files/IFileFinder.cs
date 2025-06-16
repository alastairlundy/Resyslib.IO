/*
    Resyslib.IO
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace AlastairLundy.Resyslib.IO.Core.Files;

/// <summary>
/// Defines an interface for finding files and determining whether a specific file path is a file.
/// </summary>
public interface IFileFinder
{
    /// <summary>
    /// Determines whether a string is the name of a file.
    /// </summary>
    /// <param name="filePath">The string to be searched.</param>
    /// <returns>True if the string is a file; false otherwise.</returns>
    bool IsAFile(string filePath);
}