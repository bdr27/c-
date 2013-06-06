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
        private IPEndPoint localIP;

        #region MessageHandler Members

        public void ConnectTo(string address, int port)
        {
            this.address = address;
            this.port = port;


        }

        public void SendRequest(string message)
        {
            throw new NotImplementedException();
        }

        public string GetResponse()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
