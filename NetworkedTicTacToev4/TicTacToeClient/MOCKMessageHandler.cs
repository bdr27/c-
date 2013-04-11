using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TicTacToeClient
{
    enum GameState
    {
        WAIT_PLAYER1_NAME,
        WAIT_PLAYER2_NAME,
        PLAYER1_MOVING,
        PLAYER2_MOVING
    }

    class MOCKMessageHandler : MessageHandler
    {
        private GameState gameState;
        private string response;
        private string player1Name, player2Name;
        private Board board;
        private string currentPlayer;

        public MOCKMessageHandler()
        {
            gameState = GameState.WAIT_PLAYER1_NAME;
            response = "NONE";
        }

        public void ConnectTo(string ipAddress, int portNumber)
        {
        }

        public void SendRequest(string request)
        {
            response = "ERROR";
            switch (gameState)
            {
                case GameState.WAIT_PLAYER1_NAME:
                    if (Regex.Match(request, @"^SET_NAME,[A-Za-z]+$").Success)
                    {
                        gameState = GameState.WAIT_PLAYER2_NAME;
                        player1Name = request.Split(',')[1];
                        response = "OKAY";
                    }
                    break;
                case GameState.WAIT_PLAYER2_NAME:
                    if (Regex.Match(request, @"^SET_NAME,[A-Za-z]+$").Success)
                    {
                        string[] elements = request.Split(',');
                        string name = elements[1];
                        if (name != player1Name)
                        {
                            gameState = GameState.PLAYER1_MOVING;
                            currentPlayer = player1Name;
                            player2Name = name;
                            response = "OKAY";
                        }
                    }
                    break;
                case GameState.PLAYER1_MOVING:
                    if (Regex.Match(request, @"^PUT,[A-Za-z]+,[1-3],[1-3]$").Success) {
                        currentPlayer = player2Name;
                        gameState = GameState.PLAYER2_MOVING;
                        response = "OKAY";
                    }
                    break;
                case GameState.PLAYER2_MOVING:
                    if (Regex.Match(request, @"^PUT,[A-Za-z]+,[1-3],[1-3]$").Success) {
                        currentPlayer = player1Name;
                        gameState = GameState.PLAYER1_MOVING;
                        response = "OKAY";
                    }
                    break;
            }
        }

        public string GetResponse()
        {
            return response;
        }
    }
}
