using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace TicTacToeClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow player1Window;
        private MainWindow player2Window;
        private String networkIPAddress;

        public App()
            : base()
        {
            player1Window = new MainWindow();         

            player1Window.Title = "Player 1 Window";            

            wireHandlers(player1Window);
            
            //player1Window.AddIPAddressMenuItemHandler(HandleIPAddressMenuItem);
            //player1Window.GameBoard.AddMouseHandler(HandleMouseEvent);

            player1Window.Show();
            //player2Window.Show();                     
        }

        private void wireHandlers(MainWindow playerWindow)
        {
            playerWindow.AddIPAddressMenuItemHandler(HandleIPAddressMenuItem);
            playerWindow.GameBoard.AddMouseHandler(HandleMouseEvent);
        }

        private void HandleMouseEvent(object sender, MouseButtonEventArgs e)
        {
            TicTacToeBoard board = (TicTacToeBoard)sender;
            IInputElement element = board.InputHitTest(e.GetPosition(board));
            board.HitTest(e.GetPosition(board));

            System.Diagnostics.Debug.WriteLine("element: " + element);
            System.Diagnostics.Debug.WriteLine("sender: " + sender);
            System.Diagnostics.Debug.WriteLine("e: " + e);
        }

        private void HandleIPAddressMenuItem(object sender, RoutedEventArgs e)
        {
            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            networkIPAddress = dialog.GetIPAddress();
            System.Diagnostics.Debug.WriteLine("Address is: " + networkIPAddress);
            if(networkIPAddress.Equals("127.0.0.1"))
            {
                player2Window = new MainWindow();
                player2Window.Title = "Player 2 Window";
                wireHandlers(player2Window);
                player2Window.Show();
            }
        }
    }
}


