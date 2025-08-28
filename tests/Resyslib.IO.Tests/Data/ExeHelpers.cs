using System;
using System.IO;
using System.Runtime.InteropServices;
using AlastairLundy.Resyslib.IO.Core.Files;
using AlastairLundy.Resyslib.IO.Files;

namespace Resyslib.IO.Tests.Data;

public static class ExeHelpers
{

    public static string CmdExePath
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return $"{Environment.SystemDirectory}{Path.DirectorySeparatorChar}cmd.exe";
            }
                
            throw new PlatformNotSupportedException();
        }
    }
        
    public static string WinCalcExePath
    {
        get
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return $"{Environment.SystemDirectory}{Path.DirectorySeparatorChar}calc.exe";
            }
                
            throw new PlatformNotSupportedException();
        }
    }

}