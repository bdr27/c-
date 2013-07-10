using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Network
{
    public interface BroadcastListener
    {
        void StartListener();

        void SendResponse();

        void SetupListener(int port);

        string GetMessage();
    }
}
