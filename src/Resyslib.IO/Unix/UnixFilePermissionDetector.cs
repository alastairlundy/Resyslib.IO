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
using AlastairLundy.CliInvoke.Core.Abstractions;
using AlastairLundy.CliInvoke.Core.Builders;
using AlastairLundy.CliInvoke.Core.Builders.Abstractions;
using AlastairLundy.CliInvoke.Core.Primitives.Results;
using AlastairLundy.Resyslib.IO.Core.Primitives.Permissions;

using AlastairLundy.Resyslib.IO.Core.Unix;
using AlastairLundy.Resyslib.IO.Permissions;

namespace AlastairLundy.Resyslib.IO.Unix;

public class UnixFilePermissionDetector : IUnixFilePermissionDetector
{
    private readonly IProcessFactory _processFactory;

    public UnixFilePermissionDetector(IProcessFactory processFactory)
    {
        _processFactory = processFactory;
    }
    
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

        ProcessStartInfo startInfo = new ProcessStartInfo()
        {
            FileName = "stat",
            Arguments = $"-c %a ",
            RedirectStandardOutput = true,
            RedirectStandardInput = false,
            UseShellExecute = false,
            CreateNoWindow = true,
        };

        Process process = _processFactory.StartNew(startInfo);

        // We don't need to dispose of the Process afterwards since ProcessFactory does that for us.
        BufferedProcessResult processResult = await _processFactory.ContinueWhenExitBufferedAsync(process, 
            ProcessResultValidation.None, cancellationToken);

        string result = processResult.StandardOutput;

        if (processResult.ExitCode != 0)
        {
            return -1;
        }
        
        bool isInputValid = int.TryParse(result, out int output);

        return isInputValid ? output : -1;
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