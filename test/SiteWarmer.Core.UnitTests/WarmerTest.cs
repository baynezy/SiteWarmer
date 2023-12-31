namespace SiteWarmer.Core.UnitTests;

public class WarmerTest
{
    [Fact]
    public async Task WarmAsync_ReceivesPopulatedConfig_CallsCorrectMethods()
    {
        // arrange
        var config = Substitute.For<IConfig>();
        config.Checks.Returns(
            new List<Check>
            {
                new()
                {
                    Url = "https://www.yahoo.com/"
                },
                new()
                {
                    Url = "https://www.google.com/"
                },
                new()
                {
                    Url = "https://www.github.com/"
                },
                new()
                {
                    Url = "https://www.bbc.co.uk/"
                }
            }
        );
        var requester = Substitute.For<IRequester>();
        await requester.CheckAsync(Arg.Any<Check>());
        var logger = Substitute.For<ILogger>();
        logger.Log(Arg.Any<Check>());
        var warmer = new Warmer(config, requester, logger);
        
        // act
        await warmer.WarmAsync();
        
        // assert
        await requester.Received(4).CheckAsync(Arg.Any<Check>());
        logger.Received(4).Log(Arg.Any<Check>());
        logger.Received(1).Close();
    }
}