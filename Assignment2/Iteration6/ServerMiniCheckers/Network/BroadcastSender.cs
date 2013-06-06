using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Network
{
    public class BroadcastSender : MessageHandler
    {
        private UdpClient udpClient;
        private int port;
        private string address;
        private IPEndPoint endPoint;
        private IPAddress multicastIP;

        #region MessageHandler Members

        public void ConnectTo(string address, int port)
        {
            this.address = address;
            this.port = port;
            multicastIP = IPAddress.Parse(address);
            udpClient = new UdpClient();
            udpClient.JoinMulticastGroup(multicastIP);
            endPoint = new IPEndPoint(multicastIP, port);
        }

        public void SendRequest(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            udpClient.Send(bytes, bytes.Length, endPoint);
        }

        public string GetResponse()
        {
            return "";
        }

        public void Close()
        {
            udpClient.DropMulticastGroup(multicastIP);
            udpClient.Close();
        }

        #endregion
    }
}
