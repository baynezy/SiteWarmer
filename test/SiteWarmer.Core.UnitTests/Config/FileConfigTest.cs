
namespace SiteWarmer.Core.UnitTests.Config;

public class FileConfigTest
{
    private readonly IConfig _config;

    public FileConfigTest()
    {
        var testPath = TestPath();
        _config = new FileConfig(testPath + "Data/urls.txt");
    }

    private static string TestPath()
    {
        return Environment.GetEnvironmentVariable("Test.Path") ?? @"..\..\..\";
    }

    [Fact]
    public void Checks_ReadingInCorrectFile_ReturnsChecksCorrectly()
    {
        // arrange
        var checks = _config.Checks;
        
        // assert
        checks.Count.Should().Be(4);
        checks[0].Url.Should().Be("https://www.yahoo.com/");
        checks[1].Url.Should().Be("https://www.google.com/");
        checks[2].Url.Should().Be("https://www.github.com/");
        checks[3].Url.Should().Be("https://www.bbc.co.uk/");
    }
}