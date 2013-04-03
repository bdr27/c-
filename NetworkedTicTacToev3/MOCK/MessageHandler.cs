using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeClient
{
    interface MessageHandler
    {
        void connectTo(string address, int port);

        void sendRequest(string request);

        string getResponse();
    }
}
