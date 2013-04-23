using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BrendanRusso_V1_PartOne
{
    public class MOCKMessageHandler : MessageHandler
    {
        private GameState gameState;
        private string response, player1Name, player2Name, currentPlayer;

        public MOCKMessageHandler()
        {
            response = "NONE";
            gameState = GameState.WAIT_PLAYER1;
        }

        public void connectTo(string ipAddress, int portNumber)
        {
            response = "ERROR";
            string pattern = @"^(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9]{1,2})(\.(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[0-9]{1,2})){3}$";
            if (Regex.Match(ipAddress, pattern).Success && portNumber >= 49152 && portNumber <= 65535)
            {
                response = "VALID";
            }
            Debug.WriteLine("response: " + response);
        }

        public void sendRequest(string request)
        {
            response = "ERROR";

            if (Regex.Match(request, @"^UPDATE$").Success)
            {
                response = "N81N74|N12N14N27";
            }
            else if (Regex.Match(request, @"STATUS$").Success)
            {
                response = "WAITING";
            }
            else
            {
                switch (gameState)
                {
                    case GameState.WAIT_PLAYER1:
                        if (Regex.Match(request, @"^ID,[a-zA-Z]+$").Success)
                        {
                            response = "PLAYER1";
                            player1Name = request.Split(',')[1];
                            gameState = GameState.WAIT_PLAYER2;
                            Debug.WriteLine("Player 1 Name: " + player1Name);
                        }
                        break;
                    case GameState.WAIT_PLAYER2:
                        string tempPlayerName = request.Split(',')[1];
                        if (Regex.Match(request, @"^ID,[a-zA-Z]+$").Success && !tempPlayerName.Equals(player1Name))
                        {
                            response = "PLAYER2";
                            player2Name = tempPlayerName;
                            currentPlayer = player1Name;
                            gameState = GameState.PLAYER1_MOVING;
                            Debug.WriteLine("Player 2 Name: " + player2Name);
                            Debug.WriteLine("Current Player Name: " + currentPlayer);
                        }
                        break;
                }
                Debug.WriteLine("GameState: " + gameState.ToString());
            }
            Debug.WriteLine("response: " + response);
        }

        public string getResponse()
        {
            return response;
        }

        public GameState getGameState()
        {
            return gameState;
        }

        public string getPlayer1Name()
        {
            return player1Name;
        }

        public string getPlayer2Name()
        {
            return player2Name;
        }

        public string getCurrentPlayerName()
        {
            return currentPlayer;
        }
    }
}
