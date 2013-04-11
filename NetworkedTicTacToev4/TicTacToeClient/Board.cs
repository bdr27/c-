using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TicTacToeClient
{
    public class Board
    {
        private Cell[,] cells;

        public Board()
        {
            //create cell objects
            int nextID = 1;
            cells = new Cell[3, 3];
            for (int i = 0; i < 3; ++i)
            {
                for (int j = 0; j < 3; ++j)
                {
                    cells[i, j] = new Cell()
                    {
                        row = i,
                        col = j,
                        ID = nextID++,
                        
                    };
                    cells[i, j].setupCells();
                }
            }

            // setup above/below neighbourhood information for top & bottom rows
            for (int j = 0; j < 3; ++j)
            {
                cells[0, j].below = cells[1, j];
                cells[2, j].above = cells[1, j];
            }

            // setup neighbourhood information for middle row
            for (int j = 0; j < 3; ++j)
            {
                cells[1, j].above = cells[0, j];
                cells[1, j].below = cells[2, j];
            }
        }

        public void ResetPiece()
        {
            foreach (var cell in cells)
            {
                cell.piece = Piece.EMPTY;
            }
        }

        public bool isValidMove(string move)
        {
            bool valid = false;
            if (Regex.Match(move, @"^[1-3]+[1-3]$").Success)
            {
                char[] moveChars = move.ToCharArray();
                int x = Int32.Parse(moveChars[0].ToString()) - 1;
                int y = Int32.Parse(moveChars[1].ToString()) - 1;
                Console.WriteLine("Cell [x,y]: [" + x + "," + y + "]");
                if (cells[x, y].piece.Equals(Piece.EMPTY))
                {
                    valid = true;
                }
            }
            return valid;
        }

        public void Move(string move, Piece piece)
        {
            char[] moveChars = move.ToCharArray();
            int x = Int32.Parse(moveChars[0].ToString()) - 1;
            int y = Int32.Parse(moveChars[1].ToString()) - 1;
            cells[x, y].piece = piece;
        }

        public string GetPieces()
        {
            string player1Pieces = "";
            string player2Pieces = "";

            foreach (var cell in cells)
            {
                if (cell.piece.Equals(Piece.PLAYER1))
                {
                    player1Pieces = player1Pieces + "N" + (cell.row + 1) + (cell.col + 1);
                }
                else if (cell.piece.Equals(Piece.PLAYER2))
                {
                    player2Pieces = player2Pieces + "N" + (cell.row + 1) + (cell.col + 1);
                }
            }
            return player1Pieces + "|" + player2Pieces;
        }

        public Cell[,] getCells()
        {
            return cells;
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Cell cell in cells)
            {
                builder.AppendFormat("{0}{1}",cell, Environment.NewLine);
            }
            return builder.ToString();
        }

        public void Reset()
        {

        }
    }
}
