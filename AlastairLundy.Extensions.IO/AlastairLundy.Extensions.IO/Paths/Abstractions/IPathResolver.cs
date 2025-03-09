/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using AlastairLundy.Extensions.IO.Files;

namespace AlastairLundy.Extensions.IO.Paths.Abstractions
{
    public interface IPathResolver
    {
        public bool DoesPathHaveExtension(string path);
    
        public PathType GetPathType(string path);
    
        public bool DoesPathExist(string path);
        public bool IsPathFullyQualified(string path);
    
        public string ToRelativePath(string path);
        public string ToAbsolutePath(string path);

        public string NormalizePath(string path);

        public FileModel GetFile(string path);
        public string GetFileNameWithoutExtension(string path);
        public string GetFileName(string path);
        public string GetPathExtension(string path);
    
        public string CombinePaths(string path1, string path2);
    
        public string ExpandEnvironmentVariablesInPath(string path);
    }
}