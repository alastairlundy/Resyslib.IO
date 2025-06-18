/*
    Resyslib.IO
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System.IO;
using System.Security.AccessControl;

#if NET5_0_OR_GREATER
using System.Runtime.Versioning;
#endif

using System.Threading.Tasks;

using AlastairLundy.Resyslib.IO.Core.Primitives.Permissions;
using AlastairLundy.Resyslib.IO.Core.Windows;

namespace AlastairLundy.Resyslib.IO.Windows;

public class WindowsFilePermissionDetector : IWindowsFilePermissionDetector
{
    #if NET5_0_OR_GREATER
    [SupportedOSPlatform("windows")]
    #endif
    public Task<WindowsFilePermission> GetWindowsFilePermissionAsync(string filePath)
    {
        FileInfo fileInfo = new FileInfo(filePath);

        FileSecurity security = fileInfo.GetAccessControl();
}