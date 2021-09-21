using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    /// <summary>
    /// Mocking example with Moq.
    /// </summary>
    [TestFixture]
    internal class VideoServiceTests
    {


        private VideoService _videoService;
        private Mock<IFileReader> _fileReader;

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReader.Object);
        }


        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle("video.txt");

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void ReadVideoTitle_EmptyJsonArgument_ReturnRespectiveTitle()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("{}");

            var result = _videoService.ReadVideoTitle("video.txt");

            Assert.That(result, Is.EqualTo(null));
        }
    }
}
