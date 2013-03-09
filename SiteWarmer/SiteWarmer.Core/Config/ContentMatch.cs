namespace SiteWarmer.Core.Config
{
	public class ContentMatch
	{
		/// <summary>
		/// The string match we are checking on
		/// </summary>
		public string Match { get; set; }

		/// <summary>
		/// Whether the content is required to be there or required to not be there
		/// </summary>
		public bool Required { get; set; }
	}
}
