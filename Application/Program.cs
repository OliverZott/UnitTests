namespace TestNinja.Mocking
{
    internal class Program
    {

        public static void Main()
        {
            var videoService = new VideoService();

            // In reality a DI-Framework is used instead of "new"íng object!
            var title = videoService.ReadVideoTitle("video.txt");
            System.Console.WriteLine(title);
        }
    }
}
