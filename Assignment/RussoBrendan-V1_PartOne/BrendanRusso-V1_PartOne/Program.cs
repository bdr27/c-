using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrendanRusso_V1_PartOne
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageHandler handler = new MOCKMessageHandler();
            Console.WriteLine("Get current response (NONE): " + handler.getResponse());
            string testAddress = "125.4.3.2";
            int testPortNumber = 50676;
            handler.connectTo(testAddress, testPortNumber);
            Console.WriteLine(String.Format("Connect to IP address: {0}, port number: {1} ({2}): {3}", testAddress, testPortNumber, "VALID", handler.getResponse()));
            Console.WriteLine("Press any key to close...");
            Console.ReadKey();
        }
    }
}
