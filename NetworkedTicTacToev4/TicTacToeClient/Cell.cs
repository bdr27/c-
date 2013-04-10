using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeClient
{
    public class Cell
    {
        internal int row, col, ID;
        private List<List<Cell>> winList;
        internal Cell above;
        internal Cell below;
        internal Cell left;
        internal Cell right;
        public Cell()
        {
            above = below = left = right = null;
        }

        public Cell(int ID)
        {
            above = below = left = right = null;
            this.ID = ID;
        }

        public void setID(int ID)
        {
            this.ID = ID;
        }
        public override string ToString()
        {
            string aboveID = "None";
            if (above != null) aboveID = "" + above.ID;
            string belowID = "None";
            if (below != null) belowID = "" + below.ID;
            return string.Format("Cell {4} ({0},{1}) above: {2} below: {3}",
                row, col, aboveID, belowID, ID);
        }

        public int getID()
        {
            return ID;
        }

        public string printWinList()
        {
            string message = "winlist for " + ID + ": ";
            for (int i = 0; i < winList.Count; i++)
            {
                message += "(";
                for (int j = 0; j < winList[i].Count; j++)
                {
                    message += winList[i][j].getID();
                }
                message += ") ";
            }
            return message;
        }

        public void setupCells()
        {
            switch (ID)
            {
                case 1:
                    winList = setupwinListForCell_1();
                    break;
                case 2:
                    winList = setupwinListForCell_2();
                    break;
                case 3:
                    winList = setupwinListForCell_3();
                    break;
                case 4:
                    winList = setupwinListForCell_4();
                    break;
                case 5:
                    winList = setupwinListForCell_5();
                    break;
                case 6:
                    winList = setupwinListForCell_6();
                    break;
                case 7:
                    winList = setupwinListForCell_7();
                    break;
                case 8:
                    winList = setupwinListForCell_8();
                    break;
                case 9:
                    winList = setupwinListForCell_9();
                    break;
                
            }
        }

        private List<List<Cell>> setupwinListForCell_1()
        {
            List<List<Cell>> winLists = new List<List<Cell>>();
            List<Cell> winList = new List<Cell>();
            winList.Add(new Cell(2));
            winList.Add(new Cell(3));
            winLists.Add(winList); //ID 2,3

            winList = new List<Cell>();
            winList.Add(new Cell(4));
            winList.Add(new Cell(7));
            winLists.Add(winList); //ID 4,7

            winList = new List<Cell>();
            winList.Add(new Cell(5));
            winList.Add(new Cell(9));
            winLists.Add(winList); //ID 5,9

            return winLists;
        }
    }
}
