using CommandLine;

namespace SiteWarmer.App
{
	class Options
	{
		[Option("i", "input", Required = true, HelpText = "This is the input file containing the urls to warm")]
		public string Inputfile { get; set; }

		[Option("l", "logerror", Required = false, DefaultValue = false, HelpText = "If you wish to create a log file of all the non-passing checks")]
		public bool LogError { get; set; }
	}
}
