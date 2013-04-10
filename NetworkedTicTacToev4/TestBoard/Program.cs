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
            Console.WriteLine("\nIs Valid Move Test");
            Console.WriteLine("string [13] (True): " + board.isValidMove("13").ToString());
            board.Move("13", Piece.PLAYER1);
            Console.WriteLine("string [13] when occupied by another player (False): " + board.isValidMove("13").ToString());
            Console.WriteLine("string [hello] (False): " + board.isValidMove("hello").ToString());
            Console.WriteLine("string [02] (False): " + board.isValidMove("02").ToString());


            Console.WriteLine("\nPrint Board Test");
            Console.WriteLine(board);
            Console.ReadKey();
        }
    }
}
