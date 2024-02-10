namespace SiteWarmer.Core.Config;

/// <summary>
/// Content we are looking to find or not find in the pages being requested
/// </summary>
public class ContentMatch
{
    /// <summary>
    /// The string match we are checking on
    /// </summary>
    public required string Match { get; init; }

    /// <summary>
    /// Whether the content is required to be there or required to not be there
    /// </summary>
    public required bool Required { get; init; }
}