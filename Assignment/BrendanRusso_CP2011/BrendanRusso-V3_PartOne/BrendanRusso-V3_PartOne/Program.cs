using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrendanRusso_V2_PartOne
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageHandler handler = new MOCKMessageHandler();
            handler.connectTo("4.5.6.7", 50000);
            handler.sendRequest("ID,Bob");
            handler.sendRequest("ID,John");
            Console.ReadKey();
        }
    }
}
