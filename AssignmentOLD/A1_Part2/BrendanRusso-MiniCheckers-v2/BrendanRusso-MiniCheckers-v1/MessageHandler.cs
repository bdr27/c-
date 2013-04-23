using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrendanRusso_MiniCheckers_v1
{
    public interface MessageHandler
    {
        void sendRequest(string request);

        void connectTo(string ipAddress, int portNumber);

        string getRequest();
    }
}
