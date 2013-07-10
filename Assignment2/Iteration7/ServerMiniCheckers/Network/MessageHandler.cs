using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Network
{
    public interface MessageHandler
    {
        void ConnectTo(string address, int port);
        void SendRequest(string message);
        string GetResponse();
        void Close();
    }
}
