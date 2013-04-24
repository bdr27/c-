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
            MessageHandler hander = new MOCKMessageHandler();

            //Need to get to the right state first
            hander.connectTo("126.5.6.7",50000);
            hander.sendRequest("ID,someone");
            hander.sendRequest("ID,bob");
            
            //Board is not set up yet
            hander.sendRequest("TRY,someone,N32,N43");
            Assert.AreEqual("DONE", hander.getResponse());

            //Send valid name
            hander.sendRequest("TRY,bob,N61,N52");
            Assert.AreEqual("DONE", hander.getResponse());

            //Move to invalid location
            hander.sendRequest("TRY,someone,N43,N53");
            Assert.AreEqual("ERROR", hander.getResponse());

            //Send invalid name
            hander.sendRequest("TRY,j0hn,N12,N34");
            Assert.AreEqual("ERROR", hander.getResponse());
        }
    }
}
