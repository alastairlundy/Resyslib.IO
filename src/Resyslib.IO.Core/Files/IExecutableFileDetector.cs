/*
    Resyslib.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace AlastairLundy.Resyslib.IO.Core.Files
{
    /// <summary>
    /// 
    /// </summary>
    public interface IExecutableFileDetector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool IsFileExecutable(string filename);
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        bool DoesFileHaveExecutablePermissions(string filename);
    }
}