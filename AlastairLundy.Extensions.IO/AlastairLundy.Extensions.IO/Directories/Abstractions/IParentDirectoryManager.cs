/*
    AlastairLundy.Extensions.IO 
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System.IO;

namespace AlastairLundy.Extensions.IO.Directories.Abstractions;

public interface IParentDirectoryManager
{
#if NET8_0_OR_GREATER
    public bool TryCreateParentDirectory(string directoryPath, UnixFileMode unixFileMode);
    public void CreateParentDirectory(string parentDirectory, UnixFileMode unixFileMode);
#else
    public bool TryCreateParentDirectory(string directoryPath);
    public void CreateParentDirectory(string parentDirectory);
#endif
    
    void DeleteParentDirectory(string directory, bool deleteEmptyDirectory);

}