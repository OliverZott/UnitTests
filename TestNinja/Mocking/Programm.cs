using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    internal class Programm
    {

        public static void Main()
        {
            var videoService = new VideoService();

            // In reality a DI-Framework is used instead of "new"íng object!
            var title = videoService.ReadVideoTitle(new FileReader());
        }
    }
}
