using System.Collections.Generic;
using System.IO;

namespace SiteWarmer.Core.Config
{
	/// <summary>
	/// Configuration contained in a file
	/// </summary>
	public class FileConfig : IConfig
	{
		private readonly IList<Check> _checks;

		/// <summary>
		/// Creates a new configuration from a file
		/// </summary>
		/// <param name="configPath">The path to the configuration file</param>
		public FileConfig(string configPath)
		{
			_checks = new List<Check>();
			Load(configPath);
		}

		/// <summary>
		/// The collection of Checks contained within the config
		/// </summary>
		public IList<Check> Checks
		{
			get { return _checks; }
		}

		private void Load(string configPath)
		{
			var fullPath = Path.GetFullPath(configPath);
			ParseFile(fullPath);
		}

		private void ParseFile(string fullPath)
		{
			using(var file = new StreamReader(fullPath))
			{
				string line;

				while ((line = file.ReadLine()) != null)
				{
					AddCheck(line);
				}
			}
		}

		private void AddCheck(string line)
		{
			var check = new Check {Url = line.Trim()};
			_checks.Add(check);
		}
	}
}
