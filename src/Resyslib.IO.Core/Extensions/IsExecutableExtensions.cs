/*
    Resyslib.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

#if NET5_0_OR_GREATER
using System;
#endif

using System.IO;
using System.Runtime.InteropServices;

namespace AlastairLundy.Resyslib.IO.Core.Extensions;

/// <summary>
/// 
/// </summary>
public static class IsExecutableExtensions
{
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static bool IsExecutableExtension(this string filePath)
    {
        bool output = false;
        string fileExtension = Path.GetExtension(filePath);
        
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            output = fileExtension switch
            {
                ".exe" => true,
                ".msi" => true,
                ".appx" => true,
                ".com" => true,
                ".bat" => true,
                ".cmd" => true,
                ".jar" => true,
                _ => false
            };
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            output = fileExtension switch
            {
                ".appimage" => true,
                ".deb" => true,
                ".rpm" => true,
                ".so" => true,
                ".o" => true,
                ".out" => true,
                ".bin" => true,
                ".elf" => true,
                ".mod" => true,
                ".axf" => true,
                ".ko" => true,
                ".prx" => true,
                ".puff" => true,
                ".jar" => true,
                ".sh" => true,
                _ => false
            };
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            output = fileExtension switch
            {
                ".kext" => true,
                ".pkg" => true,
                ".app" => true,
                ".so" => true,
                ".o" => true,
                ".out" => true,
                ".bin" => true,
                ".elf" => true,
                ".mod" => true,
                ".axf" => true,
                ".ko" => true,
                ".prx" => true,
                ".puff" => true,
                ".jar" => true,
                ".sh" => true,
                _ => false
            };
        }
#if NETCOREAPP_3_1_OR_GREATER || NET5_0_OR_GREATER
        else if(OperatingSystem.IsFreeBSD() || RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            output = fileExtension switch
            {
                ".appimage" => true,
                ".so" => true,
                ".o" => true,
                ".out" => true,
                ".bin" => true,
                ".elf" => true,
                ".mod" => true,
                ".axf" => true,
                ".ko" => true,
                ".prx" => true,
                ".puff" => true,
                ".jar" => true,
                ".sh" => true,
                _ => false
            };
        }
#endif
#if NET5_0_OR_GREATER
        else if(OperatingSystem.IsAndroid())
        {
            output = fileExtension switch
            {
                ".apk" => true,
                ".aab" => false,
                ".so" => true,
                ".o" => true,
                ".out" => true,
                ".bin" => true,
                ".elf" => true,
                ".mod" => true,
                ".axf" => true,
                ".ko" => true,
                ".prx" => true,
                ".puff" => true,
                ".jar" => true,
                ".sh" => true,
                _ => false
            };
        }
        else if (OperatingSystem.IsIOS())
        {
            output = fileExtension switch
            {
                ".ipa" => true,
                _ => false
            };
        }
#endif
        
        return output;
    }
}