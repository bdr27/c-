using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrendanRusso_MiniCheckers_v1
{
    class Board
    {
        private const int BOARD_HEIGHT = 8;
        private const int BOARD_WIDTH = 8;
        Cell[,] cells;

        public Board()
        {
            int nextID = 1;
            cells = new Cell[BOARD_HEIGHT, BOARD_WIDTH];
            for (int i = 0; i < BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < BOARD_WIDTH; j++)
                {
                    cells[i,j] = new Cell()
                    {
                        row = i,
                        col = j,
                        ID = nextID++,
                        piece = Piece.EMPTY
                    };                    
                }
            }

            //Sets up empty cells for now
            foreach (Cell cell in cells)
            {
                int row = cell.row;
                int col = cell.col;
                //Checks for top left
                if (row > 0 && col > 0)
                {
                    cells[row, col].topLeft = cells[row - 1, col - 1];
                }
                //Checks for bottom left
                if (row < BOARD_HEIGHT - 1 && col > 0)
                {
                    cells[row, col].bottomLeft = cells[row + 1, col - 1];
                }
                //Checks for top right
                if (row > 0 && col < BOARD_WIDTH - 1)
                {
                    cells[row, col].topRight = cells[row - 1, col + 1];
                }
                //Checks for bottom right
                if (row < BOARD_HEIGHT - 1 && col < BOARD_WIDTH - 1)
                {
                    cells[row, col].bottomRight = cells[row + 1, col + 1];
                }
                Debug.WriteLine(cells[row, col].ToString());
            }
        }
    }
}
