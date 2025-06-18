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

using System.Threading;
using System.Threading.Tasks;

using AlastairLundy.Resyslib.IO.Core.Primitives.Permissions;

namespace AlastairLundy.Resyslib.IO.Core.Unix;

public interface IUnixFilePermissionDetector
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    #if NET5_0_OR_GREATER
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("windows")]
    #endif
    Task<int> GetUnixFilePermissionAsOctalAsync(string filePath,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
#if NET5_0_OR_GREATER
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("windows")]
#endif
    Task<UnixFilePermission> GetUnixFilePermissionAsync(string filePath,
        CancellationToken cancellationToken = default);
}