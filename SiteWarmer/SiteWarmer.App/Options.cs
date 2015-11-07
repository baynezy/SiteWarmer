using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace SiteWarmer.App
{
	public class Options
	{
		[OptionList('i', "input", Required = false, Separator = ',', HelpText = "This is a comma seperated list of input files containing the urls to warm")]
		public IList<string> Inputfiles { get; set; }

		[Option('l', "logerror", Required = false, DefaultValue = false, HelpText = "If you wish to create a log file of all the non-passing checks")]
		public bool LogError { get; set; }

		[Option('r', "retries", Required = false, DefaultValue = 0, HelpText = "The number of times to retry non-successful http calls")]
		public int Retries { get; set; }

		[Option('v', "version", Required = false, DefaultValue = false, HelpText = "Outputs the product version number.")]
		public bool Version { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            var help = new HelpText
                {
                    Heading = new HeadingInfo("Site Warmer", App.Version.GetAppVersion()),
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
