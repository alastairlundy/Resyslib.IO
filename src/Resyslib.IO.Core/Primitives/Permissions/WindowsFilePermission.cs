/*
    Resyslib.IO
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;

namespace AlastairLundy.Resyslib.IO.Core.Primitives.Permissions;

/// <summary>
/// 
/// </summary>
[Flags]
public enum WindowsFilePermission
{
    SystemFullControl,
    SystemModify,
    SystemRead,
    SystemWrite,
    SystemReadAndExecute,
    SystemListFolderContents,
    UserFullControl,
    UserModify,
    UserRead,
    UserWrite,
    UserReadAndExecute,
    UserListFolderContents,
    GroupFullControl,
    GroupModify,
    GroupRead,
    GroupWrite,
    GroupReadAndExecute,
    GroupListFolderContents,
}