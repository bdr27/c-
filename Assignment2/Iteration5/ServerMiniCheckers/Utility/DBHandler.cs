using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Utility
{
    interface DBHandler
    {
        bool IsValidLogin(string username, string password);

        void LoadHighScores();

        void InsertHighScore(int ScorePlayerID, Score score);

        Dictionary<int, string> GetPlayers();

        List<Score> GetHighScores();
    }
}
