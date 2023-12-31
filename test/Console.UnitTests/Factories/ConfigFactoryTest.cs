namespace Console.UnitTests.Factories;

public class ConfigFactoryTest : BaseTest
{
    [Fact]
    public void Create_WhenConfigFileHasXmlExtension_ThenReturnXmlConfig()
    {
        // arrange
        var files = new List<string> { TestPath + "../SiteWarmer.Core.UnitTests/Data/urls.xml" };
        var config = ConfigFactory.Create(files);

        // assert
        config.Should().BeAssignableTo<XmlConfig>();
    }

    [Fact]
    public void Create_WhenConfigFileDoesNotHaveAnXmlExtension_ThenReturnFileConfig()
    {
        // arrange
        var files = new List<string> { TestPath + "../SiteWarmer.Core.UnitTests/Data/urls.txt" };
        var config = ConfigFactory.Create(files);

        // assert
        config.Should().BeAssignableTo<FileConfig>();
    }

    [Fact]
    public void Create_WhenThereAreMultipleFiles_ThenCreateConfigCollection()
    {
        // arrange
        const int expectedCount = 2;
        var files = new List<string>
        {
            TestPath + "../SiteWarmer.Core.UnitTests/Data/urls.txt",
            TestPath + "../SiteWarmer.Core.UnitTests/Data/urls.xml"
        };

        // act
        var config = ConfigFactory.Create(files) as ConfigCollection;

        // assert
        config.Should().NotBeNull();
        config!.Size().Should().Be(expectedCount);
    }
}