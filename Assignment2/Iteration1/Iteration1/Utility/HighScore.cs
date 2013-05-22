using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iteration1.Utility
{
    public class HighScore
    {
        private Player player;
        private int playerMoves;
        private DateTime playerScoreDate;

        public HighScore(Player player, int playerMoves)
        {
            this.player = player;
            this.playerMoves = playerMoves;
            playerScoreDate = DateTime.Now;
        }

        public Player GetPlayer()
        {
            return player;
        }

        public int GetPlayerMoves()
        {
            return playerMoves;
        }

        public DateTime GetPlayerScoreDate()
        {
            return playerScoreDate;
        }
    }
}
