using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BrendanRusso_V2_PartOne;

namespace BoardCellTests
{
    [TestClass]
    public class BoardTests
    {
        [TestMethod]
        public void initiliseNewBoard()
        {
            Board board = new Board();

            //Checks for empty board
            Assert.AreEqual("|", board.getPlayerPieces());

            //Checks all the cells are empty
            Cell[,] cells = board.getCells();
            foreach (Cell cell in cells)
            {
                Assert.AreEqual(Piece.EMPTY, cell.getPiece());
            }
        }

        [TestMethod]
        public void testSettingUpPlayers()
        {
            Board board = new Board();
            board.setupStartingLocations();
            Assert.AreEqual("N12N14N16N18N21N23N25N27N32N34N36N38|N61N63N65N67N72N74N76N78N81N83N85N87", board.getPlayerPieces());
        }

    }
}
