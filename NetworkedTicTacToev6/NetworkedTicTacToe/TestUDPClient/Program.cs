using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestUDPClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            //setup the sender object
            var udpClient = new UdpClient("127.0.0.1", 50000);

            while (true)
            {
                Console.WriteLine("enter your request: ");
                var request = Console.ReadLine();
                if(request == "end") break;

                //send the request as bytes
                var bytes = Encoding.UTF8.GetBytes(request);
                udpClient.Send(bytes, bytes.Length);

                //get response and display it
                var endPoint = new IPEndPoint(IPAddress.Any, 50000);
                bytes = udpClient.Receive(ref endPoint);
                var response = Encoding.UTF8.GetString(bytes);
                Console.WriteLine("response: " + response);
            }

            udpClient.Close();
        }
    }
}
