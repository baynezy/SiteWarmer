namespace SiteWarmer.Core.UnitTests.Config;

public class CheckTest
{
	private readonly Faker _faker = new();

	[Fact]
	public void Passed_WhenStatusCodeIs200AndThereAreNoContentChecks_ThenReturnTrue()
	{
		// arrange
		var check = new Check
		{
			Status = Check.Ok,
			Url = _faker.Internet.Url()
		};

		// assert
		check.Passed().Should().BeTrue();
	}

	[Fact]
	public void Passed_WhenStatusCodeIsNot200_ThenReturnFalse()
	{
		// arrange
		var check = new Check
		{
			Status = 500,
			Url = _faker.Internet.Url()
		};

		// assert
		check.Passed().Should().BeFalse();
	}

	[Fact]
	public void Passed_WhenStatusCodeIs200AndAContentMatchFails_ThenReturnFalse()
	{
		// arrange
		var check = new Check
		{
			Status = Check.Ok,
			Source = "This is the test source.",
			ContentMatches = new List<ContentMatch>
			{
				new()
				{
					Match = "match",
					Required = true
				}
			},
			Url = _faker.Internet.Url()
		};

		// assert
		check.Passed().Should().BeFalse();
	}

	[Fact]
	public void Passed_WhenStatusCodeIs200AndAContentMatchPasses_ThenReturnTrue()
	{
		// arrange
		var check = new Check
		{
			Status = Check.Ok,
			Source = "This is the test source containing a match.",
			ContentMatches = new List<ContentMatch>
			{
				new()
				{
					Match = "match",
					Required = true
				}
			},
			Url = _faker.Internet.Url()
		};

		// assert
		check.Passed().Should().BeTrue();
	}

	[Fact]
	public void Passed_WhenStatusCodeIs200AndANegativeContentMatchFails_ThenReturnTrue()
	{
		// arrange
		var check = new Check
		{
			Status = Check.Ok,
			Source = "This is the test source.",
			ContentMatches = new List<ContentMatch>
			{
				new()
				{
					Match = "match",
					Required = false
				}
			},
			Url = _faker.Internet.Url()
		};

		// assert
		check.Passed().Should().BeTrue();
	}

	[Fact]
	public void Passed_WhenStatusCodeIs200AndANegativeContentMatchIsPresent_ThenReturnFalse()
	{
		// arrange
		var check = new Check
		{
			Status = Check.Ok,
			Source = "This is the test source, but has match.",
			ContentMatches = new List<ContentMatch>
			{
				new()
				{
					Match = "match",
					Required = false
				}
			},
			Url = _faker.Internet.Url()
		};

		// assert
		check.Passed().Should().BeFalse();
	}
}