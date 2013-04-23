using System;
using System.Collections.Generic;
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
        }

        public void sendRequest(string request)
        {
            response = "ERROR";
            switch (gameState)
            {
                case GameState.WAIT_PLAYER1:
                    if (Regex.Match(request, @"^ID,[a-zA-Z]+$").Success)
                    {
                        response = "PLAYER1";
                        player1Name = request.Split(',')[1];
                        gameState = GameState.WAIT_PLAYER2;
                        
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
                    }
                    break;
                case GameState.PLAYER1_MOVING:
                    if (Regex.Match(request, @"UPDATE$").Success)
                    {
                        currentPlayer = player2Name;
                        gameState = GameState.PLAYER2_MOVING;
                        request = "N12N14N27|N81N74";
                    }
                    break;
                case GameState.PLAYER2_MOVING:
                    if (Regex.Match(request, @"UPDATE$").Success)
                    {
                        currentPlayer = player1Name;
                        gameState = GameState.PLAYER1_MOVING;
                        request = "N81N74|N12N14N27";
                    }
                    break;
            }
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
