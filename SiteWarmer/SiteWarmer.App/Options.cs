using CommandLine;
using CommandLine.Text;

namespace SiteWarmer.App
{
	class Options
	{
		[Option("i", "input", Required = true, HelpText = "This is the input file containing the urls to warm")]
		public string Inputfile { get; set; }

		[Option("l", "logerror", Required = false, DefaultValue = false, HelpText = "If you wish to create a log file of all the non-passing checks")]
		public bool LogError { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
                {
                    Heading = new HeadingInfo("Site Warmer", "3.0.0"),
                    Copyright = new CopyrightInfo("Simon Baynes", 2013),
                    AdditionalNewLineAfterOption = true,
                    AddDashesToOption = true
                };
            help.AddPreOptionsLine("--------------------");
            help.AddPreOptionsLine("Usage: -i [InputFilePath]");

            help.AddOptions(this);
            return help;
        }
	}
}
