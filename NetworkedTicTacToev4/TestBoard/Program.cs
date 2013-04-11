using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToeClient;
namespace TestBoard
{
    class Program
    {
        static void Main(string[] args)
        {            
            Board board = new Board();
            Cell[,] cells = board.getCells();
            Console.WriteLine("Win List Test");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.WriteLine(cells[i, j].printWinList());
                }
            }
            board.ResetPiece();
            Console.WriteLine("\nIs Valid Move Test");
            addPieceToBoard("11", Piece.PLAYER1, board);
            addPieceToBoard("22", Piece.PLAYER1, board);
            addPieceToBoard("12", Piece.PLAYER2, board);
            addPieceToBoard("33", Piece.PLAYER2, board);
   
            Console.WriteLine("string [hello] (False): " + board.isValidMove("hello").ToString());
            Console.WriteLine("string [02] (False): " + board.isValidMove("02").ToString());
            Console.WriteLine("Pieces string: " + board.GetPieces());

            Console.WriteLine("\nPrint Board Test");
            Console.WriteLine(board);
            Console.ReadKey();
        }
        private static void addPieceToBoard(string position, Piece player, Board board)
        {
            Console.WriteLine("string [" + position + "] (True): " + board.isValidMove(position).ToString());
            board.Move(position, player);
            Console.WriteLine("string [" + position + "] when occupied by a player (False): " + board.isValidMove(position).ToString());
        }
    }
}
