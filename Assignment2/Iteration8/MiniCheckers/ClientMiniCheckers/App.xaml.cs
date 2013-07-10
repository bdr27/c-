using System.Diagnostics;
using System.Threading;
using System.Windows;
using ClientMiniCheckers.Modal;
using ClientMiniCheckers.Utility;
using ClientMiniCheckers.Network;
using ServerMiniCheckers.Network;
using ServerMiniCheckers.Modal;
using ServerMiniCheckers.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ClientMiniCheckers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow clientMiniCheckers;
        private NetworkDetails sendAddress;
        private NetworkDetails multicastAddress;
        private MessageHandler udpHandler;
        private MessageHandler multicastHandler;
        private Thread requestResponseThread;
        private Thread multicastRequestThread;
        private MessageSent messageSent;
        private string username = "";
        private GameStatus gameStatus = GameStatus.OBSERVER;
        private Point playerOldPosition;
        private Point playerNewPosition;
        private bool mouseDown = false;

        public App()
            : base()
        {
            messageSent = MessageSent.NONE;
            clientMiniCheckers = new MainWindow();
            udpHandler = new ClientUDPMessageHandler();
            multicastHandler = new ClientUDPBroadcastHandler();
            clientMiniCheckers.Show();
            WireHandlers();
            setupRequestResponseBackgroundThead();
        }

        private void setupRequestResponseBackgroundThead()
        {
            requestResponseThread = new Thread(new ThreadStart(RequestResponseBackground_Thread));
            requestResponseThread.IsBackground = true;
            multicastRequestThread = new Thread(new ThreadStart(MulticastRequestBackgroud_Thread));
            multicastRequestThread.IsBackground = true;
        }

        private void MulticastRequestBackgroud_Thread()
        {
            while (true)
            {
                var message = multicastHandler.GetResponse();
                CheckMulticastMessage(message);
            }
        }

        private void CheckMulticastMessage(string message)
        {
            if (CheckRegex.CheckValidUsers(message))
            {
                Debug.WriteLine("Adding new players");
                var userListOfPlayers = message.Split(',');
                var listOfPlayers = new List<string>();
                for (int i = 1; i < userListOfPlayers.Length; i++)
                {
                    listOfPlayers.Add(userListOfPlayers[i]);
                }
                clientMiniCheckers.AddPlayers(listOfPlayers);
            }
            else if (CheckRegex.CheckValidGameStart(message))
            {
                var player1 = message.Split(',')[1];
                var player2 = message.Split(',')[2];
                if (player1.Equals(username))
                {
                    clientMiniCheckers.UpdateColorTitle("Navy");
                }
                else if (player2.Equals(username))
                {
                    clientMiniCheckers.UpdateColorTitle("Pink");
                }
                clientMiniCheckers.DisablePlayers();
            }
            else if (CheckRegex.CheckValidGameState(message))
            {
                var playerPieces = message.Split(',')[1];
                var player1UnfilteredPieces = playerPieces.Split('|')[0];
                var player2UnfilteredPieces = playerPieces.Split('|')[1];
                var player1Pieces = SplitPlayerPieces.GetPieces(player1UnfilteredPieces);
                var player2Pieces = SplitPlayerPieces.GetPieces(player2UnfilteredPieces);

                clientMiniCheckers.UpdateGameBoard(player1Pieces, player2Pieces);


                Debug.Write(message);
            }
            else if (message.Contains("STATUS"))
            {
                DetermineGameStatus(message);
            }
            else
            {
                Debug.WriteLine(message);
            }
        }

        private void DetermineGameStatus(string message)
        {
            var status = message.Split(',');
            for (int i = 1; i < status.Length; i++)
            {
                var indivisualStatus = status[i].Split('|');
                var playerName = indivisualStatus[0];
                if (indivisualStatus[1].Equals("WON") || indivisualStatus[1].Equals("LOST"))
                {
                    clientMiniCheckers.EnablePlayers();
                }
                if (playerName.Equals(username))
                {
                    var action = indivisualStatus[1];
                    switch (action)
                    {
                        case "MOVING":
                            updatePlayerMoving();                            
                            break;
                        case "WAITING":
                            updatePlayerPause();                            
                            break;
                        case "WON":
                            updatePlayerWon();                            
                            break;
                        case "LOST":
                            updateaPlayerLost();                            
                            break;
                    }
                    break;
                }
            }
        }

        private void updateaPlayerLost()
        {
            gameStatus = GameStatus.LOST;
            clientMiniCheckers.UpdateStatus("You lost");
        }

        private void updatePlayerWon()
        {
            gameStatus = GameStatus.WON;
            clientMiniCheckers.UpdateStatus("You won");
        }

        private void updatePlayerPause()
        {
            gameStatus = GameStatus.WAITING;
            clientMiniCheckers.UpdateStatus("waiting for opponent to move...");
        }

        private void updatePlayerMoving()
        {
            gameStatus = GameStatus.MOVING;
            clientMiniCheckers.UpdateStatus("your move...");
        }

        private void RequestResponseBackground_Thread()
        {
            while (true)
            {
                var response = udpHandler.GetResponse();
                var action = "";
                if (response.Equals("OKAY") || response.Equals("DONE"))
                {
                    switch (messageSent)
                    {
                        case MessageSent.LOGIN:
                            clientMiniCheckers.UpdateStatus("Logged on as " + username);
                            clientMiniCheckers.MenuLoginSuccessful();
                            action = "Login";
                            break;
                        case MessageSent.LOGOUT:
                            clientMiniCheckers.UpdateStatus("Logged out");
                            clientMiniCheckers.MenuLogoutSuccessful();
                            username = "";
                            action = "Logout";
                            break;
                        case MessageSent.NONE:
                            action = "wrong message";
                            break;
                        default:
                            action = "Wrong message";
                            break;
                    }
                }
                else if (response.Equals("ERROR")) 
                {
                    switch (messageSent)
                    {
                        case MessageSent.LOGIN:
                            ShowLoginErrorDialog();                            
                            action = "Login";
                            break;
                        case MessageSent.LOGOUT:
                            action = "Logout";
                            break;
                        case MessageSent.NONE:
                            action = "wrong message";
                            break;
                        default:
                            action = "Wrong message";
                            break;
                    }
                }
                Debug.WriteLine(action + ": " + response);
                messageSent = MessageSent.NONE;
            }
        }

        private void ShowLoginErrorDialog()
        {
            username = "";
            Dispatcher.Invoke(() =>
                {
                    var dialog = new LoginErrorModal();
                    dialog.Owner = clientMiniCheckers;
                    dialog.ShowDialog();
                });
        }

        private void WireHandlers()
        {
            clientMiniCheckers.AddMenuLoginHandler(HandleMenuLogin);
            clientMiniCheckers.AddMenuLogoutHandler(HandleMenuLogout);
            clientMiniCheckers.AddMenuSetMulticastHandler(HandleMenuSetMulticast);
            clientMiniCheckers.AddMenuSetNetworkHandler(HandlerSetNetwork);
            clientMiniCheckers.AddPlayersListViewHandler(HandleListView);
            clientMiniCheckers.board.AddMouseDownHandler(HandleMouseDownEvent);
            clientMiniCheckers.board.AddMouseUpHandler(HandleMouseUpEvent);
        }

        private void HandleMouseUpEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (gameStatus.Equals(GameStatus.MOVING))
            {
                MiniCheckersBoard board = (MiniCheckersBoard)sender;
                IInputElement element = board.InputHitTest(e.GetPosition(board));
                Point point = e.GetPosition(board);
                int row, col;
                board.GetGridPosition(point, out row, out col);
                playerOldPosition = new Point(col, row);
                if (mouseDown)
                {
                    udpHandler.SendRequest(string.Format("TRY,{0},N{1}{2},N{3}{4}", username, playerNewPosition.Y, playerNewPosition.X, playerOldPosition.Y, playerOldPosition.X));
                    mouseDown = false;
                }
            }            
        }

        private void HandleMouseDownEvent(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (gameStatus.Equals(GameStatus.MOVING))
            {
                MiniCheckersBoard board = (MiniCheckersBoard)sender;
                IInputElement element = board.InputHitTest(e.GetPosition(board));
                Point point = e.GetPosition(board);
                int row, col;
                board.GetGridPosition(point, out row, out col);
                playerNewPosition = new Point(col, row);
                mouseDown = true;
            }
        }

        private void HandleListView(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var opponent = clientMiniCheckers.GetSelectedPlayer();
            if (opponent != null)
            {
                if (opponent.Equals(username))
                {
                    var dialog = new PlayWithYourselfModal();
                    dialog.Owner = clientMiniCheckers;
                    dialog.ShowDialog();
                }
                else
                {
                    udpHandler.SendRequest(string.Format("PLAY,{0},{1}", username, opponent));
                }
            }
            Debug.WriteLine(opponent);
        }

        private void HandlerSetNetwork(object sender, RoutedEventArgs e)
        {
            var dialog = new IPAddressModal();
            dialog.Owner = clientMiniCheckers;
            dialog.ShowDialog();
            sendAddress = dialog.GetNetworkDetails();
            udpHandler.ConnectTo(sendAddress.GetIPAddress(), sendAddress.GetPortNumber());
            SetUserLogin();

            Debug.WriteLine("Sending to: " + sendAddress);
        }

        private void HandleMenuSetMulticast(object sender, RoutedEventArgs e)
        {
            var dialog = new IPAddressModal();
            dialog.Owner = clientMiniCheckers;
            dialog.SetMultiCast();
            dialog.ShowDialog();
            multicastAddress = dialog.GetNetworkDetails();
            multicastHandler.ConnectTo(multicastAddress.GetIPAddress(), multicastAddress.GetPortNumber());
            SetUserLogin();

            Debug.WriteLine("Set multicast to: " + multicastAddress);
        }

        private void HandleMenuLogout(object sender, RoutedEventArgs e)
        {
            messageSent = MessageSent.LOGOUT;
            udpHandler.SendRequest(string.Format("LOGOUT,{0}", username));
        }

        private void HandleMenuLogin(object sender, RoutedEventArgs e)
        {
            username = "";
            var dialog = new LoginModal();
            dialog.Owner = clientMiniCheckers;
            dialog.ShowDialog();
            messageSent = MessageSent.LOGIN;
            username = dialog.GetUsername();
            udpHandler.SendRequest(string.Format("LOGIN,{0},{1}", username, dialog.GetPassword()));
        }

        private void SetUserLogin()
        {
            if (sendAddress == null || multicastAddress == null)
            {

                clientMiniCheckers.DisableLogin();
            }
            else
            {
                StartThreads();
                clientMiniCheckers.EnableLogin();
            }
        }

        private void StartThreads()
        {
            requestResponseThread.Start();
            multicastRequestThread.Start();
        }
    }
}
