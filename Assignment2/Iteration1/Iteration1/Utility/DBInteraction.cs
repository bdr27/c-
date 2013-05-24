using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MiniCheckers.Utility
{
    public class DBInteraction
    {
        private Dictionary<int, string> players;
        private SortedSet<HighScore> highScores;

        public DBInteraction()
        {
            players = new Dictionary<int, string>();            
            highScores = new SortedSet<HighScore>();
            LoadHighScores();
        }

        public bool isValidLogin(string username, string password)
        {
            using (var context = new CheckersDatabaseContainer())
            {
                var query = from Player in context.Players where Player.PlayerUsername == username && Player.PlayerPassword == password select Player;

                foreach (var result in query)
                {
                    if (!players.ContainsKey(result.PlayerID))
                    {
                        players.Add(result.PlayerID, result.PlayerUsername);
                        return true;
                    }
                }
            }
            return false;
        }

        public void LoadHighScores()
        {
            using (var context = new CheckersDatabaseContainer())
            {
                var query = from HighScore in context.HighScores select HighScore;

                foreach (var result in query)
                {
                    var queryGetName = from Player in context.Players where result.PlayerID == Player.PlayerID select Player.PlayerUsername;
                    foreach (var name in queryGetName)
                    {
                        highScores.Add(new HighScore(name, result.PlayerMoves, result.PlayerScoreDate));
                    }
                }
            }
        }

        public void insertHighScore(int playerID, HighScore highScore)
        {

        }

        public Dictionary<int, string> GetPlayers()
        {
            return players;
        }

        public SortedSet<HighScore> GetHighScores()
        {
            return highScores;
        }
    }
}