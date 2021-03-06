﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SiteWarmer.Core.Config
{
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
						 where check.Element("url") != null
			             select new Check
				             {
					             Url = (string)check.Element("url"),
								 ContentMatches = (from content in check.Elements("content")
												   where content.Element("positive") != null
												   select new ContentMatch
													{
														 Match = (string)content.Element("positive"),
														 Required = true
													})
													.Union(from content in check.Elements("content")
															  where content.Element("negative") != null
															  select new ContentMatch
													{
														Match = (string)content.Element("negative"),
														Required = false
													})
													.ToList()
				             };

			Checks = checks.ToList();
		}

		/// <summary>
		/// The collection of Checks contained within the config
		/// </summary>
		public IList<Check> Checks { get; private set; }
	}
}