using System;

namespace MiniCheckers.Utility
{
    public class Score
    {
        private string username;
        private int playerMoves;
        private DateTime playerScoreDate;

        public Score(string username, int playerMoves)
        {
            this.username = username;
            this.playerMoves = playerMoves;
            playerScoreDate = DateTime.Now;
        }

        public Score(string username, int playerMoves, DateTime playerScoreDate)
        {
            this.username = username;
            this.playerMoves = playerMoves;
            this.playerScoreDate = playerScoreDate;
        }

        public string GetUsername()
        {
            return username;
        }

        public int GetPlayerMoves()
        {
            return playerMoves;
        }

        public DateTime GetPlayerScoreDate()
        {
            return playerScoreDate;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", username, playerMoves, playerScoreDate);
        }
    }
}
