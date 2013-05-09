using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

[assembly: InternalsVisibleTo("TestTicTacTowClient")]

namespace TicTacToeClient.Model
{
    public class GameLogic
    {
        internal GameBoard board;
        internal Piece currentPlayerPiece;

        public GameLogic()
        {
            board = new GameBoard();
        }

        public void StartNewGame()
        {
            board.ResetPieces();
            currentPlayerPiece = Piece.PLAYER1;
        }

        public string Player1Name { set; get; }
        public string Player2Name { set; get; }

        public string CurrentPlayer
        {
            get
            {
                if (currentPlayerPiece == Piece.PLAYER1)
                {
                    return Player1Name;
                }
                return Player2Name;
            }
        }

        public bool IsValidMove(string position, string playerName)
        {
            if (playerName != CurrentPlayer) return false;

            var validFormat = Regex.Match(position, @"^[1-3][1-3]$").Success;
            if (!validFormat) return false;

            int row = int.Parse(position.Substring(0, 1)) - 1;
            int col = int.Parse(position.Substring(1, 1)) - 1;
            return board.cells[row, col].piece == Piece.EMPTY;
        }

        public void Move(string position)
        {
            int row = int.Parse(position.Substring(0, 1)) - 1;
            int col = int.Parse(position.Substring(1, 1)) - 1;

            board.cells[row, col].piece = currentPlayerPiece;
            currentPlayerPiece = (currentPlayerPiece == Piece.PLAYER1)
                ? Piece.PLAYER2 : Piece.PLAYER1;
        }

        public string EncodeBoardPieces()
        {
            var player1Pieces = new List<string>();
            var player2Pieces = new List<string>();

            foreach (var cell in board.cells)
            {
                switch (cell.piece)
                {
                    case Piece.PLAYER1:
                        player1Pieces.Add("N" + (cell.row + 1) + (cell.col + 1));
                        break;
                    case Piece.PLAYER2:
                        player2Pieces.Add("N" + (cell.row + 1) + (cell.col + 1));
                        break;
                }
            }

            var builder = new StringBuilder();
            foreach (var piece in player1Pieces)
            {
                builder.Append(piece);
            }

            builder.Append("|");

            foreach (var piece in player2Pieces)
            {
                builder.Append(piece);
            }
            return builder.ToString();
        }

        public Piece FindWinningPiece()
        {
            foreach (var cell in board.cells)
            {
                if (cell.piece == Piece.EMPTY) continue;

                var playerPiece = cell.piece;
                foreach (var winlist in cell.winLists)
                {
                    var win = true;
                    foreach (var neighbouringCell in winlist)
                    {
                        if (playerPiece != neighbouringCell.piece)
                        {
                            win = false;
                            break;
                        }
                    }
                    if (win)
                    {
                        return playerPiece;
                    }
                }
            }

            return Piece.EMPTY;
        }
    }
}
