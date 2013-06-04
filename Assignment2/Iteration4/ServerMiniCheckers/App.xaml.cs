using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
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
        private List<string> requestResponseLog;
        private ServerState serverState = ServerState.STOPPED;

        public App()
            : base ()
        {
            serverMiniCheckers = new MainWindow();
            requestResponseLog = new List<string>();
            dbHandler = new MOCKDBHandler();

            WireHandlers();
            LoadMOCK();
            CheckStartServer();
            GetNetworkInfo();          
            
            dbHandler.LoadHighScores();
            serverMiniCheckers.LoadLeaderboard(dbHandler.GetHighScores());
            serverMiniCheckers.Show();
            
            
            updateRequestResponse("System Startup Complete");
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

        private void HandleMenuStopServer(object sender, RoutedEventArgs e)
        {
            serverState = ServerState.STOPPED;
            serverMiniCheckers.UpdateMenuState(serverState);
            
            updateRequestResponse("Server stopped");
            Debug.WriteLine("Server Stopping");
        }

        private void HandleMenuStartServer(object sender, RoutedEventArgs e)
        {
            serverState = ServerState.RUNNING;
            serverMiniCheckers.UpdateMenuState(serverState);
            updateRequestResponse("Server started");
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
