using NUnit.Framework;
using System;
using TestNinja.Fundamentals;


namespace TestNinja.UnitTests
{
    [TestFixture]
    class ErrorLoggerTest
    {

        // Test void-methods
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            var logger = new ErrorLogger();

            logger.Log("a");

            Assert.That(logger.LastError, Is.EqualTo("a"));
        }

        // Test methods that throw exceptions
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            var logger = new ErrorLogger();

            Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
        }

        // DON'T Test private methods (implementation detail). Here just dummy implementation!
        [Test]
        public void OnErrorLog_WhenCalled_RaiseEvent()
        {
            var logger = new ErrorLogger();

            logger.OnErrorLogged(new Guid());

            Assert.That(true);
        }

    }
}
