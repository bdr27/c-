using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniCheckers.Utility;

namespace MiniCheckersUnitTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void SetUpNewPlayer()
        {
            int playerID = 1;
            string playerUsername = "jack";
            Player player = new Player(playerID, playerUsername);
            Assert.AreEqual(playerID, player.GetID());
            Assert.AreEqual(playerUsername, player.GetUsername());
            Assert.AreEqual("ID: " + playerID + " username : " + playerUsername, player.ToString());
        }
    }
}
