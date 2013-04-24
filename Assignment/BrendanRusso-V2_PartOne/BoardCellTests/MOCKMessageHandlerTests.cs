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

            //Send valid name
            hander.sendRequest("TRY,someone,N12,N34");
            Assert.AreEqual("DONE", hander.getResponse());

            //Send invalid name
            hander.sendRequest("TRY,j0hn,N12,N34");
            Assert.AreEqual("ERROR", hander.getResponse());
        }
    }
}
