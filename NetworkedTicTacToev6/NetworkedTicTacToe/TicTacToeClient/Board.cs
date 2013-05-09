using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TicTacToeClient
{
    public class Board
    {
        private BoardCell[,] cells;

        public Board()
        {
            //create cell objects
            int nextID = 1;
            cells = new BoardCell[3, 3];
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    cells[i, j] = new BoardCell()
                    {
                        row = i,
                        col = j,
                        ID = nextID++
                    };
                }
            }

            // setup the winLists for each cell
            // doing this by hand, no need to figure out a loop based dynamic-sized algorithm...
            // tictactoe is 3x3 only...
            cells[0, 0].winLists = setupWinListForCell_1();
            cells[1, 0].winLists = setupWinListForCell_4();
            cells[2, 0].winLists = setupWinListForCell_7();

            cells[0, 1].winLists = setupWinListForCell_2();
            cells[1, 1].winLists = setupWinListForCell_5();
            cells[2, 1].winLists = setupWinListForCell_8();

            cells[0, 2].winLists = setupWinListForCell_3();
            cells[1, 2].winLists = setupWinListForCell_6();
            cells[2, 2].winLists = setupWinListForCell_9();
        }

        private List<List<BoardCell>> setupWinListForCell_1()
        {
            List<List<BoardCell>> winLists = new List<List<BoardCell>>();
            List<BoardCell> winList = new List<BoardCell>();
            winList.Add(cells[0, 1]);
            winList.Add(cells[0, 2]);
            winLists.Add(winList); // [2,3]

            winList = new List<BoardCell>();
            winList.Add(cells[1, 1]);
            winList.Add(cells[2, 2]);
            winLists.Add(winList); // [5,9]

            winList = new List<BoardCell>();
            winList.Add(cells[1, 0]);
            winList.Add(cells[2, 0]);
            winLists.Add(winList); // [4,7]

            return winLists;
        }

        private List<List<BoardCell>> setupWinListForCell_4()
        {
            List<List<BoardCell>> winLists = new List<List<BoardCell>>();
            List<BoardCell> winList = new List<BoardCell>();
            winList.Add(cells[0, 0]);
            winList.Add(cells[2, 0]);
            winLists.Add(winList); // [1,7]

            winList = new List<BoardCell>();
            winList.Add(cells[1, 1]);
            winList.Add(cells[1, 2]);
            winLists.Add(winList); // [5,6]

            return winLists;
        }

        private List<List<BoardCell>> setupWinListForCell_7()
        {
            List<List<BoardCell>> winLists = new List<List<BoardCell>>();
            List<BoardCell> winList = new List<BoardCell>();
            winList.Add(cells[0, 0]);
            winList.Add(cells[1, 0]);
            winLists.Add(winList); // [1,4]

            winList = new List<BoardCell>();
            winList.Add(cells[1, 1]);
            winList.Add(cells[0, 2]);
            winLists.Add(winList); // [5,3]

            winList = new List<BoardCell>();
            winList.Add(cells[2, 1]);
            winList.Add(cells[2, 2]);
            winLists.Add(winList); // [8,9]

            return winLists;
        }

        private List<List<BoardCell>> setupWinListForCell_2()
        {
            List<List<BoardCell>> winLists = new List<List<BoardCell>>();
            List<BoardCell> winList = new List<BoardCell>();
            winList.Add(cells[0, 0]);
            winList.Add(cells[0, 2]);
            winLists.Add(winList); // [1,3]

            winList = new List<BoardCell>();
            winList.Add(cells[1, 1]);
            winList.Add(cells[2, 1]);
            winLists.Add(winList); // [5,8]

            return winLists;
        }

        private List<List<BoardCell>> setupWinListForCell_5()
        {
            List<List<BoardCell>> winLists = new List<List<BoardCell>>();
            List<BoardCell> winList = new List<BoardCell>();
            winList.Add(cells[0, 0]);
            winList.Add(cells[2, 2]);
            winLists.Add(winList); // [1,9]

            winList = new List<BoardCell>();
            winList.Add(cells[0, 1]);
            winList.Add(cells[2, 1]);
            winLists.Add(winList); // [2,8]

            winList = new List<BoardCell>();
            winList.Add(cells[0, 2]);
            winList.Add(cells[2, 0]);
            winLists.Add(winList); // [3,7]

            winList = new List<BoardCell>();
            winList.Add(cells[1, 0]);
            winList.Add(cells[1, 2]);
            winLists.Add(winList); // [4,6]

            return winLists;
        }

        private List<List<BoardCell>> setupWinListForCell_8()
        {
            List<List<BoardCell>> winLists = new List<List<BoardCell>>();
            List<BoardCell> winList = new List<BoardCell>();
            winList.Add(cells[2, 0]);
            winList.Add(cells[2, 2]);
            winLists.Add(winList); // [7,9]

            winList = new List<BoardCell>();
            winList.Add(cells[0, 1]);
            winList.Add(cells[1, 1]);
            winLists.Add(winList); // [2,5]

            return winLists;
        }

        private List<List<BoardCell>> setupWinListForCell_3()
        {
            List<List<BoardCell>> winLists = new List<List<BoardCell>>();
            List<BoardCell> winList = new List<BoardCell>();
            winList.Add(cells[0, 0]);
            winList.Add(cells[0, 1]);
            winLists.Add(winList); // [1,2]

            winList = new List<BoardCell>();
            winList.Add(cells[1, 1]);
            winList.Add(cells[2, 0]);
            winLists.Add(winList); // [5,7]

            winList = new List<BoardCell>();
            winList.Add(cells[1, 2]);
            winList.Add(cells[2, 2]);
            winLists.Add(winList); // [6,9]

            return winLists;
        }

        private List<List<BoardCell>> setupWinListForCell_6()
        {
            List<List<BoardCell>> winLists = new List<List<BoardCell>>();
            List<BoardCell> winList = new List<BoardCell>();
            winList.Add(cells[0, 2]);
            winList.Add(cells[2, 2]);
            winLists.Add(winList); // [3,9]

            winList = new List<BoardCell>();
            winList.Add(cells[1, 0]);
            winList.Add(cells[1, 1]);
            winLists.Add(winList); // [4,5]

            return winLists;
        }

        private List<List<BoardCell>> setupWinListForCell_9()
        {
            List<List<BoardCell>> winLists = new List<List<BoardCell>>();
            List<BoardCell> winList = new List<BoardCell>();
            winList.Add(cells[0, 0]);
            winList.Add(cells[1, 1]);
            winLists.Add(winList); // [1,5]

            winList = new List<BoardCell>();
            winList.Add(cells[0, 2]);
            winList.Add(cells[1, 2]);
            winLists.Add(winList); // [3,6]

            winList = new List<BoardCell>();
            winList.Add(cells[2, 0]);
            winList.Add(cells[2, 1]);
            winLists.Add(winList); // [7,8]

            return winLists;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (BoardCell cell in cells)
            {
                builder.AppendFormat("{0}{1}", cell, Environment.NewLine);
            }
            return builder.ToString();
        }

        public void ResetPieces()
        {
            foreach (var cell in cells)
            {
                cell.piece = Piece.EMPTY;
            }
        }

        public bool IsValidMove(string position)
        {
            var validFormat = Regex.Match(position, @"^[1-3][1-3]$").Success;
            if (!validFormat) return false;

            int row = int.Parse(position.Substring(0, 1)) - 1;
            int col = int.Parse(position.Substring(1, 1)) - 1;
            return cells[row, col].piece == Piece.EMPTY;
        }

        public void Move(string position, Piece piece)
        {
            int row = int.Parse(position.Substring(0, 1)) - 1;
            int col = int.Parse(position.Substring(1, 1)) - 1;

            cells[row, col].piece = piece;
        }

        public string GetPieces()
        {
            var player1Pieces = new List<string>();
            var player2Pieces = new List<string>();

            foreach (var cell in cells)
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

        public Piece WinningPiece()
        {
            foreach (var cell in cells)
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
