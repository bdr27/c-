using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientMiniCheckers.Utility
{
   public class SplitPlayerPieces
    {
        public static List<int> GetPieces(string pieces)
        {
            var playerPiecesID = new List<int>();
            var pieceLocation = pieces.Split('N');
            for (int i = 1; i < pieceLocation.Length; i++)
            {
                int row = Int32.Parse(pieceLocation[i][0].ToString());
                int col = Int32.Parse(pieceLocation[i][1].ToString());
                Debug.WriteLine(string.Format("row: {0}, col: {1}", row, col));
                playerPiecesID.Add((row - 1) * 8 + (col - 1));
            }
            return playerPiecesID;
        }
    }
}
