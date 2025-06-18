/*
    Resyslib.IO
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Diagnostics;
using System.IO;

#if NET5_0_OR_GREATER
using System.Runtime.Versioning;
#else
using OperatingSystem = Polyfills.OperatingSystemPolyfill;
#endif

using System.Threading;
using System.Threading.Tasks;

using AlastairLundy.Resyslib.IO.Core.Primitives.Permissions;

using AlastairLundy.Resyslib.IO.Core.Unix;
using AlastairLundy.Resyslib.IO.Permissions;

namespace AlastairLundy.Resyslib.IO.Unix;

public class UnixFilePermissionDetector : IUnixFilePermissionDetector
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="PlatformNotSupportedException"></exception>
    /// <exception cref="FileNotFoundException"></exception>
#if NET5_0_OR_GREATER
    [SupportedOSPlatform("macos")]
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    [UnsupportedOSPlatform("windows")]
#endif
    public async Task<int> GetUnixFilePermissionAsOctalAsync(string filePath,
        CancellationToken cancellationToken = default)
    {
        if (OperatingSystem.IsLinux() == false &&
            OperatingSystem.IsMacOS() == false &&
            OperatingSystem.IsFreeBSD() == false)
        {
            throw new PlatformNotSupportedException();
        }

        if (File.Exists(filePath) == false)
        {
            throw new FileNotFoundException(filePath);
        }
        
        Process process = new Process()
        {
            StartInfo = new ProcessStartInfo()
            {
                FileName = "stat",
                Arguments = $"-c %a {Path.GetFullPath(filePath)}",
                RedirectStandardOutput = true,
                RedirectStandardInput = false,
                UseShellExecute = false,
                CreateNoWindow = true,
            }
        };
        
        process.Start();
        
        await process.WaitForExitAsync(cancellationToken);
        
        string output = await process.StandardOutput.ReadToEndAsync(cancellationToken);

        if (process.ExitCode != 0)
        {
            return -1;
        }
        
        process.Dispose();
        
        bool isInputValid = int.TryParse(output, out int result);

        if (isInputValid)
        {
            return result;
        }
        else
        {
            return -1;
        }
    }

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
    public async Task<UnixFilePermission> GetUnixFilePermissionAsync(string filePath,
        CancellationToken cancellationToken = default)
    {
        int permission = await GetUnixFilePermissionAsOctalAsync(filePath, cancellationToken);
        
        return UnixFilePermissionParser.ParseNumericNotation(permission.ToString());
    }
}