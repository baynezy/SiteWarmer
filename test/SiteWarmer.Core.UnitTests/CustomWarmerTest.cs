namespace SiteWarmer.Core.UnitTests;

public class CustomWarmerTest
{
    [Fact]
    public async Task WarmAsync_WhenProvidingDelegate_ThenRunDelegate()
    {
        // arrange
        var mockConfig = MockConfig();
        var mockRequester = MockRequester();
        var mockLogger = MockLogger();
        var warmer = new CustomWarmer(mockConfig, mockRequester);

        // act
        await warmer.WarmAsync(mockLogger.Log);

        // assert
        await mockRequester.Received(2).CheckAsync(Arg.Any<Check>());
        mockLogger.Received(2).Log(Arg.Any<Check>());
    }

    [Fact]
    public async Task WarmAsync_WhenProvidingMultiCallDelegate_ThenRunDelegate()
    {
        // arrange
        var mockConfig = MockConfig();
        var mockRequester = MockRequester();
        var mockLogger = MockLogger();
        var warmer = new CustomWarmer(mockConfig, mockRequester);

        var tracker = 0;

        // act
        await warmer.WarmAsync(
            delegate(Check check)
            {
                mockLogger.Log(check);
                tracker++;
            }
        );

        // assert
        await mockRequester.Received(2).CheckAsync(Arg.Any<Check>());
        mockLogger.Received(2).Log(Arg.Any<Check>());
        tracker.Should().Be(2);
    }

    private static ILogger MockLogger()
    {
        var mockLogger = Substitute.For<ILogger>();
        mockLogger.Log(Arg.Any<Check>());
        return mockLogger;
    }

    private static IRequester MockRequester()
    {
        var mockRequester = Substitute.For<IRequester>();
        mockRequester.CheckAsync(Arg.Any<Check>());
        return mockRequester;
    }

    private static IConfig MockConfig()
    {
        var mockConfig = Substitute.For<IConfig>();
        mockConfig.Checks.Returns(new List<Check>
        {
            new()
            {
                Url = "https://www.google.co.uk/"
            },
            new()
            {
                Url = "https://www.github.com/"
            }
        });
        return mockConfig;
    }
}