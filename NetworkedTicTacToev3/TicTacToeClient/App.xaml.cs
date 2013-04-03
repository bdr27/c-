using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
        private string player1Name;
        private string player2Name;
        private MainWindow player2Window;
        private String networkIPAddress;
        private bool player2Display = false;
        private MessageHandler handler;

        public App()
            : base()
        {
            handler = new MOCKMessageHandler();
            handler.connectTo("127.0.0.1", 50000);
            //handler.sendRequest("SET_NAME,BrendanRusso");
            Debug.WriteLine(handler.getResponse());
            handler.sendRequest("PUT,BrendanRusso,3,1");
            Debug.WriteLine(handler.getResponse());

            player1Window = new MainWindow();         

            player1Window.Title = "Player 1 Window";            

            //Wires handlers
            wireHandlers(player1Window);
            player1Window.AddSetNameMenuItemHandler(HandleSetNamePlayer1MenuItem);
            
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
            if(networkIPAddress.Equals("127.0.0.1") && !player2Display)
            {
                player2Display = true;
                player2Window = new MainWindow();
                player2Window.Title = "Player 2 Window";
                wireHandlers(player2Window);
                player2Window.Show();
                player2Window.AddSetNameMenuItemHandler(HandleSetNamePlayer2MenuItem);
            }
        }

        private void HandleSetNamePlayer2MenuItem(object sender, RoutedEventArgs e)
        {
            NameInformationWindow dialog = new NameInformationWindow();
            dialog.Owner = player2Window;
            dialog.ShowDialog();
            player2Name = dialog.getName();
            Debug.WriteLine("Name is: " + player2Name);
            handler.sendRequest("SET_NAME," + player2Name);

        }

        private void HandleSetNamePlayer1MenuItem(Object sender, RoutedEventArgs e)
        {
            NameInformationWindow dialog = new NameInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            player1Name = dialog.getName();
            Debug.WriteLine("Name is: " + player1Name);
            handler.sendRequest("SET_NAME," + player1Name);
        }
    }
}


