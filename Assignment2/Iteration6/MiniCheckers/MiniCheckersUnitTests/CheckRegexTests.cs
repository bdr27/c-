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
            Assert.IsTrue(CheckRegex.CheckLogin("LOGIN,jack,12345"));
        }

        [TestMethod]
        public void CheckInvalidLogin()
        {
            Assert.IsFalse(CheckRegex.CheckLogin("LOGIN,jack"));
        }

        [TestMethod]
        public void CheckIsValidIP()
        {
            Assert.IsTrue(CheckRegex.CheckValidIP("127.0.0.1"));
        }

        [TestMethod]
        public void CheckLeadingZeroIP()
        {
            Assert.IsFalse(CheckRegex.CheckValidIP("190.05.5.6"));
        }

        [TestMethod]
        public void CheckValidLogout()
        {
            Assert.IsTrue(CheckRegex.CheckLogout("LOGOUT,jack"));
        }

        [TestMethod]
        public void CheckInvalidValidLogout()
        {
            Assert.IsFalse(CheckRegex.CheckLogout("LOGOUT"));
        }

        [TestMethod]
        public void CheckValidUsers()
        {
            Assert.IsTrue(CheckRegex.CheckValidUsers("USERS,jack,jill"));
            Assert.IsTrue(CheckRegex.CheckValidUsers("USERS,"));
        }

        [TestMethod]
        public void CheckInvalidUsers()
        {
            Assert.IsFalse(CheckRegex.CheckValidUsers("USERS,jack-"));
        }

        [TestMethod]
        public void CheckValidPlay()
        {
            Assert.IsTrue(CheckRegex.CheckValidPlay("PLAY,jack,jill"));
        }

        [TestMethod]
        public void CheckInvalidPlay()
        {
            Assert.IsFalse(CheckRegex.CheckValidPlay("PLAY,john"));
        }
    }
}
