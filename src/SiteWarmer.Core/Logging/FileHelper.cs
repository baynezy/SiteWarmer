namespace SiteWarmer.Core.Logging;

/// <summary>
/// Utility class for working with text files
/// </summary>
public class FileHelper : IFileHelper
{
    /// <inheritdoc/>
    public void WriteLine(string fileName, string text)
    {
        using var writer = File.AppendText(fileName);
        writer.WriteLine(text);
    }

    /// <inheritdoc/>
    public bool FileExists(string fileName)
    {
        return File.Exists(fileName);
    }

    /// <inheritdoc/>
    public void CreateFile(string fileName)
    {
        FileStream? file = null;

        try
        {
            file = File.Create(fileName);
        }
        finally
        {
            file?.Close();
        }
    }
}