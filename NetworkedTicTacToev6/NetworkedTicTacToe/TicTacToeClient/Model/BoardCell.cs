using System.Collections.Generic;

namespace TicTacToeClient.Model
{
    internal class BoardCell
    {
        internal int row, col;
        internal Piece piece;
        internal List<List<BoardCell>> winLists;
    }
}
