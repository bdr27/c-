using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                        ID = nextID++
                    };
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
        }

        public bool isValidMove(string move)
        {
            bool valid = true;
            return valid;
        }

        //public void Move(string move, Piece piece)
        //{
       // }

        public string GetPieces()
        {
            string pieces = "";
            return pieces;
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
