using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestUDPServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            //setup the listener object
            var udpServer = new UdpClient(50000);

            //request handler loop
            var endPoint = new IPEndPoint(IPAddress.Any, 50000);
            while (true)
            {
                var bytes = udpServer.Receive(ref endPoint);
                var request = Encoding.UTF8.GetString(bytes);

                Console.WriteLine("request: " + request +
                    "from: " + endPoint.Address);

                Console.WriteLine("What is the response: ");
                var response = Console.ReadLine();

                bytes = Encoding.UTF8.GetBytes(response);
                udpServer.Send(bytes, bytes.Length, endPoint);
            }
        }
    }
}
