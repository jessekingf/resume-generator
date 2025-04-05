namespace Common.IO;

using System.IO;

/// <summary>
/// Provides functionality for interacting with the file system.
/// </summary>
public class FileSystem : IFileSystem
{
    /// <summary>
    /// Determines whether the specified directory exists.
    /// </summary>
    /// <param name="path">The path to test.</param>
    /// <returns>Whether the directory exists.</returns>
    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    /// <summary>
    /// Determines whether the specified file exists.
    /// </summary>
    /// <param name="path">The file to check.</param>
    /// <returns>Whether the file exists.</returns>
    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    /// <summary>
    /// Opens a text file, reads all the text in the file into a string, and then closes the file.
    /// </summary>
    /// <param name="path">The file to open for reading.</param>
    /// <returns>A string containing all the text in the file.</returns>
    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }

    /// <summary>
    /// Creates a new file, writes the specified string to the file, and then closes the file.
    /// If the target file already exists, it is overwritten.
    /// </summary>
    /// <param name="path">The file to write to.</param>
    /// <param name="contents">The string to write to the file.</param>
    public void WriteAllText(string path, string contents)
    {
        File.WriteAllText(path, contents);
    }

    /// <summary>
    /// Copies an existing file to a new file.
    /// </summary>
    /// <param name="sourceFileName">Name of the source file.</param>
    /// <param name="destFileName">The name of the destination file. This cannot be a directory or an existing file.</param>
    /// <param name="overwrite">Whether to overwrite the destination file if it already exists.</param>
    public void CopyFile(string sourceFileName, string destFileName, bool overwrite = false)
    {
        File.Copy(sourceFileName, destFileName, overwrite);
    }
}
