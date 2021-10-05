using System.Net;

namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string url, string destination);
    }

    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string url, string destination)
        {
            var client = new WebClient();
            client.DownloadFile(url, destination);
        }
    }


}
