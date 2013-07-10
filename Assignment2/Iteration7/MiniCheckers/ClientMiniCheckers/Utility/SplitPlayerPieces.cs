using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMiniCheckers.Utility
{
   public class SplitPlayerPieces
    {
        public static List<int> GetPieces(string pieces)
        {
            var listPieces = new List<int>();
            var something = pieces.Split('N');
            foreach (string piece in something)
            {
                if (!piece.Equals(""))
                {
                    listPieces.Add(Int32.Parse(piece));
                }
            }
            return listPieces;
        }
    }
}
