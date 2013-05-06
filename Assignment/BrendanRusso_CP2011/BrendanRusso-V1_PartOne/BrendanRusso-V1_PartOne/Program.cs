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
            string menu = "1. Test ConnectTo()\n2. Test SendRequest\n3. (Q)uit";
            Console.WriteLine(menu);
            string choice = Console.ReadLine();
            while (!choice.ToLower().Equals("q"))
            {
                switch (choice)
                {
                    case "1":
                        Console.Write("IP Address: ");
                        string ipAddress = Console.ReadLine();
                        Console.Write("Port Number: ");
                        int portNumber = Int32.Parse(Console.ReadLine());
                        handler.connectTo(ipAddress, portNumber);
                        break;
                    case "2":
                        Console.Write("Request: ");
                        string request = Console.ReadLine();
                        handler.sendRequest(request);
                        break;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
                Console.WriteLine(menu);
                choice = Console.ReadLine();
            }
            Console.WriteLine("Thank you for testing");
            Console.ReadKey();
        }
    }
}
