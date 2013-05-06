using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToeClient.Model;
using TicTacToeClient;

namespace TestTicTacTowClient
{
    [TestClass]
    public class TestGameBoard
    {
        [TestMethod]
        public void TestSize()
        {
            var board = new GameBoard();
            Assert.AreEqual(9, board.cells.Length);
        }

        [TestMethod]
        public void CheckResetPieces()
        {
            var board = new GameBoard();
            board.ResetPieces();
            foreach(var cell in board.cells)
            {
                Assert.AreEqual(Piece.EMPTY, cell.piece);
            }
        }
    }
}
