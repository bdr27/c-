using System.Windows;

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
