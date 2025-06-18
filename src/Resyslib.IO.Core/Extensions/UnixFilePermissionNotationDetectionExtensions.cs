/*
    Resyslib.IO
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

namespace AlastairLundy.Resyslib.IO.Core.Extensions;

public static class UnixFilePermissionNotationDetectionExtensions
{
    
    /// <summary>
    /// Detects whether a Unix Octal file permission notation is valid.
    /// </summary>
    /// <param name="notation">The numeric notation to be compared.</param>
    /// <returns>True if a valid unix file permission octal notation has been provided; false otherwise.</returns>
    public static bool IsNumericNotation(this string notation)
    {
        if (notation.Length is >= 3 and <= 4 || int.TryParse(notation, out int result) == false) 
            return false;
        
        return result is >= 0 and <= 777 && notation.Length is >= 3 and <= 4;
    }
    
    /// <summary>
    /// Detects whether a Unix symbolic file permission is valid.
    /// </summary>
    /// <param name="notation">The symbolic notation to be compared.</param>
    /// <returns>True if a valid unix file permission symbolic notation has been provided; false otherwise.</returns>
    public static bool IsSymbolicNotation(this string notation)
    {
        if (notation.Length != 10) 
            return false;
        
#if NET6_0_OR_GREATER
            return notation switch
            {
                "----------" or
                    "---x--x--x" or
                    "--w--w--w-" or
                    "--wx-wx-wx" or
                    "-r--r--r--" or
                    "-r-xr-xr-x" or
                    "-rw-rw-rw-" or
                    "-rwx------" or
                    "-rwxr-----" or
                    "-rwxrwx---" or
                    "-rwxrwxrwx" => true,
                _ => false
            };
#else
        return notation == "----------" ||
               notation == "---x--x--x" ||
               notation == "--w--w--w-" ||
               notation == "--wx-wx-wx" ||
               notation == "-r--r--r--" ||
               notation == "-r-xr-xr-x" ||
               notation == "-rw-rw-rw-" ||
               notation == "-rwx------" ||
               notation == "-rwxr-----" ||
               notation == "-rwxrwx---" ||
               notation == "-rwxrwxrwx";
#endif
    }
}