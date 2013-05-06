using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToeClient.Model
{
    internal class BoardCell
    {
        internal int row, col;
        internal Piece piece;
        internal List<List<BoardCell>> winLists;
    }
}
