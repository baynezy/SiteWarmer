namespace Console.UnitTests.Factories;

public class LoggerFactoryTest
{
    [Fact]
    public void Create_WhenLoggingErrors_ShouldReturnLoggerCollection()
    {
        // arrange
        var logger = LoggerFactory.Create(true);

        // act
        var collection = (LoggerCollection)logger;

        // assert
        logger.Should().BeAssignableTo<LoggerCollection>();
        collection.Size().Should().Be(2);
    }

    [Fact]
    public void Create_WhenNotLoggingErrors_ShouldReturnConsoleLogger()
    {
        // arrange
        var logger = LoggerFactory.Create(false);

        // assert
        logger.Should().BeAssignableTo<ConsoleLogger>();
    }
}