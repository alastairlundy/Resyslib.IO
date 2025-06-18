/*
    Resyslib.IO
    Copyright (c) 2024-2025 Alastair Lundy

    This Source Code Form is subject to the terms of the Mozilla Public
    License, v. 2.0. If a copy of the MPL was not distributed with this
    file, You can obtain one at http://mozilla.org/MPL/2.0/.
 */

using System;
using System.Collections.Generic;
using System.Text;

using AlastairLundy.DotExtensions.Collections.Generic.Enumerables;

using AlastairLundy.Resyslib.IO.Internal.Localizations;

// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedType.Global

namespace AlastairLundy.Resyslib.IO.Permissions;

public static class UnixFilePermissionConverter
{
    /// <summary>
    /// Converts a Unix file permission in symbolic notation to Octal notation.
    /// </summary>
    /// <param name="symbolicNotation">The symbolic notation to be converted to octal notation.</param>
    /// <returns>The octal notation equivalent of the specified symbolic notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
    public static string ToNumericNotation(string symbolicNotation)
    {
        StringBuilder stringBuilder = new StringBuilder();

        if (symbolicNotation.Length == 10)
        {
            IEnumerable<IEnumerable<char>> parts = symbolicNotation.ToLower()
                .SplitByCount(3);

            foreach (IEnumerable<char> substring in parts)
            {
                string s = string.Join("", substring);

                switch (s)
                {
                    case "---":
                        stringBuilder.Append($"0");
                        break;
                    case "--x":
                        stringBuilder.Append($"1");
                        break;
                    case "-w-":
                        stringBuilder.Append($"2");
                        break;
                    case "-wx":
                        stringBuilder.Append($"3");
                        break;
                    case "r--":
                        stringBuilder.Append($"4");
                        break;
                    case "r-x":
                        stringBuilder.Append($"5");
                        break;
                    case "rw-":
                        stringBuilder.Append($"6");
                        break;
                    case "rwx":
                        stringBuilder.Append($"7");
                        break;
                    default:
                        throw new ArgumentException(
                            Resources.Exceptions_Permissions_InvalidSymbolicNotation.Replace("{x}",
                                symbolicNotation));
                }
            }

            return stringBuilder.ToString();
        }

        throw new ArgumentException(
            Resources.Exceptions_Permissions_InvalidSymbolicNotation.Replace("{x}", symbolicNotation));
    }

    /// <summary>
    /// Converts a Unix file permission in octal notation to symbolic notation.
    /// </summary>
    /// <param name="numericNotation">The octal notation to be converted to symbolic notation.</param>
    /// <returns>The symbolic notation equivalent of the specified octal notation.</returns>
    /// <exception cref="ArgumentException">Thrown if the octal notation specified is invalid.</exception>
    public static string ToSymbolicNotation(string numericNotation)
    {
        if (numericNotation.Length < 3 || numericNotation.Length > 4 ||
            int.TryParse(numericNotation, out int result) == false)
        {
            throw new ArgumentException(
                Resources.Exceptions_Permissions_InvalidNumericNotation.Replace("{x}", numericNotation));
        }

        StringBuilder stringBuilder = new StringBuilder();

        char[] parts = numericNotation.ToLower().ToCharArray();

        foreach (char c in parts)
        {
            switch (c)
            {
                case '0':
                    stringBuilder.Append("---");
                    break;
                case '1':
                    stringBuilder.Append("--x");
                    break;
                case '2':
                    stringBuilder.Append("-w-");
                    break;
                case '3':
                    stringBuilder.Append("-wx");
                    break;
                case '4':
                    stringBuilder.Append("r--");
                    break;
                case '5':
                    stringBuilder.Append("r-x");
                    break;
                case '6':
                    stringBuilder.Append("rw-");
                    break;
                case '7':
                    stringBuilder.Append("rwx");
                    break;
                default:
                    throw new ArgumentException(
                        Resources.Exceptions_Permissions_InvalidNumericNotation.Replace("{x}",
                            numericNotation));
            }
        }

        return stringBuilder.ToString();
    }
}