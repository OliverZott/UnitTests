using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private readonly IFileReader _fileReader;
        private readonly IVideoRepository _videoRepository;

        // DI via:
        //   - Parameter
        //   - Propertie
        //   - Constructor

        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
        {
            _fileReader = fileReader ?? new FileReader();
            _videoRepository = videoRepository ?? new VideoRepository();
        }

        public string ReadVideoTitle(string path)
        {
            var str = _fileReader.Read(path);  // Decouple via DI of interface in constructor.
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _videoRepository.GetUnprocessedVideos();  // Decouple via DI of interface in constructor.
            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);

        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}