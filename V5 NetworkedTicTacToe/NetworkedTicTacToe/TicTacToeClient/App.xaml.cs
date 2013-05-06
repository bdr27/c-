using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace TicTacToeClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow player1Window, player2Window;
        private string networkIPAddress;
        private MessageHandler handler;
        private string player1Name, player2Name;

        private DispatcherTimer timer;

        public App()
            : base()
        {
            // create long-lived objects
            handler = new MOCKMessageHandler();
            player1Window = new MainWindow();
            player1Window.Title = "Player1";
            player2Window = new MainWindow();
            player2Window.Title = "Player2";
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);

            // wire up event handler
            player1Window.AddIPAddressMenuItemHandler(HandleIPAddressMenuItemForPlayer1);
            player2Window.AddIPAddressMenuItemHandler(HandleIPAddressMenuItemForPlayer2);
            player1Window.AddNameMenuItemHandler(HandleNameMenuItemForPlayer1);
            player2Window.AddNameMenuItemHandler(HandleNameMenuItemForPlayer2);
            player1Window.GameBoard.AddMouseHandler(HandleMouseEventForPlayer1);
            player2Window.GameBoard.AddMouseHandler(HandleMouseEventForPlayer2);

            // show the Views
            player1Window.Show();
            player2Window.Show();
            timer.Start();
        }

        private void HandleIPAddressMenuItemForPlayer1(object sender, RoutedEventArgs e)
        {
            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            networkIPAddress = dialog.GetIPAddress();
            handler.ConnectTo(networkIPAddress, 50000);
        }

        private void HandleIPAddressMenuItemForPlayer2(object sender, RoutedEventArgs e)
        {
            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = player2Window;
            dialog.ShowDialog();
            networkIPAddress = dialog.GetIPAddress();
            handler.ConnectTo(networkIPAddress, 50000);
        }

        private void HandleNameMenuItemForPlayer1(object sender, RoutedEventArgs e)
        {
            SetNameWindow dialog = new SetNameWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            string name = dialog.GetName();
            handler.SendRequest(string.Format("SET_NAME,{0}", name));
            string response = handler.GetResponse();
            if (response != "ERROR")
            {
                player1Name = name;
                player1Window.SetStatus("player name accepted");
                timer.Tick += timer_TickPlayer1;
            }
            else
            {
                player1Window.SetStatus("player name not accepted, try again...");
            }
        }

        void timer_TickPlayer1(object sender, EventArgs e)
        {
            handler.SendRequest("UPDATE");
            string piecesString = handler.GetResponse();
            player1Window.GameBoard.PlacePieces(piecesString);

            handler.SendRequest("STATUS," + player1Name);
            var response = handler.GetResponse();
            if (response != "ERROR")
                player1Window.SetStatus(response);
        }

        private void HandleNameMenuItemForPlayer2(object sender, RoutedEventArgs e)
        {
            SetNameWindow dialog = new SetNameWindow();
            dialog.Owner = player2Window;
            dialog.ShowDialog();
            string name = dialog.GetName();
            handler.SendRequest(string.Format("SET_NAME,{0}", name));
            string response = handler.GetResponse();
            if (response != "ERROR")
            {
                player2Name = name;
                player2Window.SetStatus("player name accepted");
                timer.Tick += timer_TickPlayer2;
            }
            else
            {
                player2Window.SetStatus("player name not accepted, try again...");
            }
        }

        void timer_TickPlayer2(object sender, EventArgs e)
        {
            handler.SendRequest("UPDATE");
            string piecesString = handler.GetResponse();
            player2Window.GameBoard.PlacePieces(piecesString);

            handler.SendRequest("STATUS," + player2Name);
            var response = handler.GetResponse();
            if (response != "ERROR")
                player2Window.SetStatus(response);
        }

        private void HandleMouseEventForPlayer1(object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            if (player1Name == null) return;

            TicTacToeBoard board = (TicTacToeBoard)sender;
            IInputElement element = board.InputHitTest(e.GetPosition(board));
            //System.Diagnostics.Debug.WriteLine("element: " + element);
            Point point = e.GetPosition(board);
            //board.HitTest(point);
            int row, col;
            board.GetGridPosition(point, out row, out col);
            handler.SendRequest(string.Format("PUT,{0},{1}{2}", player1Name, row, col));
            string response = handler.GetResponse();
            if (response == "OKAY")
            {
                player1Window.SetStatus("move accepted");
            }
            else
            {
                player1Window.SetStatus("move not accepted, try again..");
            }
        }

        private void HandleMouseEventForPlayer2(object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            if (player2Name == null) return;

            TicTacToeBoard board = (TicTacToeBoard)sender;
            IInputElement element = board.InputHitTest(e.GetPosition(board));
            //System.Diagnostics.Debug.WriteLine("element: " + element);
            Point point = e.GetPosition(board);
            //board.HitTest(point);
            int row, col;
            board.GetGridPosition(point, out row, out col);
            handler.SendRequest(string.Format("PUT,{0},{1}{2}", player2Name, row, col));
            string response = handler.GetResponse();
            if (response == "OKAY")
            {
                player2Window.SetStatus("move accepted");
            }
            else
            {
                player2Window.SetStatus("move not accepted, try again...");
            }
        }
    }
}


