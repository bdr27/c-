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
            Cell cell = new Cell(1);
            cell.setupCells();
            Console.WriteLine(cell.printWinList());
            Board board = new Board();
            Console.WriteLine(board);
            Console.ReadKey();
        }
    }
}
