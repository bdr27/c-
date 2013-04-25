using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrendanRusso_V3_PartOne
{
    public interface MessageHandler
    {
        void connectTo(string ipAddress, int portNumber);

        void sendRequest(string request);

        string getResponse();

        GameState getGameState();

        string getPlayer1Name();

        string getPlayer2Name();

        string getCurrentPlayerName();

        void setStatus(string status);
    }
}
