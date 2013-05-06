using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeClient
{
    interface MessageHandler
    {
        void ConnectTo(string ipAddress, int portNumber);
        void SendRequest(string request);
        string GetResponse();
    }
}
