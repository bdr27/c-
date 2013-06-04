using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Network
{
    public class UDPMessageHandler : MessageHandler
    {
        private UdpClient udpClient;
        #region MessageHandler Members

        public void ConnectTo(string address, int port)
        {
        }

        public void SendRequest(string message)
        {
            throw new NotImplementedException();
        }

        public string GetResponse()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
