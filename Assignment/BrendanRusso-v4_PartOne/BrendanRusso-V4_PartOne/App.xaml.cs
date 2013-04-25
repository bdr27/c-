using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace BrendanRusso_V3_PartOne
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow player1Window;
        private MainWindow player2Window;
        private MessageHandler handler;
        private Thread thread;
        private string networkIPAddress;
        private int portNumber;

        private string player1Name;
        private string player2Name;
        private bool pieceSelectedPlayer1 = false;
        private bool pieceSelectedPlayer2 = false;
        private int player1OldCol, player1OldRow, player1NewRow, player1NewCol;
        private int player2OldCol, player2OldRow, player2NewRow, player2NewCol;

        public App()
            : base()
        {
            player1Window = new MainWindow();
            player2Window = new MainWindow();
            player2Window.IPAddressMenuItem.IsEnabled = false;
            player1Window.Show();
            player2Window.Show();

            setupThread();
            handler = new MOCKMessageHandler();

            //wire the handlers
            wireHandlers(player1Window);
            wireHandlers(player2Window);
            player1Window.GameBoard.AddMouseHandler(HandleMouseEventForPlayer1);
            player2Window.GameBoard.AddMouseHandler(HandleMouseEventForPlayer2);

            player1Window.NameMenuItemHandler(HandleNameMenuItemPlayer1);
            player2Window.NameMenuItemHandler(HandleNameMenuItemPlayer2);

        }

        private void setupThread()
        {
            thread = new Thread(new ThreadStart(DisplayThread));
        }

        void DisplayThread()
        {
            while (true)
            {
                handler.sendRequest("UPDATE");
                Debug.WriteLine(handler.getResponse());

                handler.sendRequest("STATUS");
                Debug.WriteLine("Status: " + handler.getResponse());

                Thread.Sleep(1000);
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            handler.sendRequest("UPDATE");
            drawBoard(handler.getResponse());

            handler.sendRequest("STATUS");
            Debug.WriteLine("Status: " + handler.getResponse());
        }

        private void wireHandlers(MainWindow playerWindow)
        {
            playerWindow.AddIPAddressMenuItemHandler(HandleIPAddressMenuItem);
        }

        private void HandleIPAddressMenuItem(object sender, RoutedEventArgs e)
        {
            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            networkIPAddress = dialog.GetIPAddress();
            portNumber = dialog.getPortNumber();
            handler.connectTo(networkIPAddress, portNumber);
            if (handler.getResponse().Equals("VALID"))
            {
                player1Window.IPAddressMenuItem.IsEnabled = false;
                player1Window.setName.IsEnabled = true;
                player1Window.setStatusEnterYourName();
                player2Window.setStatusOpponmentName();
            }
        }

        private void HandleNameMenuItemPlayer1(object sender, RoutedEventArgs e)
        {
            NameInformationWindow dialog = new NameInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            player1Name = dialog.GetName();
            handler.sendRequest(String.Format("ID,{0}", player1Name));
            if (handler.getResponse().Equals("PLAYER1"))
            {
                player1Window.setName.IsEnabled = false;
                player2Window.setName.IsEnabled = true;
                player2Window.setStatusEnterYourName();
                player1Window.setStatusOpponmentName();
            }
            else if (!player1Name.Equals(""))
            {
                player1Window.setStatusInvalidName();
            }

        }
        private void HandleNameMenuItemPlayer2(object sender, RoutedEventArgs e)
        {
            NameInformationWindow dialog = new NameInformationWindow();
            dialog.Owner = player2Window;
            dialog.ShowDialog();
            player2Name = dialog.GetName();
            handler.sendRequest(String.Format("ID,{0}", player2Name));
            if (handler.getResponse().Equals("PLAYER2"))
            {
                player2Window.setName.IsEnabled = false;
                player1Window.setStatusYourMove();
                player2Window.setStatusOpponentsMove();
                handler.sendRequest("UPDATE");
                string boardPieces = handler.getResponse();
                drawBoard(boardPieces);

                thread.Start();
            }
            else if (!player2Name.Equals(""))
            {
                player2Window.setStatusInvalidName();
            }
        }
        private void drawBoard(string response)
        {
            string[] playerPieces = response.Split('|');
            List<int> player1Pieces = getListPlayerPieces(playerPieces[0]);
            List<int> player2Pieces = getListPlayerPieces(playerPieces[1]);

            Debug.WriteLine(playerPieces);
            player1Window.GameBoard.resetView();
            player2Window.GameBoard.resetView();
            player1Window.GameBoard.updateView(player1Pieces, player2Pieces);
            player2Window.GameBoard.updateView(player1Pieces, player2Pieces);
        }

        private List<int> getListPlayerPieces(string playerPieces)
        {
            List<int> playerPiecesID = new List<int>();
            string[] piecesLocation = playerPieces.Split('N');
            for (int i = 1; i < piecesLocation.Length; i++)
            {
                int row = Int32.Parse(piecesLocation[i][0].ToString());
                int col = Int32.Parse(piecesLocation[i][1].ToString());
                Debug.WriteLine(string.Format("row: {0}, col: {1}", row, col));
                playerPiecesID.Add((row - 1) * 8 + (col - 1));
            }
            return playerPiecesID;
        }

        private void HandleMouseEventForPlayer1(object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            handler.setStatus("MOVING");
            Debug.Write(e.ToString());
            MiniCheckersBoard board = (MiniCheckersBoard)sender;
            IInputElement element = board.InputHitTest(e.GetPosition(board));
            Point point = e.GetPosition(board);
            int row, col;
            board.GetGridPosition(point, out row, out col);
            if (pieceSelectedPlayer1 == true)
            {
                pieceSelectedPlayer1 = false;
                player1NewCol = col;
                player1NewRow = row;
                //Makes sure player 1 can only go down
                if (player1OldRow < player1NewRow)
                {
                    handler.sendRequest(string.Format("TRY,{0},N{1}{2},N{3}{4}", player1Name, player1OldRow, player1OldCol, player1NewRow, player1NewCol));
                    string response = handler.getResponse();
                    if (response.Equals("DONE"))
                    {
                        handler.sendRequest("UPDATE");
                        string boardPieces = handler.getResponse();
                        drawBoard(boardPieces);
                        player2Window.setStatusYourMove();
                        player1Window.setStatusOpponentsMove();
                    }
                }
                handler.setStatus("WAITING");
            }
            else
            {
                pieceSelectedPlayer1 = true;
                player1OldCol = col;
                player1OldRow = row;
            }
            Debug.WriteLine("row: " + row + "col: " + col);
        }

        private void HandleMouseEventForPlayer2(object sender,
            System.Windows.Input.MouseButtonEventArgs e)
        {
            handler.setStatus("MOVING");
            MiniCheckersBoard board = (MiniCheckersBoard)sender;
            IInputElement element = board.InputHitTest(e.GetPosition(board));
            Point point = e.GetPosition(board);
            int row, col;
            board.GetGridPosition(point, out row, out col);
            if (pieceSelectedPlayer2 == true)
            {
                pieceSelectedPlayer2 = false;
                player2NewCol = col;
                player2NewRow = row;
                if (player2OldRow > player2NewRow)
                {
                    handler.sendRequest(string.Format("TRY,{0},N{1}{2},N{3}{4}", player2Name, player2OldRow, player2OldCol, player2NewRow, player2NewCol));
                    string response = handler.getResponse();
                    if (response.Equals("DONE"))
                    {
                        handler.sendRequest("UPDATE");
                        string boardPieces = handler.getResponse();
                        drawBoard(boardPieces);

                        player1Window.setStatusYourMove();
                        player2Window.setStatusOpponentsMove();
                    }
                }
                handler.setStatus("WAITING");
            }
            else
            {
                pieceSelectedPlayer2 = true;
                player2OldCol = col;
                player2OldRow = row;
            }
            Debug.WriteLine("row: " + row + "col: " + col);
        }
    }
}
