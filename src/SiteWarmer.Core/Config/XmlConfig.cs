using System.Xml.Linq;

namespace SiteWarmer.Core.Config;

/// <summary>
/// Create a configuration from an XML file
/// </summary>
public class XmlConfig : IConfig
{
    private readonly string _configPath;

    /// <summary>
    /// Create a new configuration
    /// </summary>
    /// <param name="configPath">Path to XML file containing config</param>
    public XmlConfig(string configPath)
    {
        Checks = new List<Check>();
        _configPath = configPath;
        Load();
    }

    private void Load()
    {
        var doc = XElement.Load(_configPath);

        ParseXml(doc);
    }

    private void ParseXml(XContainer doc)
    {
        var checks = doc.Elements("check")
            .Where(check => check.Element("url") != null)
            .Select(check => new Check
            {
                Url = ((string) check.Element("url"))!,
                ContentMatches =
                    (check.Elements("content")
                        .Where(content => content.Element("positive") != null)
                        .Select(content =>
                            new ContentMatch {Match = (string) content.Element("positive")!, Required = true}))
                    .Union(check.Elements("content")
                        .Where(content => content.Element("negative") != null)
                        .Select(content =>
                            new ContentMatch {Match = ((string) content.Element("negative"))!, Required = false}))
                    .ToList()
            });

        Checks = checks.ToList();
    }

    /// <summary>
    /// The collection of Checks contained within the config
    /// </summary>
    public IList<Check> Checks { get; private set; }
}