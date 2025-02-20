using System.IO;

namespace AlastairLundy.Extensions.IO.Files;

public interface IFilePathResolver
{
    /// <summary>
    /// Resolves the file path to a specific file if the file path is part of a PATH environment variable.
    /// </summary>
    /// <param name="inputFilePath">The input file path to resolve.</param>
    /// <param name="outputFilePath">The resolved file path if the file path is in the PATH environment variable; the original input file path otherwise.</param>
    /// <exception cref="FileNotFoundException">Thrown if the input file path is null or empty.</exception>
    void ResolveFilePath(string inputFilePath, out string outputFilePath);
}