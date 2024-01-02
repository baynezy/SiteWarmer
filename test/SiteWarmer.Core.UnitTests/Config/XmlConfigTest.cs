using System.Xml;

namespace SiteWarmer.Core.UnitTests.Config;

public class XmlConfigTest
{
	private readonly string _testPath = TestPath();

	private static string TestPath()
	{
		return Environment.GetEnvironmentVariable("Test.Path") ?? @"..\..\..\";
	}

	[Fact]
	public void XmlConfig_ImplementsIConfig()
	{
		// arrange
		var config = new XmlConfig(_testPath + "Data/urls.xml");

		// assert
		config.Should().BeAssignableTo<IConfig>();
	}

	[Fact]
	public void Checks_ReadingInCorrectFile_ReturnsChecksCorrectly()
	{
		// arrange
		const int expectedNumberOfChecks = 4;
		const int expectedNumberOfNegativeContentMatches = 1;
		const int expectedNumberOfPositiveContentMatches = 1;
		var config = new XmlConfig(_testPath + "Data/urls.xml");

		// act
		var checks = config.Checks;

		// assert
		checks.Count.Should().Be(expectedNumberOfChecks);
		
		checks[0].Url.Should().Be("https://www.yahoo.com/");
		var matches = checks[0].ContentMatches;
		matches.Count(m => m.Required).Should().Be(expectedNumberOfPositiveContentMatches);
		matches.Count(m => !m.Required).Should().Be(expectedNumberOfNegativeContentMatches);
		
		checks[1].Url.Should().Be("https://www.google.com/");
		matches = checks[1].ContentMatches;
		matches.Count(m => m.Required).Should().Be(expectedNumberOfPositiveContentMatches);
		matches.Count(m => !m.Required).Should().Be(0);

		checks[2].Url.Should().Be("https://www.github.com/");
		matches = checks[2].ContentMatches;
		matches.Count(m => m.Required).Should().Be(expectedNumberOfPositiveContentMatches);
		matches.Count(m => !m.Required).Should().Be(0);

		checks[3].Url.Should().Be("https://www.bbc.co.uk/");
		matches = checks[3].ContentMatches;
		matches.Count(m => m.Required).Should().Be(expectedNumberOfPositiveContentMatches);
		matches.Count(m => !m.Required).Should().Be(0);
	}

	[Fact]
	public void Checks_ReadingInEmptyFile_ReturnsEmptyListOfChecks()
	{
		// arrange
		const int expectedNumberOfChecks = 0;
		var config = new XmlConfig(_testPath + "Data/empty.xml");

		// act
		var checks = config.Checks;

		// assert
		checks.Count.Should().Be(expectedNumberOfChecks);
	}

	[Fact]
	public void Checks_ReadingInFileWithEmptyMatches_ReturnsEmptyListOfMatches()
	{
		// arrange
		const int expectedNumberOfMatches = 0;
		var config = new XmlConfig(_testPath + "Data/empty_items.xml");

		// act
		var checks = config.Checks;

		// assert
		checks[0].ContentMatches.Count.Should().Be(expectedNumberOfMatches);
	}

	[Fact]
	public void XmlConfig_WhenTheXmlIsInvalid_ThenThrowXmlException()
	{
		var act = () =>
			{
				// ReSharper disable once ObjectCreationAsStatement
#pragma warning disable CA1806
				new XmlConfig(_testPath + "Data/invalid.xml");
#pragma warning restore CA1806
			};
			
		act.Should().Throw<XmlException>();
	}
}