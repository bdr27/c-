using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TicTacToeClient
{
    /// <summary>
    /// Interaction logic for TicTacToeBoard.xaml
    /// </summary>
    public partial class TicTacToeBoard : UserControl
    {
        public TicTacToeBoard()
        {
            InitializeComponent();
        }

        public void HitTest(Point point)
        {
            UIElement element = (UIElement)InputHitTest(point);
            int row = Grid.GetRow(element);
            int col = Grid.GetColumn(element);
            System.Diagnostics.Debug.WriteLine("row: {0} col: {1}", row, col);
        }

        public void GetGridPosition(Point point, out int row, out int col)
        {
            UIElement element = (UIElement)InputHitTest(point);
            row = Grid.GetRow(element) + 1;
            col = Grid.GetColumn(element) + 1; // make these start from 1
        }

        public void AddMouseHandler(MouseButtonEventHandler handler)
        {
            MouseDown += handler;
        }

        public void ShowPiece(int position, bool isShowing = true)
        {
            int value = isShowing ? 1 : 0;
            Panel.SetZIndex(MainGrid.Children[position], value);
        }

        public void SetPieceColor(int position, Brush brush)
        {
            Ellipse ellipse = (Ellipse)MainGrid.Children[position];
            ellipse.Fill = brush;
        }

        public void ResetPieces()
        {
            for (var i = 0; i < 9; ++i)
            {
                ShowPiece(i, false);
            }
        }

        public void PlacePieces(string piecesString)
        {
            ResetPieces();

            var elements = piecesString.Split('|');
            PlacePieces(elements[0], Piece.PLAYER1);
            PlacePieces(elements[1], Piece.PLAYER2);
        }

        private void PlacePieces(string pieces, Piece piece)
        {
            if (pieces == "") return;

            var positions = pieces.Remove(0, 1).Split('N');

            foreach (var position in positions)
            {
                int row = int.Parse(position.Substring(0, 1)) - 1;
                int col = int.Parse(position.Substring(1, 1)) - 1;
                int index = row * 3 + col;

                ShowPiece(index);
                var brush = piece == Piece.PLAYER1 ? Brushes.Red : Brushes.Blue;
                SetPieceColor(index, brush);
            }
        }
    }
}
