/*
    AlastairLundy.Extensions.IO.Abstractions
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using AlastairLundy.Extensions.IO.Abstractions.Files;

namespace AlastairLundy.Extensions.IO.Abstractions.Paths
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPathResolver
    {
        /// <summary>
        /// Checks if a given file or directory has an extension.
        /// </summary>
        /// <param name="path">The file or directory path to check.</param>
        /// <returns>True if the path has an extension, false otherwise.</returns>
        bool DoesPathHaveExtension(string path);

        /// <summary>
        /// Gets the type of the specified file or directory (e.g. file, folder, etc.).
        /// </summary>
        /// <param name="path">The file or directory path to determine the type for.</param>
        /// <returns>The type of the specified path.</returns>
        PathType GetPathType(string path);

        /// <summary>
        /// Checks if the specified file or directory exists.
        /// </summary>
        /// <param name="path">The file or directory path to check for.</param>
        /// <returns>True if the path exists; false otherwise.</returns>
        bool DoesPathExist(string path);

        /// <summary>
        /// Determines whether a given path is fully qualified (i.e. includes drive letter and root).
        /// </summary>
        /// <param name="path">The file or directory path to check.</param>
        /// <returns>True if the path is fully qualified; false otherwise.</returns>
        bool IsPathFullyQualified(string path);

        /// <summary>
        /// Converts a given absolute path to a relative path (i.e. removes drive letter and root).
        /// </summary>
        /// <param name="path">The file or directory path to convert.</param>
        /// <returns>The converted relative path.</returns>
        string ToRelativePath(string path);

        /// <summary>
        /// Converts a given relative path to an absolute path (i.e. adds drive letter and root).
        /// </summary>
        /// <param name="path">The file or directory path to convert.</param>
        /// <returns>The converted absolute path.</returns>
        string ToAbsolutePath(string path);

        /// <summary>
        /// Normalizes a given file or directory path by removing redundant separators.
        /// </summary>
        /// <param name="path">The file or directory path to normalize.</param>
        /// <returns>The normalized path.</returns>
        string NormalizePath(string path);

        /// <summary>
        /// Creates a FileModel for a given file path.
        /// </summary>
        /// <param name="path">The file path to search for.</param>
        /// <returns>A FileModel associated with the specified path.</returns>
        FileModel GetFile(string path);

        /// <summary>
        /// Gets the file name without the file extension from a given file or directory path.
        /// </summary>
        /// <param name="path">The file or directory path to extract the file name without extension from.</param>
        /// <returns>The file name the without file extension.</returns>
        string GetFileNameWithoutExtension(string path);

        /// <summary>
        /// Gets the full file name (including extension) from a given file or directory path.
        /// </summary>
        /// <param name="path">The file or directory path to extract the file name from.</param>
        /// <returns>The full file name including extension.</returns>
        string GetFileName(string path);

        /// <summary>
        /// Gets the file extension from a given file or directory path.
        /// </summary>
        /// <param name="path">The file or directory path to extract the file extension from.</param>
        /// <returns>The file extension (e.g. .txt, .docx, etc.).</returns>
        string GetPathExtension(string path);

        /// <summary>
        /// Combines two file or directory paths into a single absolute path.
        /// </summary>
        /// <param name="path1">The first path to combine.</param>
        /// <param name="path2">The second path to combine with the first.</param>
        /// <returns>The combined absolute path.</returns>
        string CombinePaths(string path1, string path2);

        /// <summary>
        /// Expands all environment variables in a given file or directory path.
        /// </summary>
        /// <param name="path">The file or directory path to be expanded.</param>
        /// <returns>The path with expanded environment variables.</returns>
        string ExpandEnvironmentVariablesInPath(string path);
    }
}