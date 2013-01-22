using CommandLine;
using SiteWarmer.Core;

namespace SiteWarmer.App
{
	class Program
	{
		static void Main(string[] args)
		{
			var options = new Options();
			var parser = new CommandLineParser();

			parser.ParseArguments(args, options);

			var config = new FileConfig(options.Inputfile);
			var requester = new Requester();
			var logger = new ConsoleLogger();
			var warmer = new Warmer(config, requester, logger);

			warmer.Warm();
		}
	}
}
