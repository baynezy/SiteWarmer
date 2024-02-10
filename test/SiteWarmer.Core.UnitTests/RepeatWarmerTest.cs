namespace SiteWarmer.Core.UnitTests;

public class RepeatWarmerTest
{
	private const string Url = "https://www.google.com/";

	[Fact]
	public void RepeatWarmer_CanBeInitialised()
	{
		// arrange
		var warmer = new RepeatWarmer(Substitute.For<IConfig>(), Substitute.For<IRequester>(), Substitute.For<ILogger>(), 1);

		// assert
		warmer.Should().BeAssignableTo<RepeatWarmer>();
	}

	[Fact]
	public async Task WarmAsync_WhenInitialCallFails_ThenRetry()
	{
		// arrange
		var config = BaseConfig();
		var requester = BaseRequester();
		var logger = BaseLogger();

		var warmer = new RepeatWarmer(config, requester, logger, 2);

		// act
		var results = await warmer.WarmAsync();

		// assert
		results.Count.Should().Be(1);
		results[0].Status.Should().Be(Check.Ok);
	}

	[Fact]
	public async Task WarmAsync_WhenInitialCallFails_ThenRetryOnlyTheNumberOfTimesWeAreConfiguredFor()
	{
		// arrange
		var config = BaseConfig();
		var requester = BaseRequester();
		var logger = BaseLogger();

		var warmer = new RepeatWarmer(config, requester, logger, 1);

		// act
		var results = await warmer.WarmAsync();

		// assert
		results.Count.Should().Be(1);
		results[0].Status.Should().NotBe(Check.Ok);
	}

	[Fact]
	public async Task WarmAsync_WhenCallPasses_ThenDoNotRetryUrl()
	{
		// arrange
		var config = Substitute.For<IConfig>();
		config.Checks.Returns(new List<Check>
			{
				new()
				{
						Url = "https://www.google.com"
					},
				new()
				{
						Url = "https://www.google.co.uk"
					}
			});

		var requester = Substitute.For<IRequester>();
		requester.When(m => m.CheckAsync(Arg.Any<Check>()))
		         .Do(c =>
		         {
			         c.Arg<Check>().Status = c.Arg<Check>().Url.Equals("https://www.google.com") ? 500 : Check.Ok;
		         });

		var logger = BaseLogger();

		var warmer = new RepeatWarmer(config, requester, logger, 2);

		// act
		await warmer.WarmAsync();

		// assert
		await requester.Received(3).CheckAsync(Arg.Any<Check>());
	}

	private static IConfig BaseConfig()
	{
		var config = Substitute.For<IConfig>();
		config.Checks.Returns(new List<Check>
			{
				new() {Url = Url}
			});

		return config;
	}

	private static IRequester BaseRequester()
	{
		var requester = Substitute.For<IRequester>();
		var statues = new List<int> { 500, Check.Ok };

		var count = 0;

		requester.When(m => m.CheckAsync(Arg.Any<Check>()))
			.Do(c => c.Arg<Check>().Status = statues[count++]);

		return requester;
	}

	private static ILogger BaseLogger()
	{
		var logger = Substitute.For<ILogger>();
		logger.Log(Arg.Any<Check>());
		logger.Close();

		return logger;
	}
}