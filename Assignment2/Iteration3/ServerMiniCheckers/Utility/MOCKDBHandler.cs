using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Utility
{
    class MOCKDBHandler : DBHandler
    {
        private Dictionary<int, string> players;
        private List<Score> highScores;

        public MOCKDBHandler()
        {
            players = new Dictionary<int, string>();
            highScores = new List<Score>();
        }

        public bool IsValidLogin(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void LoadHighScores()
        {
            highScores.Add(new Score("jack", 1));
            highScores.Add(new Score("Jack", 10));
            highScores.Add(new Score("jack", 10));
            highScores.Add(new Score("jack", 20));
        }

        public void InsertHighScore(int ScorePlayerID, Score score)
        {
            highScores.Add(score);
        }

        public Dictionary<int, string> GetPlayers()
        {
            throw new NotImplementedException();
        }

        public List<Score> GetHighScores()
        {
            return highScores;
        }
    }
}
