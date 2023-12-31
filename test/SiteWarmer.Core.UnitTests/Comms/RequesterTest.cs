namespace SiteWarmer.Core.UnitTests.Comms;

public class RequesterTest
{
    private readonly Requester _requester;

    public RequesterTest()
    {
        _requester = new Requester();
    }

    [Fact]
    public async Task CheckAsync_CallsExistingPage_Returns200StatusCode()
    {
        // arrange
        var check = new Check { Url = "https://httpstat.us/200" };

        // act
        await _requester.CheckAsync(check);

        // assert
        check.Status.Should().Be(Check.Ok);
    }

    [Fact]
    public async Task CheckAsync_CallsMissingPage_Returns404StatusCode()
    {
        // arrange
        var check = new Check { Url = "https://httpstat.us/404" };

        // act
        await _requester.CheckAsync(check);

        // assert
        check.Status.Should().Be(Check.NotFound);
    }

    [Fact]
    public async Task CheckAsync_CallsExistingPage_ReturnsCorrectContent()
    {
        // arrange
        var check = new Check { Url = "https://httpstat.us/200" };

        // act
        await _requester.CheckAsync(check);

        // assert
        AssertContainsString("200 OK", check.Source);
    }

    private static void AssertContainsString(string expected, string actual)
    {
        var match = actual.IndexOf(expected, StringComparison.Ordinal);
        
        match.Should().BeGreaterOrEqualTo(0, $"'{expected}' was not present in the source");
    }
}