using System;
using System.IO;

using AlastairLundy.Resyslib.IO.Core.Extensions;

using AlastairLundy.Resyslib.IO.Internal.Localizations;

namespace AlastairLundy.Resyslib.IO.Permissions;

public static class UnixFileModeParser
{
    #if NET8_0_OR_GREATER 
    /// <summary>
    /// Parse a Unix file permission in octal notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The octal notation to be parsed.</param>
    /// <returns>The UnixFileMode enum equivalent to the specified octal notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid octal notation is specified.</exception>
    public static UnixFileMode ParseNumericNotation(string permissionNotation)
    {
        if (permissionNotation.IsNumericNotation() == false
            || int.TryParse(permissionNotation, out int result) == false)
            throw new ArgumentException(Resources.Exceptions_Permissions_InvalidSymbolicNotation);

        switch (result)
        {

            default:
            //    throw new ArgumentException(Resources.Exceptions_Permissions_InvalidNumericNotation);
        }
    }
    
        /// <summary>
    /// Parse a Unix file permission in symbolic notation to a UnixFileMode enum.
    /// </summary>
    /// <param name="permissionNotation">The symbolic notation to be compared.</param>
    /// <returns>The UnixFileMode enum equivalent to the specified symbolic notation.</returns>
    /// <exception cref="ArgumentException">Thrown if an invalid symbolic notation is specified.</exception>
    public static UnixFileMode ParseSymbolicNotation(string permissionNotation)
    {
        if (permissionNotation.IsSymbolicNotation())
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
    public static bool TryParseSymbolicNotation(string permissionNotation, out UnixFileMode? fileMode)
    {
        try
        {
            fileMode = ParseSymbolicNotation(permissionNotation);
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
        bool isNumericNotation = permissionNotation.IsNumericNotation();
        bool isSymbolicNotation = permissionNotation.IsSymbolicNotation();

        try
        {
            if (isNumericNotation && isSymbolicNotation == false)
            {
                fileMode = ParseNumericNotation(permissionNotation);
                return true;
            }
            else if (isSymbolicNotation && isNumericNotation == false)
            {
                fileMode = ParseSymbolicNotation(permissionNotation);
                return true;
            }
            else
            {
                fileMode = null;
                return false;
            }
        }
        catch
        {
            fileMode = null;
            return false;
        }
    }
#endif
}