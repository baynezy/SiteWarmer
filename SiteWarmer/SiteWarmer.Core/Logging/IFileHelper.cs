namespace SiteWarmer.Core.Logging
{
	public interface IFileHelper
	{
		/// <summary>
		/// Writes a line to the file
		/// </summary>
		/// <param name="fileName">The file we wish to write to</param>
		/// <param name="text">The text we wish to write in the line</param>
		void WriteLine(string fileName, string text);
		bool FileExists(string fileName);
		void CreateFile(string fileName);
	}
}
