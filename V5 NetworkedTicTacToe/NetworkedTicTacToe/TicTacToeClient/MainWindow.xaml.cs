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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //GameBoard.ShowPiece(0, true);
            //GameBoard.SetPieceColor(0, Brushes.White);
            //GameBoard.ShowPiece(5, true);
            //GameBoard.SetPieceColor(5, Brushes.Black);
        }

        public void AddIPAddressMenuItemHandler(RoutedEventHandler handler)
        {
            IPAddressMenuItem.Click += handler;
        }

        public void AddNameMenuItemHandler(RoutedEventHandler handler)
        {
            NameMenuItem.Click += handler;
        }

        public void SetStatus(string message)
        {
            StatusBlock.Text = "Status: " + message;
        }
    }
}
