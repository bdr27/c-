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
        private string player1Name, player2Name, currentPlayer;
        private Board board;

        public MOCKMessageHandler()
        {
            gameState = GameState.WAIT_PLAYER1_NAME;
            response = "NONE";
            board = new Board();
        }

        public void ConnectTo(string ipAddress, int portNumber)
        {
        }

        public void SendRequest(string request)
        {
            if (request == "UPDATE")
            {
                response = board.GetPieces();
                return;
            }

            response = "ERROR";
            switch (gameState)
            {
                case GameState.WAIT_PLAYER1_NAME:
                    if (Regex.Match(request, @"^SET_NAME,[A-Za-z]+$").Success)
                    {
                        gameState = GameState.WAIT_PLAYER2_NAME;
                        player1Name = request.Split(',')[1];
                        response = "PLAYER1";
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
                            player2Name = name;
                            currentPlayer = player1Name;
                            response = "PLAYER2";
                        }
                    }
                    break;
                case GameState.PLAYER1_MOVING:
                case GameState.PLAYER2_MOVING:
                    if (Regex.Match(request, @"^PUT,[A-Za-z]+,[1-3][1-3]$").Success)
                    {

                        var elements = request.Split(',');
                        var name = elements[1];
                        var position = elements[2];

                        if (currentPlayer == name
                            && name == player1Name
                            && board.IsValidMove(position))
                        {
                            gameState = GameState.PLAYER2_MOVING;
                            currentPlayer = player2Name;
                            board.Move(position, Piece.PLAYER1);
                            response = "OKAY";
                        }
                        else if (currentPlayer == name
                            && name == player2Name
                            && board.IsValidMove(position))
                        {
                            gameState = GameState.PLAYER1_MOVING;
                            currentPlayer = player1Name;
                            board.Move(position, Piece.PLAYER2);
                            response = "OKAY";
                        }
                    }
                    else if (Regex.Match(request, @"^STATUS,[A-Za-z]+$").Success)
                    {
                        var name = request.Split(',')[1];
                        var winningPiece = board.WinningPiece();
                        if (winningPiece == Piece.EMPTY)
                        {
                            response = name == currentPlayer ? "YOUR_MOVE" : "NOT_YOUR_MOVE";
                        }
                        else
                        {
                            if (name == player1Name)
                            {
                                response = winningPiece == Piece.PLAYER1 ? "WON" : "LOST";
                            }
                            else // name == player2Name
                            {
                                response = winningPiece == Piece.PLAYER2 ? "WON" : "LOST";
                            }
                        }
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
