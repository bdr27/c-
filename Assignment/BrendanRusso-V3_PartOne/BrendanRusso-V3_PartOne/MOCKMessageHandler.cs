using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BrendanRusso_V3_PartOne
{
    public class MOCKMessageHandler : MessageHandler
    {
        private GameState gameState;
        private string response, player1Name, player2Name, currentPlayer;
        private Board board;

        public MOCKMessageHandler()
        {
            response = "NONE";
            gameState = GameState.WAIT_PLAYER1;
            board = new Board();
        }

        public void connectTo(string ipAddress, int portNumber)
        {
            response = "ERROR";
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
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
                response = board.getPlayerPieces();
            }
            else if (Regex.Match(request, @"^STATUS$").Success)
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
                            board.setupStartingLocations();
                            gameState = GameState.PLAYER1_MOVING;
                            Debug.WriteLine("Player 2 Name: " + player2Name);
                            Debug.WriteLine("Current Player Name: " + currentPlayer);
                        }
                        break;
                    case GameState.PLAYER1_MOVING:
                        Debug.WriteLine("Player 1 is moving");
                        if (checkValidMove(GameState.PLAYER1_MOVING, player1Name, request))
                        {
                            response = "DONE";
                        }
                        break;
                    case GameState.PLAYER2_MOVING:
                        Debug.WriteLine("Player 2 is moving");
                        if (checkValidMove(GameState.PLAYER2_MOVING, player2Name, request))
                        {
                            response = "DONE";
                        }
                        break;
                }
                Debug.WriteLine("GameState: " + gameState.ToString());
            }
            Debug.WriteLine("response: " + response);
        }

        private bool checkValidMove(GameState state, string playerName, string request)
        {
            bool validMove = false;
            if (Regex.Match(request, @"^TRY,[a-zA-Z]+,N[1-8][1-8],N[1-8][1-8]$").Success)
            {
                //Puts into string for easier use later
                string[] playerTryMove = request.Split(',');
                currentPlayer = playerTryMove[1];

                //Checks for correct state
                if (state.Equals(GameState.PLAYER1_MOVING))
                {
                    //Checks for right name
                    if (currentPlayer.Equals(player1Name))
                    {
                        //Checks if move is valid
                        if (board.ValidMove(playerTryMove, Piece.PLAYER1))
                        {
                            gameState = GameState.PLAYER2_MOVING;
                            validMove = true;
                        }
                    }
                }
                else if (state.Equals(GameState.PLAYER2_MOVING))
                {
                    //Checks for right name
                    if (currentPlayer.Equals(player2Name))
                    {
                        //Checks if move is valid
                        if (board.ValidMove(playerTryMove, Piece.PLAYER2))
                        {
                            gameState = GameState.PLAYER1_MOVING;
                            validMove = true;
                        }
                    }
                }
            }
            return validMove;
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
