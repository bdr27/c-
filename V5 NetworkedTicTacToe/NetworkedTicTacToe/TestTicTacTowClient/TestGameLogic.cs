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

        [TestMethod]
        public void TestIsValidMove()
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.StartNewGame();
            string player1Name = "Brendan";
            gameLogic.Player1Name = player1Name;
            Assert.AreEqual(true, gameLogic.IsValidMove("13", player1Name));
        }

        [TestMethod]
        public void TestMove()
        {
            GameLogic gameLogic = new GameLogic();
            gameLogic.StartNewGame();
            string move = "13";
            gameLogic.Move(move);
            var cell = gameLogic.board.cells[0,2];
            Assert.AreEqual(Piece.PLAYER1, cell.piece);
        }
    }
}
