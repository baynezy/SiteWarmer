
namespace SiteWarmer.Core.UnitTests.Config;

public class ManualConfigTest
{
    [Fact]
    public void ManualConfig_ImplementsIConfig()
    {
        // arrange
        var config = new ManualConfig();

        // assert
        config.Should().BeAssignableTo<IConfig>();
    }

    [Fact]
    public void Constructor_WhenInitialisingWithAListOfUrlStrings_ThenShouldCreateChecks()
    {
        // arrange
        var urls = new List<string>
        {
            "https://www.google.co.uk/",
            "https://www.bbc.co.uk/"
        };
        var config = new ManualConfig(urls);

        // act
        var checks = config.Checks;

        // assert
        checks.Count.Should().Be(urls.Count);
        checks[0].Url.Should().Be(urls[0]);
        checks[1].Url.Should().Be(urls[1]);
    }

    [Fact]
    public void Constructor_WhenInitialisingWithAListOfChecks_ThenShouldStoreChecks()
    {
        // arrange
        var urls = new List<Check>
        {
            new()
            {
                Url = "https://www.github.com/",
            },
            new()
            {
                Url = "https://www.bbc.co.uk/"
            }
        };

        var config = new ManualConfig(urls);

        // act
        var checks = config.Checks;

        // assert
        checks.Count.Should().Be(urls.Count);
        checks[0].Url.Should().Be(urls[0].Url);
        checks[1].Url.Should().Be(urls[1].Url);
    }

    [Fact]
    public void Add_WhenAddingChecks_ThenChecksPropertyShouldReturnTheCollection()
    {
        // arrange
        var config = new ManualConfig();

        const int expectedCount = 2;
        const string url1 = "https://www.bbc.co.uk/";
        const string url2 = "https://www.google.co.uk/";

        config.Add(new Check { Url = url1 });
        config.Add(new Check { Url = url2 });

        // act
        var checks = config.Checks;

        // assert
        checks.Count.Should().Be(expectedCount);
        checks[0].Url.Should().Be(url1);
        checks[1].Url.Should().Be(url2);
    }
}