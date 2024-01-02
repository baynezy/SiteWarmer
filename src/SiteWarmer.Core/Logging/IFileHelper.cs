namespace SiteWarmer.Core.Logging;

/// <summary>
/// File helper for working with text files
/// </summary>
public interface IFileHelper
{
    /// <summary>
    /// Writes a line to the file
    /// </summary>
    /// <param name="fileName">The file we wish to write to</param>
    /// <param name="text">The text we wish to write in the line</param>
    void WriteLine(string fileName, string text);

    /// <summary>
    /// Check it a file exists
    /// </summary>
    /// <param name="fileName">Path to file</param>
    /// <returns></returns>
    bool FileExists(string fileName);

    /// <summary>
    /// Create a new file
    /// </summary>
    /// <param name="fileName">Path to file</param>
    void CreateFile(string fileName);
}