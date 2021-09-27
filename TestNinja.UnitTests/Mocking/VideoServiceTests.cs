using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    /// <summary>
    /// Mocking example with Moq.
    /// 
    /// </summary>
    [TestFixture]
    internal class VideoServiceTests
    {

        private VideoService _videoService;
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _videoRepository;



        /// REMARKS: 
        /// 
        /// If there are too many mocks in the [SetUp] it could mean:
        ///     1. Class does too much and shpuld be split up!
        ///     2. We are mocking too much!
        ///     
        /// If dependencies (e.g. _fileReader) is only used in one method!  -->
        ///     --> Maybe constructor injection is not necessary!! Use parameter-injection instead.
        /// 
        ///     
        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoRepository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _videoRepository.Object);
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


        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsEmptyString()
        {
            _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(string.Empty));
        }


        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnsStringWithIdOfUnprocessedVideos()
        {
            var videos = new List<Video>()
            {
                new Video() { Id = 1},
                new Video() { Id = 2},
                new Video() { Id = 3}
            };

            _videoRepository.Setup(vr => vr.GetUnprocessedVideos()).Returns(videos);

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
