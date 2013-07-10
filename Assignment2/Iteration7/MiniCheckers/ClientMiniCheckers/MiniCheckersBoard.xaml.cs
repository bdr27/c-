using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ClientMiniCheckers
{
    /// <summary>
    /// Interaction logic for MiniCheckersBoard.xaml
    /// </summary>
    public partial class MiniCheckersBoard : UserControl
    {
        public MiniCheckersBoard()
        {
            InitializeComponent();
        }

        public void resetView()
        {
            for (int i = 64; i < 128; i++)
            {
                Ellipse ellipse = (Ellipse)CheckerBoard.Children[i];
                ellipse.Fill = null;
            }
        }

        public void updateView(List<int> player1Pieces, List<int> player2Pieces)
        {
            resetView();
            drawElipses(player1Pieces, Colors.Navy);
            drawElipses(player2Pieces, Colors.Fuchsia);
        }
        public void AddMouseHandler(MouseButtonEventHandler handler)
        {
            MouseDown += handler;
            MouseUp += handler;
        }

        public void GetGridPosition(Point point, out int row, out int col)
        {
            UIElement element = (UIElement)InputHitTest(point);
            row = Grid.GetRow(element) + 1;
            col = Grid.GetColumn(element) + 1; // make these start from 1
        }


        private void drawElipses(List<int> playerPieces, Color color)
        {
            foreach (int piece in playerPieces)
            {
                int realLocation = piece + 64;
                Ellipse ellipse = (Ellipse)CheckerBoard.Children[realLocation];
                ellipse.Fill = new SolidColorBrush(color);
                Debug.WriteLine(piece);
            }
        }
    }
}
