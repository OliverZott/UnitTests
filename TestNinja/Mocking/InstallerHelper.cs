namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private readonly IFileDownloader _fileDownloader;
        private readonly string _setupDestinationFile = "test";

        public InstallerHelper(IFileDownloader fileDownloader = null)
        {
            _fileDownloader = fileDownloader ?? new FileDownloader();
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            var url = $"http://example.com/{customerName}/{installerName}";

            return _fileDownloader.DownloadFile(url, _setupDestinationFile);
        }
    }
}