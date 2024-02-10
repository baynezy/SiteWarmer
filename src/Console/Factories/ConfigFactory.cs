using System.Collections;
using SiteWarmer.Core.Config;

namespace Console.Factories;

public class ConfigFactory
{
    public static IConfig Create(List<string> input)
    {
        IConfig config;

        if (HasMultipleFiles(input))
        {
            var configCollection = new ConfigCollection();

            foreach (var file in input)
            {
                configCollection.Add(GetConfig(file));
            }

            config = configCollection;
        }
        else
        {
            config = GetConfig(input.First());
        }

        return config;
    }

    private static bool HasMultipleFiles(ICollection input)
    {
        return input.Count > 1;
    }

    private static IConfig GetConfig(string file)
    {
        IConfig config;
			
        if (IsXmlFile(file))
        {
            config = new XmlConfig(file);
        }
        else
        {
            config = new FileConfig(file);
        }

        return config;
    }

    private static bool IsXmlFile(string inputFile)
    {
        return inputFile.EndsWith(".xml");
    }
}