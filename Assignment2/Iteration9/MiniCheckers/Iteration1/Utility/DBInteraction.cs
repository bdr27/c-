using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MiniCheckers.Utility
{
    public class DBInteraction
    {
        private Dictionary<int, string> players;
        private List<Score> highScores;

        public DBInteraction()
        {
            players = new Dictionary<int, string>();            
            highScores = new List<Score>();
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
            highScores.Clear();
            using (var context = new CheckersDatabaseContainer())
            {
                var query = from HighScore in context.HighScores orderby HighScore.PlayerMoves ascending, HighScore.PlayerScoreDate ascending select HighScore;

                foreach (var result in query)
                {
                    var queryGetName = from Player in context.Players where result.PlayerID == Player.PlayerID select Player.PlayerUsername;
                    foreach (var name in queryGetName)
                    {
                        highScores.Add(new Score(name, result.PlayerMoves, result.PlayerScoreDate));

                    }
                }
            }
        }

        public void insertHighScore(int ScorePlayerID, Score score)
        {
            using (var context = new CheckersDatabaseContainer())
            {
                var query = from Player in context.Players where Player.PlayerID == ScorePlayerID select Player;

                foreach (var result in query)
                {
                    var player = result;
                    var highScore = new HighScores()
                    {
                        Player = player,
                        PlayerID = ScorePlayerID,
                        PlayerMoves = score.GetPlayerMoves(),
                        PlayerScoreDate = score.GetPlayerScoreDate()
                    };
                    context.HighScores.Add(highScore);
                }
                context.SaveChanges();                
            }
        }

        public Dictionary<int, string> GetPlayers()
        {
            return players;
        }

        public List<Score> GetHighScores()
        {
            return highScores;
        }
    }
}