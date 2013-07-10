using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Network
{
    class ServerUDPMessageHandler : MessageHandler
    {
        private UdpClient udpServer;
        private IPEndPoint endPoint;
        private string address;
        private int port;

        #region MessageHandler Members

        public void ConnectTo(string address, int port)
        {
            this.address = address;
            this.port = port;
            udpServer = new UdpClient(port);            
        }

        public void SendRequest(string request)
        {
            var bytes = Encoding.UTF8.GetBytes(request);
            udpServer.Send(bytes, bytes.Length, endPoint);
        }

        public string GetResponse()
        {
            endPoint = new IPEndPoint(IPAddress.Any, port);
            var bytes = udpServer.Receive(ref endPoint);
            var response = Encoding.UTF8.GetString(bytes);
            Debug.WriteLine("Address: " + endPoint.Address);
            return response;
        }

        public void Close()
        {
            udpServer.Close();
        }

        #endregion
    }
}
