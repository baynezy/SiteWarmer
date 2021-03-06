﻿using Moq;
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

			Assert.That(logger, Is.InstanceOf<ILogger>());
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

		private static void LoggedAnErroringCheckThenClosed(Mock<IFileHelper> fileHelper)
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

		[Test]
		public void Close_WhenThereIsOneCheckWhichFailedButEventuallyPassed_ThenItShouldNotCreateTheFile()
		{
			var helper = new Mock<IFileHelper>();
			helper.Setup(m => m.CreateFile(It.IsAny<string>()));
			helper.Setup(m => m.FileExists(It.IsAny<string>())).Returns(false);
			helper.Setup(m => m.WriteLine(It.IsAny<string>(), It.IsAny<string>()));

			var logger = new FileLogger(helper.Object);

			var check = new Check
				{
					Url = "http://www.google.com/",
					Status = 500
				};

			logger.Log(check);

			check.Status = Check.Ok;

			logger.Log(check);

			logger.Close();

			helper.Verify(f => f.FileExists(It.IsAny<string>()), Times.Never());
			helper.Verify(f => f.CreateFile(It.IsAny<string>()), Times.Never());
			helper.Verify(f => f.WriteLine(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
		}

		[Test]
		public void Close_WhenACheckFailsButEventuallyPasses_ThenItShouldNotGetLogged()
		{
			const string passingUrl = "http://www.google.com/";
			const string failingUrl = "http://www.yahoo.com/";
			
			var helper = new Mock<IFileHelper>();
			helper.Setup(m => m.CreateFile(It.IsAny<string>()));
			helper.Setup(m => m.FileExists(It.IsAny<string>())).Returns(false);
			helper.Setup(m => m.WriteLine(It.IsAny<string>(), It.IsAny<string>()));

			var logger = new FileLogger(helper.Object);

			var check = new Check
			{
				Url = passingUrl,
				Status = 500
			};

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

			helper.Verify(f => f.FileExists(It.IsAny<string>()), Times.Once());
			helper.Verify(f => f.CreateFile(It.IsAny<string>()), Times.Once());
			helper.Verify(f => f.WriteLine(It.IsAny<string>(), failingUrl), Times.Once());
			helper.Verify(f => f.WriteLine(It.IsAny<string>(), passingUrl), Times.Never());
		}
	}
}
