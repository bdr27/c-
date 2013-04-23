using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrendanRusso_MiniCheckers_v1
{
    class Cell
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
            return String.Format("ID {0}, row {1}, col {2}", ID, row, col);
           // return String.Format("ID {0}, row {1}, col {2}\nTopLeft: {3} TopRight: {4}, BottomLeft {5}: BottomRight {6}", ID, row, col, topLeft.ID, topRight.ID, bottomLeft.ID, bottomRight.ID);
        }
    }
}
