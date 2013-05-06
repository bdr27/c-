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
            Console.WriteLine(board);
            Console.ReadKey();

            board.ResetPieces();

            board.Move("11", Piece.PLAYER1);
            board.Move("12", Piece.PLAYER1);
            board.Move("13", Piece.PLAYER2);
            board.Move("22", Piece.PLAYER2);

            Console.WriteLine("pieces: " + board.GetPieces());
            Console.ReadKey();

            board.ResetPieces();
            Console.WriteLine("pieces: " + board.GetPieces());
            Console.ReadKey();
        }
    }
}
