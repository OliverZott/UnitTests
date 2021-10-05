using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Test]
        public void DownloadInstaller_CorrectInput_ReturnsTrue()
        {
            _fileDownloader.Setup(x => x.DownloadFile("http://example.com/name/surname", "test")).Returns(true);

            var result = _installerHelper.DownloadInstaller("name", "surname");

            Assert.That(result, Is.True);
        }
    }
}
