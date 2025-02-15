using System;
using System.Collections.Generic;

namespace AlastairLundy.Extensions.IO.Files.Concatenation;

public interface IFileConcatenator
{
    /// <summary>
    /// Concatenated the contents of files in the style of the Unix Cat command.
    /// </summary>
    /// <param name="files">The files to be concatenated.</param>
    /// <returns>the concatenated files as an IEnumerable of strings.</returns>
    IEnumerable<string> ConcatenateFilesToEnumerable(IEnumerable<string> files);

    /// <summary>
    /// Concatenates the contents of specified files and saves it to a new file.
    /// </summary>
    /// <param name="filePath">The path to save the new file to.</param>
    /// <param name="newFileName">The name of the new file to be created.</param>
    /// <param name="files">The files to be concatenated.</param>
    /// <exception cref="Exception">Thrown if an exception occurs when trying to save the file.</exception>
    void ConcatenateFilesToNewFile(string filePath, string newFileName, IEnumerable<string> files);
}