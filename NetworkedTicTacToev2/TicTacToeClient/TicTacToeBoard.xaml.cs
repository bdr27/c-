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

        public void ShowPiece(int position, bool isShowing)
        {
            int value = isShowing ? 1 : 0;
            Panel.SetZIndex(MainGrid.Children[position], value);
        }

        public void SetPieceColor(int position, Brush brush)
        {
            Ellipse ellipse = (Ellipse) MainGrid.Children[position];
            ellipse.Fill = brush;
        }

        public void AddMouseHandler(MouseButtonEventHandler handler)
        {
            MouseDown += handler;
        }

        internal void HitTest(Point point)
        {
            UIElement element = (UIElement)InputHitTest(point);
            int row = Grid.GetRow(element);
            int col = Grid.GetColumn(element);
            System.Diagnostics.Debug.WriteLine("row: {0} col: {1}", row, col);
        }
    }
}
