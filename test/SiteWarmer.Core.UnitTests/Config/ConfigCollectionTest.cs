
namespace SiteWarmer.Core.UnitTests.Config;

public class ConfigCollectionTest
{
    [Fact]
    public void ConfigCollection_ImplementsIConfig()
    {
        // arrange
        var config = new ConfigCollection();

        // assert
        config.Should().BeAssignableTo<IConfig>();
    }

    [Fact]
    public void Size_WhenConfigIsEmpty_ThenReturnZero()
    {
        // arrange
        var config = new ConfigCollection();

        // assert
        config.Size().Should().Be(0);
    }

    [Fact]
    public void Add_WhenAddingMultipleConfigs_ThenSizeShouldBeEqualToThat()
    {
        // arrange
        var config = new ConfigCollection();
        const int expectedSize = 2;

        // act
        config.Add(Substitute.For<IConfig>());
        config.Add(Substitute.For<IConfig>());

        // assert
        config.Size().Should().Be(expectedSize);
    }

    [Fact]
    public void Checks_WhenUsingMultipleConfigs_ThenChecksShouldReturnTheCombinationOfTheConfigs()
    {
        // arrange
        var configCollection = new ConfigCollection();
        var configOne = Substitute.For<IConfig>();

        const int expectedChecks = 4;

        configOne.Checks.Returns(
            new List<Check>
            {
                new()
                {
                    Url = "https://www.google.com/"
                },
                new()
                {
                    Url = "https://www.bbc.co.uk/"
                }
            }
        );

        var configTwo = Substitute.For<IConfig>();

        configTwo.Checks.Returns(
            new List<Check>
            {
                new()
                {
                    Url = "https://www.bing.com/"
                },
                new()
                {
                    Url = "https://www.yahoo.com/"
                }
            }
        );

        // act
        configCollection.Add(configOne);
        configCollection.Add(configTwo);

        var checks = configCollection.Checks;

        // assert
        checks.Count.Should().Be(expectedChecks);
        checks[0].Url.Should().Be("https://www.google.com/");
        checks[1].Url.Should().Be("https://www.bbc.co.uk/");
        checks[2].Url.Should().Be("https://www.bing.com/");
        checks[3].Url.Should().Be("https://www.yahoo.com/");
    }
}