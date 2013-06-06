using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerMiniCheckers.Utility;

namespace MiniCheckersUnitTests
{
    [TestClass]
    public class CheckRegexTests
    {
        [TestMethod]
        public void CheckValidLogin()
        {
            Assert.IsTrue(CheckRegex.checkLogin("LOGIN,jack,12345"));
        }

        [TestMethod]
        public void CheckInvalidLogin()
        {
            Assert.IsFalse(CheckRegex.checkLogin("LOGIN,jack"));
        }

        [TestMethod]
        public void CheckIsValidIP()
        {
            Assert.IsTrue(CheckRegex.checkValidIP("127.0.0.1"));
        }

        [TestMethod]
        public void CheckLeadingZeroIP()
        {
            Assert.IsFalse(CheckRegex.checkValidIP("190.05.5.6"));
        }

        [TestMethod]
        public void CheckValidLogout()
        {
            Assert.IsTrue(CheckRegex.checkLogout("LOGOUT,jack"));
        }

        [TestMethod]
        public void CheckInvalidValidLogout()
        {
            Assert.IsFalse(CheckRegex.checkLogout("LOGOUT"));
        }
    }
}
