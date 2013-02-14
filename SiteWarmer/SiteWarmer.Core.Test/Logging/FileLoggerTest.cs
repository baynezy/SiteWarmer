using Moq;
using NUnit.Framework;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core.Test.Logging
{
	[TestFixture]
	class FileLoggerTest
	{
		[Test]
		public void FileLogger_ImplementsILogger()
		{
			var logger = new FileLogger(new Mock<IFileHelper>().Object);

			Assert.IsInstanceOf<ILogger>(logger);
		}

		[Test]
		public void Log_WhenLoggingWithErroringCheck_ThereAreNoFileOperationsCalled()
		{
			var fileHelper = new Mock<IFileHelper>();
			var logger = new FileLogger(fileHelper.Object);

			logger.Log(new Check
				{
					Url = "http://www.google.com/",
					Status = 500
				});

			VerifyNoFileOperations(fileHelper);
		}


		[Test]
		public void Log_WhenLoggingWithOkCheck_ThereAreNoFileOperationsCalled()
		{
			var fileHelper = new Mock<IFileHelper>();
			var logger = new FileLogger(fileHelper.Object);

			logger.Log(new Check
			{
				Url = "http://www.google.com/",
				Status = Check.Ok
			});

			VerifyNoFileOperations(fileHelper);
		}

		[Test]
		public void Close_WhenOnlyAnOkCheckHasBeenLogged_NoFileOperationsShouldBeCalled()
		{
			var fileHelper = new Mock<IFileHelper>();
			var logger = new FileLogger(fileHelper.Object);

			logger.Log(new Check
			{
				Url = "http://www.google.com/",
				Status = Check.Ok
			});

			logger.Close();

			VerifyNoFileOperations(fileHelper);
		}

		[Test]
		public void Close_WhenOnlyOkChecksHaveBeenLogged_NoFileOperationsShouldBeCalled()
		{
			var fileHelper = new Mock<IFileHelper>();
			var logger = new FileLogger(fileHelper.Object);

			logger.Log(new Check
			{
				Url = "http://www.google.com/",
				Status = Check.Ok
			});

			logger.Log(new Check
			{
				Url = "http://www.google.co.uk/",
				Status = Check.Ok
			});

			logger.Close();

			VerifyNoFileOperations(fileHelper);
		}

		private static void VerifyNoFileOperations(Mock<IFileHelper> fileHelper)
		{
			fileHelper.Verify(f => f.CreateFile(It.IsAny<string>()), Times.Never());
			fileHelper.Verify(f => f.FileExists(It.IsAny<string>()), Times.Never());
			fileHelper.Verify(f => f.WriteLine(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
		}

		[Test]
		public void Close_WhenSomeChecksErrored_ThenCheckFileExists()
		{
			var fileHelper = new Mock<IFileHelper>();
			LoggedAnErroringCheckThenClosed(fileHelper);

			fileHelper.Verify(f => f.FileExists(It.IsAny<string>()), Times.Once());
		}

		[Test]
		public void Close_WhenSomeChecksErroredAndFileExists_ThenDoNotCreateFile()
		{
			var fileHelper = new Mock<IFileHelper>();
			fileHelper.Setup(m => m.FileExists(It.IsAny<string>())).Returns(true);
			LoggedAnErroringCheckThenClosed(fileHelper);

			fileHelper.Verify(f => f.CreateFile(It.IsAny<string>()), Times.Never());
		}

		[Test]
		public void Close_WhenSomeChecksErroredAndLogFileDoesNotExist_ThenCreateFile()
		{
			var fileHelper = new Mock<IFileHelper>();

			LoggedAnErroringCheckThenClosed(fileHelper);

			fileHelper.Verify(f => f.CreateFile(It.IsAny<string>()), Times.Once());
		}

		[Test]
		public void Close_WhenLoggingASingleErroringCheck_ThenOnlyWriteOneLine()
		{
			var fileHelper = new Mock<IFileHelper>();

			LoggedAnErroringCheckThenClosed(fileHelper);

			fileHelper.Verify(f => f.WriteLine(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
		}

		private void LoggedAnErroringCheckThenClosed(Mock<IFileHelper> fileHelper)
		{
			var logger = new FileLogger(fileHelper.Object);

			logger.Log(new Check
			{
				Url = "http://www.google.com/",
				Status = 500
			});

			logger.Close();
		}

		[Test]
		public void Close_WhenLoggingSeveralErroringCheck_ThenWriteOneLinePerFailure()
		{
			var fileHelper = new Mock<IFileHelper>();
			var logger = new FileLogger(fileHelper.Object);

			logger.Log(new Check
			{
				Url = "http://www.google.com/",
				Status = 500
			});

			logger.Log(new Check
			{
				Url = "http://www.google.co.uk/",
				Status = 500
			});

			logger.Close();

			fileHelper.Verify(f => f.WriteLine(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
		}
	}
}
