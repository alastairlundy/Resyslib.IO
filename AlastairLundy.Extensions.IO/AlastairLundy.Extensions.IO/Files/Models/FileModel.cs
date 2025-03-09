/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.IO;
// ReSharper disable ConvertToPrimaryConstructor

namespace AlastairLundy.Extensions.IO.Files
{
    /// <summary>
    /// 
    /// </summary>
    public class FileModel
    {
        public string FileName { get; protected set; }

        public string FileExtension { get; protected set; }
    
        public string FilePath { get; protected set; }
    

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public FileModel(string filePath)
        {
            FileExtension = Path.HasExtension(filePath) ? Path.GetExtension(filePath) : string.Empty;

            FileName = Path.GetFileNameWithoutExtension(filePath);
        
            FilePath = Path.GetFullPath(filePath);
        }

    
        public override string ToString()
        {
            return FilePath;
        }
    }
}