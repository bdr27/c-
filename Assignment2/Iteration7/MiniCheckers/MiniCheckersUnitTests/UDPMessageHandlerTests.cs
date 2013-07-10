using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerMiniCheckers.Network;

namespace MiniCheckersUnitTests
{
    [TestClass]
    public class UDPMessageHandlerTests
    {
        private string response;
        private MessageHandler messageHandler;
        private NetworkDetails networkDetails;
        private UDPListener listener;
        private bool running;

    //    [TestMethod]
 /*       public void TestLogin()
        {
            var ipAddress = "127.0.0.1";
            var port = 50000;
            networkDetails = new NetworkDetails(ipAddress, port);
            listen();
          //  messageHandler = new UDPMessageHandler();
            messageHandler.ConnectTo(ipAddress, port);
            messageHandler.SendRequest("LOGIN,jack,12345");
            messageHandler.Close();
            Assert.AreEqual("OKAY", listener.GetMessage());
            listener.Close();
        }

        private void listen()
        {
            var listeningThread = new Thread(new ThreadStart(ListeningThread_Action));
            listeningThread.IsBackground = true;
            listeningThread.Start();
        }

        [TestMethod]
        private void ListeningThread_Action()
        {
            listener = new UDPListener();
            listener.SetupListener(networkDetails.GetPortNumber());
            listener.StartListener();
            Assert.AreEqual("OKAY", listener.GetMessage());
        }*/
    }
}
