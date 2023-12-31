namespace SiteWarmer.Core.Config;

/// <summary>
/// This is a logical representation of a Url and its content matches
/// as well as status and source Html
/// </summary>
public class Check
{
    public const int Ok = 200;
    public const int NotFound = 404;

    /// <summary>
    /// The Url to check
    /// </summary>
    public required string Url { get; init; }

    /// <summary>
    /// The status code returned when the Url is hit
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// The Html source returned from the Url
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// A collection of positive and negative content matches
    /// </summary>
    public IList<ContentMatch> ContentMatches { get; init; } = new List<ContentMatch>();

    /// <summary>
    /// Can tell you if the check passed its checks
    /// </summary>
    /// <returns>Whether the Url passed the check</returns>
    public bool Passed()
    {
        return Status == Ok && CheckContent();
    }

    public bool CheckContent()
    {
        var passed = true;
			
        foreach (var match in ContentMatches)
        {
            passed = CheckMatch(match);

            if (!passed)
            {
                break;
            }
        }

        return passed;
    }

    private bool CheckMatch(ContentMatch match)
    {
        var found = Source.Contains(match.Match);

        return found == match.Required;
    }
}