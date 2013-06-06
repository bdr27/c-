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
        private int playerCount = 0;
        private List<Score> highScores;

        public MOCKDBHandler()
        {
            players = new Dictionary<int, string>();
            highScores = new List<Score>();
        }


        #region DBHandler Members

        public bool IsValidLogin(string username, string password)
        {
            if (!players.Values.Contains(username))
            {
                if (username.Equals("jack") && password.Equals("12345"))
                {
                        players.Add(1, "jack");
                }
                else if (username.Equals("jill") && password.Equals("54321"))
                {
                        players.Add(2, "jill");
                }
                else if (username.Equals("douglas") && password.Equals("42"))
                {
                       players.Add(3, "douglas");
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
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


        public bool IsValidLogout(string username)
        {
            if (username.Equals("jack") && players.ContainsValue(username))
            {
                players.Remove(1);
            }
            else if (username.Equals("jill") && players.ContainsValue(username))
            {
                players.Remove(2);
            }
            else if (username.Equals("douglas") && players.ContainsValue(username))
            {
                players.Remove(3);
            }
            else
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
