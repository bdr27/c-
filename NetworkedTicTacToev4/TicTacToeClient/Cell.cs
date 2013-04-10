using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToeClient
{
    public class Cell
    {
        internal int row, col, ID;
        internal Piece piece;
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
                    message = message + winList[i][j].getID() + ",";
                }
                message = message.Remove(message.Length - 1);
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

        private List<List<Cell>> setupwinListForCell_2()
        {
            List<List<Cell>> winLists = new List<List<Cell>>();
            List<Cell> winList = new List<Cell>();
            winList.Add(new Cell(1));
            winList.Add(new Cell(3));
            winLists.Add(winList); //ID 1,3

            winList = new List<Cell>();
            winList.Add(new Cell(5));
            winList.Add(new Cell(8));
            winLists.Add(winList); //ID 5,8

            return winLists;
        }

        private List<List<Cell>> setupwinListForCell_3()
        {
            List<List<Cell>> winLists = new List<List<Cell>>();
            List<Cell> winList = new List<Cell>();
            winList.Add(new Cell(2));
            winList.Add(new Cell(1));
            winLists.Add(winList); //ID 2,1

            winList = new List<Cell>();
            winList.Add(new Cell(5));
            winList.Add(new Cell(7));
            winLists.Add(winList); //ID 5,7

            winList = new List<Cell>();
            winList.Add(new Cell(6));
            winList.Add(new Cell(9));
            winLists.Add(winList); //ID 6,9

            return winLists;
        }

        private List<List<Cell>> setupwinListForCell_4()
        {
            List<List<Cell>> winLists = new List<List<Cell>>();
            List<Cell> winList = new List<Cell>();
            winList.Add(new Cell(1));
            winList.Add(new Cell(7));
            winLists.Add(winList); //ID 1,7

            winList = new List<Cell>();
            winList.Add(new Cell(5));
            winList.Add(new Cell(6));
            winLists.Add(winList); //ID 5,6

            return winLists;
        }

        private List<List<Cell>> setupwinListForCell_5()
        {
            List<List<Cell>> winLists = new List<List<Cell>>();
            List<Cell> winList = new List<Cell>();
            winList.Add(new Cell(2));
            winList.Add(new Cell(8));
            winLists.Add(winList); //ID 2,8

            winList = new List<Cell>();
            winList.Add(new Cell(1));
            winList.Add(new Cell(9));
            winLists.Add(winList); //ID 1,9

            winList = new List<Cell>();
            winList.Add(new Cell(3));
            winList.Add(new Cell(7));
            winLists.Add(winList); //ID 3,7

            winList = new List<Cell>();
            winList.Add(new Cell(4));
            winList.Add(new Cell(6));
            winLists.Add(winList); //ID 4,6

            return winLists;
        }

        private List<List<Cell>> setupwinListForCell_6()
        {
            List<List<Cell>> winLists = new List<List<Cell>>();
            List<Cell> winList = new List<Cell>();
            winList.Add(new Cell(9));
            winList.Add(new Cell(3));
            winLists.Add(winList); //ID 9,3

            winList = new List<Cell>();
            winList.Add(new Cell(4));
            winList.Add(new Cell(5));
            winLists.Add(winList); //ID 4,5

            return winLists;
        }

        private List<List<Cell>> setupwinListForCell_7()
        {
            List<List<Cell>> winLists = new List<List<Cell>>();
            List<Cell> winList = new List<Cell>();
            winList.Add(new Cell(1));
            winList.Add(new Cell(4));
            winLists.Add(winList); //ID 1,4

            winList = new List<Cell>();
            winList.Add(new Cell(3));
            winList.Add(new Cell(5));
            winLists.Add(winList); //ID 3,5

            winList = new List<Cell>();
            winList.Add(new Cell(8));
            winList.Add(new Cell(9));
            winLists.Add(winList); //ID 8,9

            return winLists;
        }

        private List<List<Cell>> setupwinListForCell_8()
        {
            List<List<Cell>> winLists = new List<List<Cell>>();
            List<Cell> winList = new List<Cell>();
            winList.Add(new Cell(2));
            winList.Add(new Cell(5));
            winLists.Add(winList); //ID 2,5

            winList = new List<Cell>();
            winList.Add(new Cell(9));
            winList.Add(new Cell(7));
            winLists.Add(winList); //ID 9,7

            return winLists;
        }

        private List<List<Cell>> setupwinListForCell_9()
        {
            List<List<Cell>> winLists = new List<List<Cell>>();
            List<Cell> winList = new List<Cell>();
            winList.Add(new Cell(1));
            winList.Add(new Cell(5));
            winLists.Add(winList); //ID 1,5

            winList = new List<Cell>();
            winList.Add(new Cell(8));
            winList.Add(new Cell(7));
            winLists.Add(winList); //ID 7,8

            winList = new List<Cell>();
            winList.Add(new Cell(3));
            winList.Add(new Cell(6));
            winLists.Add(winList); //ID 3,6

            return winLists;
        }
    }
}
