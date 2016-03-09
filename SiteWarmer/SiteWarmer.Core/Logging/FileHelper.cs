using System.IO;

namespace SiteWarmer.Core.Logging
{
	/// <summary>
	/// Utility class for working with text files
	/// </summary>
	public class FileHelper : IFileHelper
	{
		public void WriteLine(string fileName, string text)
		{
			using (var writer = File.AppendText(fileName))
			{
				writer.WriteLine(text);
			}
		}

		public bool FileExists(string fileName)
		{
			return File.Exists(fileName);
		}

		public void CreateFile(string fileName)
		{
			FileStream file = null;
			
			try
			{
				file = File.Create(fileName);
			}
			finally
			{
				if (file != null)
				{
					file.Close();
				}
			}
		}
	}
}
