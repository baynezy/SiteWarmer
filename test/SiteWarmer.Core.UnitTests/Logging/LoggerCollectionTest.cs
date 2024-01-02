namespace SiteWarmer.Core.UnitTests.Logging;

public class LoggerCollectionTest
{
    [Fact]
    public void Size_EmptyCollection_ReturnsZeroSize()
    {
        // arrange
        const int expectedSize = 0;
        var collection = new LoggerCollection();

        // assert
        collection.Size().Should().Be(expectedSize);
    }

    [Fact]
    public void Add_AddingLoggerToCollection_ReturnsCorrectSize()
    {
        // arrange
        const int expectedSize = 1;
        var collection = new LoggerCollection();
        var logger = new ConsoleLogger();

        // act
        collection.Add(logger);

        // assert
        collection.Size().Should().Be(expectedSize);
    }

    [Fact]
    public void LoggerCollection_ImplementsILogger()
    {
        // arrange
        var collection = new LoggerCollection();

        // assert
        collection.Should().BeAssignableTo<ILogger>();
    }

    [Fact]
    public void Log_CallingLog_CallsInternalLoggersLogMethodTheCorrectNumberOfTimes()
    {
        // arrange
        var collection = new LoggerCollection();
        var logger1 = Substitute.For<ILogger>();
        var logger2 = Substitute.For<ILogger>();

        logger1.Log(Arg.Any<Check>());
        logger2.Log(Arg.Any<Check>());

        // act
        collection.Add(logger1);
        collection.Add(logger2);

        collection.Log(new Check
        {
            Url = "https://www.google.com/",
            Status = 200
        });

        // assert
        logger1.Received(1).Log(Arg.Any<Check>());
        logger2.Received(1).Log(Arg.Any<Check>());
    }
}