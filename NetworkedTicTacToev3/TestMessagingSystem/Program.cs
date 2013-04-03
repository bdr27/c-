using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMessagingSystem
{
    class Program
    {
        private static UsersHandler userHandler = new UsersHandler();
        static void Main(string[] args)
        {
            string userInput;
            Console.WriteLine("Enter string q to quit");
            userInput = Console.ReadLine();
            while (!userInput.ToLower().Equals("q"))
            {
                Console.WriteLine("Response: " + userHandler.handleRequest(userInput));
                userInput = Console.ReadLine();
            }
        }
    }
}
