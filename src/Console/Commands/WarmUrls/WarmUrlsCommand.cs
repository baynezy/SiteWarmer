using System.Xml;
using Cocona;
using Console.Factories;
using SiteWarmer.Core.Comms;

namespace Console.Commands.WarmUrls;

internal class WarmUrlsCommand
{
    public async Task WarmAsync(
        [Option('i', Description = "This is a comma separated list of input files containing the urls to warm")] List<string> input,
        [Option('l', Description = "If you wish to create a log file of all the non-passing checks")] bool? logError,
        [Option('r', Description = "The number of times to retry non-successful http calls")] int? retries
        )
    {
        try
        {
            var config = ConfigFactory.Create(input);
            var logger = LoggerFactory.Create(logError);
            var requester = new Requester();
            var warmer = WarmerFactory.Create(retries, config, requester, logger);

            await warmer.WarmAsync();
        }
        catch (XmlException e)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Invalid XML Config File");
            System.Console.WriteLine("-----------------------");
            System.Console.WriteLine(e.Message);
        }
        catch (FileNotFoundException e)
        {
            System.Console.WriteLine("");
            System.Console.WriteLine("Could not find the file specified. {0}", input);
            System.Console.WriteLine("-----------------------");
            System.Console.WriteLine(e.Message);
        }
    }
}