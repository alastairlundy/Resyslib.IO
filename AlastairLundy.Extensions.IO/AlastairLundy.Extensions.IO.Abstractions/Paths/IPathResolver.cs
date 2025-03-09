/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using AlastairLundy.Extensions.IO.Abstractions.Files;

namespace AlastairLundy.Extensions.IO.Abstractions.Paths
{
    public interface IPathResolver
    {
        bool DoesPathHaveExtension(string path);
    
        PathType GetPathType(string path);
    
        bool DoesPathExist(string path);
        bool IsPathFullyQualified(string path);
    
        string ToRelativePath(string path);
        string ToAbsolutePath(string path);

        string NormalizePath(string path);

        FileModel GetFile(string path);
        string GetFileNameWithoutExtension(string path);
        string GetFileName(string path);
        string GetPathExtension(string path);
    
        string CombinePaths(string path1, string path2);
    
        string ExpandEnvironmentVariablesInPath(string path);
    }
}