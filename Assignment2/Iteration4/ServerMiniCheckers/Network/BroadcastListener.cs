using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Network
{
    public class BroadcastListener : MessageHandler
    {
        #region MessageHandler Members

        public void ConnectTo(string address, int port)
        {
            throw new NotImplementedException();
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
