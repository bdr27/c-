using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeClient
{
    public class BoardCell
    {
        internal int row, col, ID;
        internal List<List<BoardCell>> winLists;
        internal Piece piece;

        public BoardCell()
        {
            piece = Piece.EMPTY;
        }

        public override string ToString()
        {
            var winListsString = new StringBuilder();
            foreach (var winlist in winLists)
            {
                foreach (var cell in winlist)
                {
                    winListsString.Append(cell.ID + " ");
                }
            }
            return string.Format("{0}: <{4}> ({1},{2}) winlists: {3}", ID, row, col,
                winListsString.ToString().TrimEnd(), piece);
        }
    }
}
