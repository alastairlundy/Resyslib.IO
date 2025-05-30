using System.IO;
using System.Runtime.Versioning;
using AlastairLundy.Resyslib.IO.Core.Files;
using AlastairLundy.Resyslib.IO.Files;
using Extensions.IO.Tests.Data;

namespace Extensions.IO.Tests.Files;

public class FilePathResolverTests
{
    private readonly IFilePathResolver _filePathResolver;
    
    public FilePathResolverTests()
    {
        _filePathResolver = new FilePathResolver();
    }

    [Fact]
    [SupportedOSPlatform(("windows"))]
    public void Resolve_Path_To_Cmd()
    {
        //Arrange 
        string path = ExeHelpers.CmdExePath;
        
        //Act
        _filePathResolver.ResolveFilePath(path, out string resolvedFilePath);
        
        //Assert
        Assert.True(File.Exists(resolvedFilePath));
    }
}