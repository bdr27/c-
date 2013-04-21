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

namespace BrendanRusso_MiniCheckers_v1
{
    /// <summary>
    /// Interaction logic for MiniCheckersBoard.xaml
    /// </summary>
    public partial class MiniCheckersBoard : UserControl
    {
        Board board;
        public MiniCheckersBoard()
        {
            board = new Board();
            InitializeComponent();
            Rectangle rect = new Rectangle();
            rect.Visibility = System.Windows.Visibility.Visible;
      
            Console.WriteLine("Number of children: " + CheckerBoard.Children.Count);
            Console.WriteLine("This is stupid: " + CheckerBoard.Children.ToString());
            Grid.SetColumn(rect, 1);
            Grid.SetRow(rect, 2);
            CheckerBoard.Children.Add(rect);
        }
    }
}
