using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using SiteWarmer.Core.Comms;
using SiteWarmer.Core.Config;
using SiteWarmer.Core.Logging;

namespace SiteWarmer.Core.Test
{
	[TestFixture]
	class WarmerTest
	{
	    [Test]
		public void Warm_ReceivesPopulatedConfig_CallsCorrectMethods()
		{
            var config = new Mock<IConfig>();
            config.Setup(m => m.Checks).Returns(
                    new List<Check>
                        {
                            new Check
                                {
                                    Url = "http://www.yahoo.com/"
                                },
                            new Check
                                {
                                    Url = "http://www.google.com/"
                                },
                            new Check
                                {
                                    Url = "http://www.github.com/"
                                },
                            new Check
                                {
                                    Url = "http://www.bbc.co.uk/"
                                }
                        }
                );
            var requester = new Mock<IRequester>();
            requester.Setup(m => m.Check(It.IsAny<Check>()));
            var logger = new Mock<ILogger>();
            logger.Setup(m => m.Log(It.IsAny<Check>()));
            var warmer = new Warmer(config.Object, requester.Object, logger.Object);
            
            warmer.Warm();

            config.Verify(f => f.Checks, Times.Once());

            requester.Verify(f => f.Check(It.IsAny<Check>()), Times.Exactly(4));

            logger.Verify(f => f.Log(It.IsAny<Check>()), Times.Exactly(4));

            logger.Verify(f => f.Close(), Times.Once());
		}
	}
}
