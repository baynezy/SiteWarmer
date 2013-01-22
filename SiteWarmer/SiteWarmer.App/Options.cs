using CommandLine;

namespace SiteWarmer.App
{
	class Options
	{
		[Option("i", "input", Required = true, HelpText = "This is the input file containing the urls to warm")]
		public string Inputfile { get; set; }
	}
}
