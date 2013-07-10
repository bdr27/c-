using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerMiniCheckers.Utility;

namespace MiniCheckersUnitTests
{
    [TestClass]
    public class MulticastTests
    {
        [TestMethod]
        public void GetListOfPlayersTest()
        {
            var dictionary = new Dictionary<int, string>();
            var players = new List<string>();
            players.Add("jack");
            players.Add("jill");
            players.Add("douglas");

            for (int i = 0; i < players.Count; i++)
            {
                dictionary.Add(i + 1, players[i]);
            }

            var listOfPlayers = GetMulticastBroadcasts.GetListOfPlayers(dictionary);

            Assert.AreEqual(players.Count, listOfPlayers.Count);

            for (int i = 0; i < listOfPlayers.Count; i++)
            {
                Assert.AreEqual(players[i], listOfPlayers[i]);
            }
        }

        [TestMethod]
        public void SendUserMessageTest()
        {
            var players = new List<string>();
            players.Add("jack");
            players.Add("jill");
            players.Add("douglas");

            var message = GetMulticastBroadcasts.GetMulticastSendPlayersMessage(players);
            Assert.AreEqual("USERS,jack,jill,douglas", message);
        }
    }
}
