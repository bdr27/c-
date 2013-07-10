using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientMiniCheckers.Utility;

namespace MiniCheckersUnitTests
{
    [TestClass]
    public class SplitPlayerPiecesTests
    {
        [TestMethod]
        public void CheckPlayerPieceSplit()
        {
            var pieces = "N11N22|N34N56";
            var player1String = pieces.Split('|')[0];
            var player2String = pieces.Split('|')[1];

            var player1 = SplitPlayerPieces.GetPieces(player1String);

            Assert.AreEqual(0, player1[0]);
            Assert.AreEqual(9, player1[1]);

            var player2 = SplitPlayerPieces.GetPieces(player2String);
            Assert.AreEqual(19, player2[0]);
            Assert.AreEqual(37, player2[1]);
        }
    }
}
