/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.IO;

namespace AlastairLundy.Extensions.IO.Abstractions.Directories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IParentDirectoryManager
    {
#if NET8_0_OR_GREATER
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="unixFileMode"></param>
        /// <returns></returns>
        bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentDirectory"></param>
        /// <param name="unixFileMode"></param>
        void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode);
#else
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        bool TryCreateParentDirectory(string directoryPath);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentDirectory"></param>
        void CreateParentDirectory(string parentDirectory);
#endif
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="deleteEmptyDirectory"></param>
        void DeleteParentDirectory(string directory, bool deleteEmptyDirectory);

    }
}