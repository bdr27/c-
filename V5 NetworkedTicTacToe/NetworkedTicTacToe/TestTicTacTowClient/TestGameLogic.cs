using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToeClient.Model;
using TicTacToeClient;

namespace TestTicTacTowClient
{
    [TestClass]
    public class TestGameLogic
    {
        [TestMethod]
        public void TestNewGame()
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.StartNewGame();
            foreach (var cell in gameLogic.board.cells)
            {
                Assert.AreEqual(Piece.EMPTY, cell.piece);
            }
            Assert.AreEqual(Piece.PLAYER1, gameLogic.currentPlayerPiece);
        }
    }
}
