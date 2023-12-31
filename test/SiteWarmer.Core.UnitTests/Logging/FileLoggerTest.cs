namespace SiteWarmer.Core.UnitTests.Logging;

public class FileLoggerTest
{
	[Fact]
	public void FileLogger_ImplementsILogger()
	{
		// arrange
		var logger = new FileLogger(Substitute.For<IFileHelper>());

		// assert
		logger.Should().BeAssignableTo<ILogger>();
	}

	[Fact]
	public void Log_WhenLoggingWithErroringCheck_ThereAreNoFileOperationsCalled()
	{
		// arrange
		var fileHelper = Substitute.For<IFileHelper>();
		var logger = new FileLogger(fileHelper);

		// act
		logger.Log(new Check
			{
				Url = "https://www.google.com/",
				Status = 500
			});

		// assert
		VerifyNoFileOperations(fileHelper);
	}


	[Fact]
	public void Log_WhenLoggingWithOkCheck_ThereAreNoFileOperationsCalled()
	{
		// arrange
		var fileHelper = Substitute.For<IFileHelper>();
		var logger = new FileLogger(fileHelper);

		// act
		logger.Log(new Check
		{
			Url = "https://www.google.com/",
			Status = Check.Ok
		});

		// assert
		VerifyNoFileOperations(fileHelper);
	}

	[Fact]
	public void Close_WhenOnlyAnOkCheckHasBeenLogged_NoFileOperationsShouldBeCalled()
	{
		// arrange
		var fileHelper = Substitute.For<IFileHelper>();
		var logger = new FileLogger(fileHelper);

		// act
		logger.Log(new Check
		{
			Url = "https://www.google.com/",
			Status = Check.Ok
		});

		logger.Close();

		// assert
		VerifyNoFileOperations(fileHelper);
	}

	[Fact]
	public void Close_WhenOnlyOkChecksHaveBeenLogged_NoFileOperationsShouldBeCalled()
	{
		// arrange
		var fileHelper = Substitute.For<IFileHelper>();
		var logger = new FileLogger(fileHelper);

		// act
		logger.Log(new Check
		{
			Url = "https://www.google.com/",
			Status = Check.Ok
		});

		logger.Log(new Check
		{
			Url = "https://www.google.co.uk/",
			Status = Check.Ok
		});

		logger.Close();

		// assert
		VerifyNoFileOperations(fileHelper);
	}

	private static void VerifyNoFileOperations(IFileHelper fileHelper)
	{
		fileHelper.Received(0).CreateFile(Arg.Any<string>());
		fileHelper.Received(0).FileExists(Arg.Any<string>());
		fileHelper.Received(0).WriteLine(Arg.Any<string>(), Arg.Any<string>());
	}

	[Fact]
	public void Close_WhenSomeChecksErrored_ThenCheckFileExists()
	{
		// arrange
		var fileHelper = Substitute.For<IFileHelper>();
		
		// act
		LoggedAnErroringCheckThenClosed(fileHelper);

		// assert
		fileHelper.Received(1).FileExists(Arg.Any<string>());
	}

	[Fact]
	public void Close_WhenSomeChecksErroredAndFileExists_ThenDoNotCreateFile()
	{
		// arrange
		var fileHelper = Substitute.For<IFileHelper>();
		fileHelper.FileExists(Arg.Any<string>()).Returns(true);

		// act
		LoggedAnErroringCheckThenClosed(fileHelper);

		// assert
		fileHelper.Received(0).CreateFile(Arg.Any<string>());
	}

	[Fact]
	public void Close_WhenSomeChecksErroredAndLogFileDoesNotExist_ThenCreateFile()
	{
		// arrange
		var fileHelper = Substitute.For<IFileHelper>();
		
		// act
		LoggedAnErroringCheckThenClosed(fileHelper);

		// assert
		fileHelper.Received(1).CreateFile(Arg.Any<string>());
	}

	[Fact]
	public void Close_WhenLoggingASingleErroringCheck_ThenOnlyWriteOneLine()
	{
		// arrange
		var fileHelper = Substitute.For<IFileHelper>();

		// act
		LoggedAnErroringCheckThenClosed(fileHelper);

		// assert
		fileHelper.Received(1).WriteLine(Arg.Any<string>(), Arg.Any<string>());
	}

	private static void LoggedAnErroringCheckThenClosed(IFileHelper fileHelper)
	{
		var logger = new FileLogger(fileHelper);

		logger.Log(new Check
		{
			Url = "https://www.google.com/",
			Status = 500
		});

		logger.Close();
	}

	[Fact]
	public void Close_WhenLoggingSeveralErroringCheck_ThenWriteOneLinePerFailure()
	{
		// arrange
		var fileHelper = Substitute.For<IFileHelper>();
		var logger = new FileLogger(fileHelper);

		// act
		logger.Log(new Check
		{
			Url = "https://www.google.com/",
			Status = 500
		});

		logger.Log(new Check
		{
			Url = "https://www.google.co.uk/",
			Status = 500
		});

		logger.Close();

		// assert
		fileHelper.Received(2).WriteLine(Arg.Any<string>(), Arg.Any<string>());
	}

	[Fact]
	public void Close_WhenThereIsOneCheckWhichFailedButEventuallyPassed_ThenItShouldNotCreateTheFile()
	{
		// arrange
		var helper = Substitute.For<IFileHelper>();
		helper.CreateFile(Arg.Any<string>());
		helper.FileExists(Arg.Any<string>()).Returns(false);
		helper.WriteLine(Arg.Any<string>(), Arg.Any<string>());

		var logger = new FileLogger(helper);

		var check = new Check
			{
				Url = "https://www.google.com/",
				Status = 500
			};

		// act
		logger.Log(check);

		check.Status = Check.Ok;

		logger.Log(check);

		logger.Close();
		
		// assert
		helper.Received(0).FileExists(Arg.Any<string>());
		helper.Received(0).CreateFile(Arg.Any<string>());
		helper.Received(0).WriteLine(Arg.Any<string>(), Arg.Any<string>());
	}

	[Fact]
	public void Close_WhenACheckFailsButEventuallyPasses_ThenItShouldNotGetLogged()
	{
		// arrange
		const string passingUrl = "https://www.google.com/";
		const string failingUrl = "https://www.yahoo.com/";
		
		var helper = Substitute.For<IFileHelper>();
		helper.CreateFile(Arg.Any<string>());
		helper.FileExists(Arg.Any<string>()).Returns(false);
		helper.WriteLine(Arg.Any<string>(), Arg.Any<string>());

		var logger = new FileLogger(helper);

		var check = new Check
		{
			Url = passingUrl,
			Status = 500
		};

		// act
		logger.Log(check);

		check.Status = Check.Ok;

		logger.Log(check);

		var failedCheck = new Check
			{
				Url = failingUrl,
				Status = 500
			};

		logger.Log(failedCheck);

		logger.Close();

		// assert
		helper.Received(1).FileExists(Arg.Any<string>());
		helper.Received(1).CreateFile(Arg.Any<string>());
		helper.Received(1).WriteLine(Arg.Any<string>(), failingUrl);
		helper.Received(0).WriteLine(Arg.Any<string>(), passingUrl);
	}
}