using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class HtmlFormatterTests
    {

        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseStringWithStringElement()
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("Test");

            // Specific
            Assert.That(result, Is.EqualTo("<strong>Test</strong>"));

            // More general
            Assert.That(result, Does.StartWith("<strong>"));
            Assert.That(result, Does.EndWith("</strong>"));
            Assert.That(result, Does.Contain("Test").IgnoreCase);
        }
    }
}
