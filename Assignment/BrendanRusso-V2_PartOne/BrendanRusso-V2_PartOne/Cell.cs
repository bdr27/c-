using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrendanRusso_V2_PartOne
{
    public class Cell
    {
        internal int ID, row, col;
        internal Piece piece;
        internal Cell topLeft, topRight, bottomLeft, bottomRight;

        public Cell()
        {
            topLeft = topRight = bottomLeft = bottomRight = null;
        }

        public override string ToString()
        {
            return String.Format("ID: {0}, row: {1}, col: {2}, piece: {3}", ID, row, col, piece.ToString());
        }

        public Piece getPiece()
        {
            return piece;
        }

        public string getPieceLocation()
        {
            return String.Format("N{0}{1}", row + 1, col + 1);
        }
    }
}
