﻿/*
    Resyslib.IO
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */


using System;
using System.IO;
using AlastairLundy.Resyslib.IO.Internal.Localizations;

// ReSharper disable MemberCanBePrivate.Global

namespace AlastairLundy.Resyslib.IO.Permissions;

public static class UnixFilePermissionParser
{
#if NET8_0_OR_GREATER 
    /// <summary>
    /// Parse a Unix file permission in octal notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The octal notation to be parsed.</param>
    /// <returns>The UnixFileMode enum equivalent to the specified octal notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid octal notation is specified.</exception>
    // TODO: Rename to ParseNumericNotation in v8.0
    public static UnixFileMode ParseNumericValue(string permissionNotation)
    {
        if (IsNumericNotation(permissionNotation) 
            && int.TryParse(permissionNotation, out int result))
        {
            return result switch
            {
                0 => UnixFileMode.None,
                111 => UnixFileMode.UserExecute,
                222 => UnixFileMode.UserWrite,
                333 => UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                444 => UnixFileMode.UserRead,
                555 => UnixFileMode.UserRead & UnixFileMode.UserExecute,
                666 => UnixFileMode.UserRead & UnixFileMode.UserWrite,
                700 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                740 => UnixFileMode.UserExecute & UnixFileMode.UserWrite & UnixFileMode.UserRead &
                       UnixFileMode.GroupRead,
                770 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                       UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute,
                777 => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                       UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute &
                       UnixFileMode.OtherRead & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute,
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation);
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in Octal notation to a UnixFileMode enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission octal notation to be parsed.</param>
    /// <param name="fileMode">The UnixFileMode equivalent value to the octal notation if a valid octal notation was specified; null otherwise.</param>
    /// <returns>True if a valid Unix file permission octal notation was specified; false otherwise.</returns>
    public static bool TryParseNumericValue(string permissionNotation, out UnixFileMode? fileMode)
    {
        try
        {
            fileMode = ParseNumericValue(permissionNotation);
            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }

    /// <summary>
    /// Parse a Unix file permission in symbolic notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The symbolic notation to be compared.</param>
    /// <returns>The UnixFileMode enum equivalent to the specified symbolic notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
    // TODO: Rename to ParseSymbolicNotation in v8
    public static UnixFileMode ParseSymbolicValue(string permissionNotation)
    {
        if (IsSymbolicNotation(permissionNotation))
        {
            return permissionNotation.ToLower() switch
            {
                "----------" => UnixFileMode.None,
                "---x--x--x" => UnixFileMode.UserExecute,
                "--w--w--w-" => UnixFileMode.UserWrite,
                "--wx-wx-wx" => UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                "-r--r--r--" => UnixFileMode.UserRead,
                "-r-xr-xr-x" => UnixFileMode.UserRead & UnixFileMode.UserExecute,
                "-rw-rw-rw-" => UnixFileMode.UserRead & UnixFileMode.UserWrite,
                "-rwx------" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute,
                "-rwxr-----" => UnixFileMode.UserExecute & UnixFileMode.UserWrite & UnixFileMode.UserRead &
                                UnixFileMode.GroupRead,
                "-rwxrwx---" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                                UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute,
                "-rwxrwxrwx" => UnixFileMode.UserRead & UnixFileMode.UserWrite & UnixFileMode.UserExecute &
                                UnixFileMode.GroupRead & UnixFileMode.GroupWrite & UnixFileMode.GroupExecute &
                                UnixFileMode.OtherRead & UnixFileMode.OtherWrite & UnixFileMode.OtherExecute,
                _ => throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation)
            };
        }

        throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation);
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in Symbolic notation to a UnixFileMode enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="fileMode">The UnixFileMode equivalent value to the symbolic notation if a valid symbolic notation was specified; null otherwise.</param>
    /// <returns>True if a valid Unix file permission symbolic notation was specified; false otherwise.</returns>
    // TODO: Rename to TryParseSymbolicNotation in v8
    public static bool TryParseSymbolicValue(string permissionNotation, out UnixFileMode? fileMode)
    {
        try
        {
            fileMode = ParseSymbolicValue(permissionNotation);
            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }

    /// <summary>
    /// Attempts to parse a Unix file permission in either Numeric or Symbolic notation to a UnixFileMode enum. 
    /// </summary>
    /// <param name="permissionNotation">The Unix file permission symbolic notation to be parsed.</param>
    /// <param name="fileMode">The unix file mode if a valid permission notation was parsed; null otherwise.</param>
    /// <returns>True if the file mode notation was parsed successfully; false otherwise.</returns>
    public static bool TryParse(string permissionNotation, out UnixFileMode? fileMode)
    {
        bool isNumericNotation = IsNumericNotation(permissionNotation);
        bool isSymbolicNotation = IsSymbolicNotation(permissionNotation);

        try
        {
            if (isNumericNotation && !isSymbolicNotation)
            {
                fileMode = ParseNumericValue(permissionNotation);
            }
            else if (isSymbolicNotation && !isNumericNotation)
            {
                fileMode = ParseSymbolicValue(permissionNotation);
            }
            else
            {
                fileMode = UnixFileMode.UserRead & UnixFileMode.UserWrite;
            }

            return true;
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }
#endif
    
    /// <summary>
    /// Detects whether a Unix Octal file permission notation is valid.
    /// </summary>
    /// <param name="notation">The numeric notation to be compared.</param>
    /// <returns>True if a valid unix file permission octal notation has been provided; false otherwise.</returns>
    public static bool IsNumericNotation(string notation)
    {
        if (notation.Length == 4 && int.TryParse(notation, out int result))
        {
#if NET6_0_OR_GREATER
            return result switch
            {
                0 or 111 or 222 or 333 or 444 or 555 or 666 or 700 or 740 or 777 => true,
                _ => false
            };
            
            return result.len
#else
                return result == 0 ||
                       result == 111 ||
                       result == 222 ||
                       result == 333 ||
                       result == 444 ||
                       result == 555 ||
                       result == 666 ||
                       result == 700 ||
                       result == 740 ||
                       result == 777;
#endif
        }

        return false;
    }

    /// <summary>
    /// Detects whether a Unix symbolic file permission is valid.
    /// </summary>
    /// <param name="notation">The symbolic notation to be compared.</param>
    /// <returns>True if a valid unix file permission symbolic notation has been provided; false otherwise.</returns>
    public static bool IsSymbolicNotation(string notation)
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