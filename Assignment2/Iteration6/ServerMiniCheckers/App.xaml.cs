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

        public App()
            : base()
        {
            serverMiniCheckers = new MainWindow();
            dbHandler = new MOCKDBHandler();
            running = false;
            udpMessageHandler = new ServerUDPMessageHandler();
            multicastSender = new BroadcastSender();
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
                udpMessageHandler.SendRequest(request);

                if (CheckRegex.CheckLogin(response) || CheckRegex.CheckLogout(response))
                {
                    MulticastSendPlayers();
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
                   // MulticastSendPlayers();
                }
            }
            else if (CheckRegex.CheckLogout(response))
            {
                var username = response.Split(',')[1];
                if (dbHandler.IsValidLogout(username))
                {
                    message = "OKAY";
                  //  MulticastSendPlayers();
                }
            }
            return message;
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
            dbHandler.InsertHighScore(2, new Score("jill", 3));
            serverMiniCheckers.LoadLeaderboard(dbHandler.GetHighScores());
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
