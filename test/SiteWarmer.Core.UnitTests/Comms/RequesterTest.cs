using System.Net;
using Mockly;

namespace SiteWarmer.Core.UnitTests.Comms;

public class RequesterTest
{
    private readonly Requester _sut;
    private readonly HttpMock _httpMock = new();

    private readonly Faker _faker = new();

    public RequesterTest()
    {
        _sut = new Requester(_httpMock.GetClient());
    }

    [Fact]
    public async Task CheckAsync_CallsExistingPage_Returns200StatusCode()
    {
        await CheckStatusCodeHandling(HttpStatusCode.OK, Check.Ok);
    }

    [Fact]
    public async Task CheckAsync_CallsMissingPage_Returns404StatusCode()
    {
        await CheckStatusCodeHandling(HttpStatusCode.NotFound, Check.NotFound);
    }

    private async Task CheckStatusCodeHandling(HttpStatusCode received, int expected)
    {
        // arrange
        var check = ArrangeRequester(received);

        // act
        await _sut.CheckAsync(check);

        // assert
        check.Status.Should()
            .Be(expected);
    }

    [Fact]
    public async Task CheckAsync_CallsExistingPage_ReturnsCorrectContent()
    {
        // arrange
        var host = _faker.Internet.DomainName();
        var path = $"/{_faker.Random.AlphaNumeric(10)}";
        var checkUrl = $"https://{host}{path}";
        var check = new Check {Url = checkUrl};

        const string expectedContent = "200 OK";
        _httpMock.ForGet()
            .ForHttps()
            .ForHost(host)
            .WithPath(path)
            .RespondsWithContent(expectedContent);

        // act
        await _sut.CheckAsync(check);

        // assert
        AssertContainsString(expectedContent, check.Source);
    }

    private Check ArrangeRequester(HttpStatusCode received)
    {
        var host = _faker.Internet.DomainName();
        var path = $"/{_faker.Random.AlphaNumeric(10)}";
        var checkUrl = $"https://{host}{path}";
        var check = new Check {Url = checkUrl};

        _httpMock.ForGet()
            .ForHttps()
            .ForHost(host)
            .WithPath(path)
            .RespondsWithStatus(received);
        return check;
    }

    private static void AssertContainsString(string expected, string actual)
    {
        var match = actual.IndexOf(expected, StringComparison.Ordinal);

        match.Should()
            .BeGreaterOrEqualTo(0, $"'{expected}' was not present in the source");
    }
}