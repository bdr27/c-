using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ServerMiniCheckers.Modal;
using ServerMiniCheckers.Network;
using ServerMiniCheckers.Utility;


namespace ServerMiniCheckers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow serverMiniCheckers;
        private NetworkDetails networkDetails = null;
        private NetworkDetails multiCastDetails = null;
        private DBHandler dbHandler;
        private List<string> loggedOnUsers;
        private ServerState serverState = ServerState.STOPPED;
        private Thread requestResponseThread;
        private bool running;
        private MessageHandler udpMessageHandler;
        private BroadcastSender multicastSender;
        private GameState gameState;
        private string player1;
        private string player2;
        private Board board;
        private int gameMoves = 0;

        public App()
            : base()
        {
            serverMiniCheckers = new MainWindow();
            dbHandler = new MOCKDBHandler ();
            running = false;
            udpMessageHandler = new ServerUDPMessageHandler();
            multicastSender = new BroadcastSender();
            board = new Board();
            gameState = GameState.WAIT_FOR_GAME_START;

            SetupThreads();
            WireHandlers();
            LoadMOCK();
            CheckStartServer();
            GetNetworkInfo();

            dbHandler.LoadHighScores();
            serverMiniCheckers.LoadLeaderboard(dbHandler.GetHighScores());
            serverMiniCheckers.Show();

            updateRequestResponse("System Startup Complete");
        }

        private void SetupThreads()
        {
            requestResponseThread = new Thread(new ThreadStart(RequestResponseBackground_Thread));
        }

        private void RequestResponseBackground_Thread()
        {
            multicastSender.ConnectTo(multiCastDetails.GetIPAddress(), multiCastDetails.GetPortNumber());
            udpMessageHandler.ConnectTo(networkDetails.GetIPAddress(), networkDetails.GetPortNumber());
            while (serverState.Equals(ServerState.RUNNING))
            {
                Debug.WriteLine("Listening to port: " + networkDetails.GetPortNumber());
                var response = udpMessageHandler.GetResponse();
                var request = CheckResponse(response);
                if (request.Equals("MULTICAST"))
                {

                }
                else
                {
                    udpMessageHandler.SendRequest(request);
                }
                
            }
            udpMessageHandler.Close();
        }

        private string CheckResponse(string response)
        {
            var message = "ERROR";

            if (CheckRegex.CheckLogin(response))
            {
                var usernamePassword = response.Split(',');
                var username = usernamePassword[1];
                var password = usernamePassword[2];
                if (dbHandler.IsValidLogin(username, password) && !loggedOnUsers.Contains(username))
                {
                    message = "OKAY";
                   MulticastSendPlayers();
                   updateRequestResponse("Logged in: " + username);
                }
            }
            else if (CheckRegex.CheckLogout(response))
            {
                var username = response.Split(',')[1];
                if (dbHandler.IsValidLogout(username))
                {
                    message = "OKAY";
                    MulticastSendPlayers();
                    updateRequestResponse("Logged out: " + username);
                }
            }
            else if (CheckRegex.CheckValidPlay(response))
            {
                player1 = response.Split(',')[1];
                player2 = response.Split(',')[2];
                updateRequestResponse("Game start: " + player1 + " vs " + player2);
                gameState = GameState.Player1_MOVING;
                updateRequestResponse("Current GameState: " + gameState);
                message = "MULTICAST";
                multicastSender.SendRequest(string.Format("GAMESTART,{0},{1}", player1, player2));
                board.setupStartingLocations();
                UpdateGame();
            }
            else if (CheckRegex.CheckValidTry(response))
            {
                var playerTryMove = response.Split(',');
                var playerMove = playerTryMove[1];
                Piece player;
                if (gameState.Equals(GameState.Player1_MOVING))
                {
                    player = Piece.PLAYER1;
                }
                else
                {
                    player = Piece.PLAYER2;
                }
                updateRequestResponse("Player Move:" + response);

                if (board.ValidMove(playerTryMove, player))
                {
                    gameMoves++;
                    updateRequestResponse("Valid Move");
                    message = "DONE";
                    UpdatePlayers();
                    UpdateGame();
                }
                else
                {
                    updateRequestResponse("Invalid Move");
                }
            }
            return message;
        }

        private void UpdatePlayers()
        {
            if (gameState.Equals(GameState.Player1_MOVING))
            {
                gameState = GameState.Player2_MOVING;  
            }
            else if(gameState.Equals(GameState.Player2_MOVING))
            {
                gameState = GameState.Player1_MOVING;
            }
            updateRequestResponse("Current GameStats: " + gameState);
        }

        private void UpdateGame()
        {
            string playerPieces = board.getPlayerPieces();
            updateRequestResponse("Sending Pieces: " + playerPieces);
            BroadcastGameState(playerPieces);
            BroadcastStatus();
        }

        private void BroadcastStatus()
        {
            var status = "STATUS";

            if (board.CheckWin())
            {
                if (gameState.Equals(GameState.Player1_MOVING))
                {
                    status = status + "," + player2 + "|WON" + "," + player1 + "|LOST";
                    dbHandler.InsertHighScore(0, new Score(player2, gameMoves));
                }
                else if (gameState.Equals(GameState.Player2_MOVING))
                {
                    status = status + "," + player1 + "|WON" + "," + player2 + "|LOST";
                    dbHandler.InsertHighScore(0, new Score(player1, gameMoves));
                }
                serverMiniCheckers.LoadLeaderboard(dbHandler.GetHighScores());
            }
            else
            {

                if (gameState.Equals(GameState.Player1_MOVING))
                {
                    status = status + "," + player1 + "|MOVING" + "," + player2 + "|WAITING";
                }
                else if (gameState.Equals(GameState.Player2_MOVING))
                {
                    status = status + "," + player2 + "|MOVING" + "," + player1 + "|WAITING";
                }
            }
            updateRequestResponse("Sending Status: " + status);
            multicastSender.SendRequest(status);
        }

        private void BroadcastGameState(string pieces)
        {
            multicastSender.SendRequest(string.Format("GAMESTATE,{0}",pieces));
        }

        private void MulticastSendPlayers()
        {
            var players = GetMulticastBroadcasts.GetListOfPlayers(dbHandler.GetPlayers());
            var message = GetMulticastBroadcasts.GetMulticastSendPlayersMessage(players);
            multicastSender.SendRequest(message);
        }

        private void GetNetworkInfo()
        {
            IPAddressModal ipAddress = new IPAddressModal();
            ipAddress.SetStaticIP();
            networkDetails = ipAddress.GetNetworkDetails();
        }

        private void updateRequestResponse(string message)
        {
            serverMiniCheckers.UpdateRequestRespnseLog(DateTime.Now.ToString("HH:mm tt") + ": " + message);
        }

        private void LoadMOCK()
        {
            Dispatcher.Invoke(() =>
                {
                    dbHandler.InsertHighScore(2, new Score("jill", 3));
                    serverMiniCheckers.LoadLeaderboard(dbHandler.GetHighScores());
                });
        }

        private void WireHandlers()
        {
            serverMiniCheckers.AddMenuNetworkSetupHandler(HandleMenuSetNetwork);
            serverMiniCheckers.AddMenuMulticastSetupHandler(HandlerMenuMulticastSetup);
            serverMiniCheckers.AddMenuStartServerHandler(HandleMenuStartServer);
            serverMiniCheckers.AddMenuStopServerHandler(HandleMenuStopServer);
        }

        private void DisableNetwork()
        {
            requestResponseThread.Interrupt();
            udpMessageHandler.Close();
        }

        private void HandleMenuStopServer(object sender, RoutedEventArgs e)
        {

            serverState = ServerState.STOPPED;
            serverMiniCheckers.UpdateMenuState(serverState);
            DisableNetwork();
            loggedOnUsers = null;

            updateRequestResponse("Server stopped");
            Debug.WriteLine("Server Stopping");
        }

        private void HandleMenuStartServer(object sender, RoutedEventArgs e)
        {
            loggedOnUsers = new List<string>();
            serverState = ServerState.RUNNING;
            requestResponseThread.Start();
            serverMiniCheckers.UpdateMenuState(serverState);
            updateRequestResponse("Server started");
            updateRequestResponse("Current GameState: " + gameState);
            Debug.WriteLine("Server Starting");
        }

        private void HandlerMenuMulticastSetup(object sender, RoutedEventArgs e)
        {
            var dialog = new IPAddressModal();
            dialog.Owner = serverMiniCheckers;
            dialog.SetMultiCast();
            dialog.ShowDialog();
            multiCastDetails = dialog.GetNetworkDetails();
            CheckStartServer();
            updateRequestResponse("Setting multicast details to: " + multiCastDetails.ToString());
            Debug.WriteLine(multiCastDetails.ToString());
            Debug.WriteLine("Setup Multicast");
        }

        private void HandleMenuSetNetwork(object sender, RoutedEventArgs e)
        {
            var dialog = new IPAddressModal();
            dialog.Owner = serverMiniCheckers;
            dialog.SetStaticIP();
            dialog.ShowDialog();
            networkDetails = dialog.GetNetworkDetails();
            CheckStartServer();
            updateRequestResponse("Setting network details to: " + networkDetails.ToString());
            Debug.WriteLine(networkDetails.ToString());
            Debug.WriteLine("Setup Network");
        }

        private void CheckStartServer()
        {
            if (networkDetails == null || multiCastDetails == null)
            {
                serverMiniCheckers.DisableServer();
            }
            else
            {
                serverMiniCheckers.UpdateMenuState(serverState);
            }
        }
    }
}
