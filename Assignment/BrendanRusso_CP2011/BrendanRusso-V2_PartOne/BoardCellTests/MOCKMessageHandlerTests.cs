using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrendanRusso_V2_PartOne;

namespace BoardCellTests
{
    [TestClass]
    public class MOCKMessageHandlerTests
    {
        [TestMethod]
        public void TestTryMethod()
        {
            MessageHandler handler = new MOCKMessageHandler();

            //Need to get to the right state first
            handler.connectTo("126.5.6.7",50000);
            handler.sendRequest("ID,someone");
            handler.sendRequest("ID,bob");

            //Send bad info
            handler.sendRequest("TRY,pap");
            Assert.AreEqual("ERROR", handler.getResponse());
            
            //Board is not set up yet
            handler.sendRequest("TRY,someone,N32,N43");
            Assert.AreEqual("DONE", handler.getResponse());

            //Send valid name
            handler.sendRequest("TRY,bob,N61,N52");
            Assert.AreEqual("DONE", handler.getResponse());

            //Move to invalid location
            handler.sendRequest("TRY,someone,N43,N53");
            Assert.AreEqual("ERROR", handler.getResponse());

            //Send invalid name
            handler.sendRequest("TRY,j0hn,N12,N34");
            Assert.AreEqual("ERROR", handler.getResponse());

            //Send invalid location
            handler.sendRequest("TRY,someone,N61,N52");
            Assert.AreEqual("ERROR", handler.getResponse());

            //Point to a random location
            handler.sendRequest("TRY,someone,N44,N33");
            Assert.AreEqual("ERROR", handler.getResponse());
        }
    }
}
