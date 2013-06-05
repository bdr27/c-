using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ServerMiniCheckers.Network;

[assembly: InternalsVisibleTo("MiniCheckersUnitTests")]

namespace ClientMiniCheckers.Network
{
    public class UDPMessageHandler : MessageHandler
    {
        private UdpClient udpClient;
        private string address;
        private int port;
        #region MessageHandler Members

        public void ConnectTo(string address, int port)
        {
            this.address = address;
            this.port = port;
            udpClient = new UdpClient(address, port);
        }

        public void SendRequest(string request)
        {
            var bytes = Encoding.UTF8.GetBytes(request);
            udpClient.Send(bytes, bytes.Count());

            Debug.WriteLine("sending message: " + request);
        }

        public string GetResponse()
        {
            var endPoint = new IPEndPoint(IPAddress.Parse(address), port);
            var bytes = udpClient.Receive(ref endPoint);
            var response = Encoding.UTF8.GetString(bytes);
            return response;
        }


        public void Close()
        {
            udpClient.Close();
        }

        #endregion
    }
}
