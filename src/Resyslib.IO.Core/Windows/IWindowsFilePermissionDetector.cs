/*
    Resyslib.IO
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

#if NET5_0_OR_GREATER
using System.Runtime.Versioning;
#endif

using System.Threading.Tasks;
using AlastairLundy.Resyslib.IO.Core.Primitives.Permissions;

namespace AlastairLundy.Resyslib.IO.Core.Windows;

public interface IWindowsFilePermissionDetector
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    #if NET5_0_OR_GREATER
    [SupportedOSPlatform("windows")]
    #endif
    Task<WindowsFilePermission> GetWindowsFilePermissionAsync(string filePath);
}