namespace SiteWarmer.Core.Config;

/// <summary>
/// Configuration contained in a file
/// </summary>
public class FileConfig : IConfig
{
    /// <summary>
    /// Creates a new configuration from a file
    /// </summary>
    /// <param name="configPath">The path to the configuration file</param>
    public FileConfig(string configPath)
    {
        Checks = new List<Check>();
        Load(configPath);
    }

    /// <summary>
    /// The collection of Checks contained within the config
    /// </summary>
    public IList<Check> Checks { get; }

    private void Load(string configPath)
    {
        var fullPath = Path.GetFullPath(configPath);
        ParseFile(fullPath);
    }

    private void ParseFile(string fullPath)
    {
        using var file = new StreamReader(fullPath);

        while (file.ReadLine() is { } line)
        {
            AddCheck(line);
        }
    }

    private void AddCheck(string line)
    {
        var check = new Check {Url = line.Trim()};
        Checks.Add(check);
    }
}