using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMessagingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            UsersHandler handler = new UsersHandler();
            while (true)
            {
                string request = Console.ReadLine();

                string response = handler.handleRequest(request);
                Console.WriteLine("response: " + response);
            }
        }
    }
}
