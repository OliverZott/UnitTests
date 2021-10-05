using System.Net;

namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        bool DownloadFile(string url, string destination);
    }

    public class FileDownloader : IFileDownloader
    {
        public bool DownloadFile(string url, string destination)
        {
            var client = new WebClient();
            try
            {
                client.DownloadFile(url, destination);
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }


}
