using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrendanRusso_V1_PartOne;

namespace BrendanRusso_V1_PartOne_Tests
{
    [TestClass]
    public class MOCKMessageHandlerTests
    {
        [TestMethod]
        public void TestInitialiseMOCKMessageHandler()
        {
            MessageHandler handler = new MOCKMessageHandler();
            Assert.AreEqual("NONE", handler.getResponse());
        }

        [TestMethod]
        public void TestValidIPAddress()
        {
            MessageHandler handler = new MOCKMessageHandler();
            string ipAddress = "127.0.0.1";
            int portNumber = 60000;

            //Checks for VALID ip address and valid port
            handler.connectTo(ipAddress, portNumber);
            Assert.AreEqual("VALID", handler.getResponse());

            //Checks for port boundary low out of range
            portNumber = 49151;
            handler.connectTo(ipAddress, portNumber);
            Assert.AreEqual("ERROR", handler.getResponse());

            //Checks for port boundary low
            portNumber = 49152;
            handler.connectTo(ipAddress, portNumber);
            Assert.AreEqual("VALID", handler.getResponse());

            //Checks for port boudary high out of range
            portNumber = 65536;
            handler.connectTo(ipAddress, portNumber);
            Assert.AreEqual("ERROR", handler.getResponse());

            //Checks for port boundary high
            portNumber = 65535;
            handler.connectTo(ipAddress, portNumber);
            Assert.AreEqual("VALID", handler.getResponse());

            //Checks for invalid string
            ipAddress = "hello world";
            handler.connectTo(ipAddress, portNumber);
            Assert.AreEqual("ERROR", handler.getResponse());

            //One too less numbers
            ipAddress = "125.5.4.";
            handler.connectTo(ipAddress, portNumber);
            Assert.AreEqual("ERROR", handler.getResponse());

            //One too many numbers
            ipAddress = "34.5.6.7.8";
            handler.connectTo(ipAddress, portNumber);
            Assert.AreEqual("ERROR", handler.getResponse());

            //Address out of range
            ipAddress = "256.256.256.256";
            handler.connectTo(ipAddress, portNumber);
            Assert.AreEqual("ERROR", handler.getResponse());
        }

        [TestMethod]
        public void testSendRequestForPlayer1()
        {
            MessageHandler handler = new MOCKMessageHandler();
            
            //Tests valid name and states are correct
            handler.sendRequest("ID,John");
            Assert.AreEqual("PLAYER1", handler.getResponse());
            Assert.AreEqual(GameState.WAIT_PLAYER2, handler.getGameState());
            Assert.AreEqual("John", handler.getPlayer1Name());

            //Can't have any numbers in their name
            handler = new MOCKMessageHandler();
            handler.sendRequest("ID,j0hn");
            Assert.AreEqual("ERROR", handler.getResponse());
            Assert.AreEqual(GameState.WAIT_PLAYER1, handler.getGameState());
            Assert.AreNotEqual("j0hn", handler.getPlayer1Name());
        }


        [TestMethod]
        public void testSendRequestForMultiplePlayers()
        {
            MessageHandler handler = new MOCKMessageHandler();

            //Tests can't have same name
            handler.sendRequest("ID,John");
            handler.sendRequest("ID,John");
            Assert.AreEqual("ERROR", handler.getResponse());
            Assert.AreEqual(GameState.WAIT_PLAYER2, handler.getGameState());
            Assert.AreNotEqual("John", handler.getPlayer2Name());

            //Can successfully add another name
            handler.sendRequest("ID,smith");
            Assert.AreEqual("PLAYER2", handler.getResponse());
            Assert.AreEqual(GameState.PLAYER1_MOVING, handler.getGameState());
            Assert.AreEqual("smith", handler.getPlayer2Name());

            //Can't add a third name
            handler.sendRequest("ID,IamNumber3");
            Assert.AreEqual("ERROR", handler.getResponse());
        }

        [TestMethod]
        public void testSendRequestForUpdate()
        {
            MessageHandler handler = new MOCKMessageHandler();
            setup2Players(handler);

            //check in right state and the right player is the current player
            Assert.AreEqual(GameState.PLAYER1_MOVING, handler.getGameState());
            Assert.AreEqual("joy", handler.getCurrentPlayerName());

            //Sees if I can update the board for player 1
            handler.sendRequest("UPDATE");
            Assert.AreEqual(GameState.PLAYER2_MOVING, handler.getGameState());
            Assert.AreEqual("N12N14N27|N81N74", handler.getResponse());
            Assert.AreEqual("smith", handler.getCurrentPlayerName());

            //See if I can update the board for player 2
            //Sees if I can update the board for player 1
            handler.sendRequest("UPDATE");
            Assert.AreEqual(GameState.PLAYER1_MOVING, handler.getGameState());
            Assert.AreEqual("N81N74|N12N14N27", handler.getResponse());
            Assert.AreEqual("joy", handler.getCurrentPlayerName());
        }

        private void setup2Players(MessageHandler handler)
        {
            handler.sendRequest("ID,joy");
            handler.sendRequest("ID,smith");
        }
    }
}
