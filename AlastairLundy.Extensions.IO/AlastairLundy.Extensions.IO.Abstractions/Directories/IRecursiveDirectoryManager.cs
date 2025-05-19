/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.Collections.Generic;

namespace AlastairLundy.Extensions.IO.Abstractions.Directories
{
    public interface IRecursiveDirectoryManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        bool AreSubdirectoriesEmpty(string directory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        (IEnumerable<string> files, IEnumerable<string> directories) GetRecursiveDirectoryContents(string directory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="includeEmptyDirectories"></param>
        /// <returns></returns>
        (IEnumerable<string> files, IEnumerable<string> directories, IEnumerable<string> emptyDirectories) GetRecursiveDirectoryContents(string directory, bool includeEmptyDirectories);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        IEnumerable<string> GetRecursiveEmptyDirectories(string directory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        IEnumerable<string> GetFilesRecursively(string directory);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        IEnumerable<string> GetFolderRecursively(string directory);
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="deleteEmptyDirectory"></param>
        void DeleteDirectoryRecursively(string directory, bool deleteEmptyDirectory);
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="deleteEmptyDirectories"></param>
        /// <returns></returns>
        bool TryDeleteDirectoryRecursively(string directory, bool deleteEmptyDirectories);
    }
}