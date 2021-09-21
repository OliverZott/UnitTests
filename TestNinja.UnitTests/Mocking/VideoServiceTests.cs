using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    internal class VideoServiceTests
    {

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnErrorMessage()
        {
            var videoService = new VideoService();
            videoService.FileReader = new FakeFileReader();

            var result = videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);

        }
    }
}
