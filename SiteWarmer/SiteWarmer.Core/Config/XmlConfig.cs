using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SiteWarmer.Core.Config
{
	public class XmlConfig : IConfig
	{
		private readonly string _configPath;

		public XmlConfig(string configPath)
		{
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
			var checks = from check in doc.Elements("check")
			             select new Check
				             {
					             Url = (string)check.Element("url"),
								 ContentMatches = (from content in check.Elements("content") select new ContentMatch
									 {
										 Match = (string)content.Element("positive"),
										 Required = true
									 }).Union(from content in check.Elements("content") select new ContentMatch
										 {
											 Match = (string)content.Element("negative"),
											 Required = false
										 })
										 .Where(m => m.Match != null)
										 .ToList()
				             };

			Checks = checks.Where(c => !string.IsNullOrEmpty(c.Url)).ToList();
		}

		public IList<Check> Checks { get; private set; }
	}
}