using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniCheckers.Utility;

namespace MiniCheckersUnitTests
{
    [TestClass]
    public class HighScoreTests
    {
        [TestMethod]
        public void CreateNewHighScore()
        {
            string username = "bob";
            HighScore highScore = new HighScore(username, 4);
            DateTime dateTime = highScore.GetPlayerScoreDate();
            Assert.AreEqual(string.Format("{0} {1} {2}", username, highScore.GetPlayerMoves(), highScore.GetPlayerScoreDate()), highScore.ToString());
        }
    }
}
