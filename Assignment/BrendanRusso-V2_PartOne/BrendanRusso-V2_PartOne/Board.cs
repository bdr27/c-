using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrendanRusso_V2_PartOne
{
    public class Board
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
                    cells[i, j] = new Cell()
                    
                    {
                        row = i,
                        col = j,
                        ID = nextID++,
                        piece = Piece.EMPTY
                    };
                }
            }
            setupMoveLocations();
           
        }
        private void setupMoveLocations()
        {
            //Sets up empty cells for now
            foreach (Cell cell in cells)
            {
                int row = cell.row;
                int col = cell.col;

                Debug.WriteLine(cells[row, col].ToString());
                //Checks for top left
                if (row > 0 && col > 0)
                {
                    cells[row, col].topLeft = cells[row - 1, col - 1];
                    Debug.WriteLine("\tTop Left: " + cells[row, col].topLeft.ToString());
                }
                //Checks for bottom left
                if (row < BOARD_HEIGHT - 1 && col > 0)
                {
                    cells[row, col].bottomLeft = cells[row + 1, col - 1];
                    Debug.WriteLine("\tTop Right: " + cells[row, col].bottomLeft.ToString());
                }
                //Checks for top right
                if (row > 0 && col < BOARD_WIDTH - 1)
                {
                    cells[row, col].topRight = cells[row - 1, col + 1];
                    Debug.WriteLine("\tBottom Left: " + cells[row, col].topRight.ToString());
                }
                //Checks for bottom right
                if (row < BOARD_HEIGHT - 1 && col < BOARD_WIDTH - 1)
                {
                    cells[row, col].bottomRight = cells[row + 1, col + 1];
                    Debug.WriteLine("\tBottom Right: " + cells[row, col].bottomRight.ToString());
                }               
            }
        }

        public string getPlayerPieces()
        {
            string player1Pieces = "";
            string player2Pieces = "";
            foreach (Cell cell in cells)
            {
                if (cell.getPiece().Equals(Piece.PLAYER1))
                {
                    player1Pieces += cell.getPieceLocation();
                }
                else if (cell.getPiece().Equals(Piece.PLAYER2))
                {
                    player2Pieces += cell.getPieceLocation();
                }
            }

            return player1Pieces + "|" + player2Pieces;
        }

        public Cell[,] getCells()
        {
            return cells;
        }

        public void setupStartingLocations()
        {
            //Setup for top player
            setupPlayerLocations(0, 3, Piece.PLAYER1);
            //Setup for bottom player
            setupPlayerLocations(BOARD_HEIGHT - 3, BOARD_HEIGHT, Piece.PLAYER2);
        }

        public void updateCell()
        {
        }

        private void setupPlayerLocations(int startRow, int endRow, Piece playerPiece)
        {
            for (int i = startRow; i < endRow; i++)
            {
                for (int j = 0; j < BOARD_WIDTH; j++)
                {
                    //if on even rows
                    if ((i % 2) == 0)
                    {
                        //player piece only on odd squares
                        if ((j % 2) == 1)
                        {
                            cells[i, j].piece = playerPiece;
                            Debug.WriteLine(cells[i, j].ToString());
                        }
                    }
                    //if on odd rows
                    else
                    {
                        //player only on even squares
                        if ((j % 2) == 0)
                        {
                            cells[i, j].piece = playerPiece;
                            Debug.WriteLine(cells[i, j].ToString());
                        }
                    }
                }
            }
        }
    }
}
