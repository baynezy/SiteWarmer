using NSubstitute;
using SiteWarmer.Core;
using SiteWarmer.Core.Comms;

namespace Console.UnitTests.Factories;

public class WarmerFactoryTest
{
    [Fact]
    public void Create_WhenRetriesAreZero_ReturnWarmer()
    {
        // arrange
        var config = Substitute.For<IConfig>();
        var requester = Substitute.For<IRequester>();
        var logger = Substitute.For<ILogger>();

        // act
        var warmer = WarmerFactory.Create(0, config, requester, logger);

        // assert
        warmer.Should().BeAssignableTo<Warmer>();
    }

    [Fact]
    public void Create_WhenRetriesAreOne_ReturnWarmer()
    {
        // arrange
        var config = Substitute.For<IConfig>();
        var requester = Substitute.For<IRequester>();
        var logger = Substitute.For<ILogger>();

        // act
        var warmer = WarmerFactory.Create(1, config, requester, logger);

        // assert
        warmer.Should().BeAssignableTo<Warmer>();
    }

    [Fact]
    public void Create_WhenRetriesAreMoreThanOne_ReturnRepeatWarmer()
    {
        // arrange
        var config = Substitute.For<IConfig>();
        var requester = Substitute.For<IRequester>();
        var logger = Substitute.For<ILogger>();

        // act
        var warmer = WarmerFactory.Create(2, config, requester, logger);

        // assert
        warmer.Should().BeAssignableTo<RepeatWarmer>();
    }
}