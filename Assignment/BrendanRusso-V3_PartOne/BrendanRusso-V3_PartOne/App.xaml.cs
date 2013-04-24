using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BrendanRusso_V3_PartOne
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow player1Window;
        private MainWindow player2Window;
        private string networkIPAddress;
        private int portNumber;
        private MessageHandler handler;
        private GameState state;
        private string player1Name;
        private string player2Name;

        public App()
            : base()
        {
            player1Window = new MainWindow();
            player1Window.Show();
            player2Window = new MainWindow();
            player2Window.IPAddressMenuItem.IsEnabled = false;
            player1Window.Show();
            player2Window.Show();

            handler = new MOCKMessageHandler();
            //wire the handlers
            wireHandlers(player1Window);
            wireHandlers(player2Window);
            player1Window.NameMenuItemHandler(HandleNameMenuItemPlayer1);
            player2Window.NameMenuItemHandler(HandleNameMenuItemPlayer2);

        }

        private void wireHandlers(MainWindow playerWindow)
        {
            playerWindow.AddIPAddressMenuItemHandler(HandleIPAddressMenuItem);
            //playerWindow.NameMenuItemHandler(HandleNameMenuItemPlayer1);
        }

        private void HandleIPAddressMenuItem(object sender, RoutedEventArgs e)
        {
            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            networkIPAddress = dialog.GetIPAddress();
            portNumber = dialog.getPortNumber();
            handler.connectTo(networkIPAddress, portNumber);
            if(handler.getResponse().Equals("VALID"))
            {
                player1Window.IPAddressMenuItem.IsEnabled = false;
                player1Window.setName.IsEnabled = true;
                state = GameState.WAIT_PLAYER1;
                player1Window.setStatusEnterYourName();
                player2Window.setStatusOpponmentName();
            }            
        }

        private void HandleNameMenuItemPlayer1(object sender, RoutedEventArgs e)
        {
            NameInformationWindow dialog = new NameInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            handler.sendRequest(String.Format("ID,{0}", dialog.GetName()));
            if (handler.getResponse().Equals("PLAYER1"))
            {
                player1Window.setName.IsEnabled = false;
                player2Window.setName.IsEnabled = true;
                player2Window.setStatusEnterYourName();
                player1Window.setStatusOpponmentName();
            }
            else
            {
                player1Window.setStatusInvalidName();
            }
            
        }
        private void HandleNameMenuItemPlayer2(object sender, RoutedEventArgs e)
        {
            NameInformationWindow dialog = new NameInformationWindow();
            dialog.Owner = player2Window;
            dialog.ShowDialog();
            handler.sendRequest(String.Format("ID,{0}", dialog.GetName()));
            if (handler.getResponse().Equals("PLAYER2"))
            {
                player2Window.setName.IsEnabled = false;
                player1Window.setStatusYourMove();
                player2Window.setStatusOpponentsMove();
                drawBoard();
            }
            else
            {
                player2Window.setStatusInvalidName();
            }            
        }
        private void drawBoard()
        {
            handler.sendRequest("UPDATE");
            string[] playerPieces = handler.getResponse().Split('|');
            List<int> player1Pieces = getListPlayerPieces(playerPieces[0]);
            List<int> player2Pieces = getListPlayerPieces(playerPieces[1]);
            
            Debug.WriteLine(playerPieces);
            player1Window.updateDisplay(player1Pieces, player2Pieces);
            player2Window.updateDisplay(player1Pieces, player2Pieces);
        }

        private List<int> getListPlayerPieces(string playerPieces)
        {
            List<int> playerPiecesID = new List<int>();
            string[] piecesLocation = playerPieces.Split('N');
            for (int i = 1; i < piecesLocation.Length; i++)
            {
                int row = Int32.Parse(piecesLocation[i][0].ToString()) - 1;
                int col = Int32.Parse(piecesLocation[i][1].ToString()) - 1;
                Debug.WriteLine(string.Format("row: {0}, col: {1}", row, col));
                playerPiecesID.Add(row * 8 + col);
            }
            return playerPiecesID;
        }
    }
}
