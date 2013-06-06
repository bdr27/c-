using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServerMiniCheckers.Network;

namespace ClientMiniCheckers.Network
{
    class ClientUDPBroadcastHandler : MessageHandler
    {
        private IPEndPoint localIP;
        private UdpClient udpClient;
        private IPAddress multicastingIP;

        #region MessageHandler Members

        public void ConnectTo(string address, int port)
        {
            localIP = new IPEndPoint(IPAddress.Any, port);
            udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.ExclusiveAddressUse = false;
            udpClient.Client.Bind(localIP);

            multicastingIP = IPAddress.Parse(address);
            udpClient.JoinMulticastGroup(multicastingIP);
        }

        public void SendRequest(string message)
        {
            //I never send
        }

        public string GetResponse()
        {
            var bytes = udpClient.Receive(ref localIP);
            var message = Encoding.UTF8.GetString(bytes);
            return message;
        }

        public void Close()
        {
            udpClient.DropMulticastGroup(multicastingIP);
            udpClient.Close();
        }

        #endregion
    }
}
