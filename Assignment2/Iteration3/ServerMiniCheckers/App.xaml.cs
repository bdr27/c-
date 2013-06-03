using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ServerMiniCheckers.Utility;

namespace ServerMiniCheckers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow serverMiniCheckers;
        private DBHandler dbHandler;
        private ServerState serverState = ServerState.STOPPED;

        public App()
            : base ()
        {
            serverMiniCheckers = new MainWindow();
            serverMiniCheckers.UpdateMenuState(serverState);
            dbHandler = new MOCKDBHandler();
            dbHandler.LoadHighScores();
            serverMiniCheckers.LoadLeaderboard(dbHandler.GetHighScores());
            WireHandlers();
            serverMiniCheckers.Show();
            
            LoadMOCK();
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
            Debug.WriteLine("Server Stopping");
        }

        private void HandleMenuStartServer(object sender, RoutedEventArgs e)
        {
            serverState = ServerState.RUNNING;
            serverMiniCheckers.UpdateMenuState(serverState);
            Debug.WriteLine("Server Starting");
        }

        private void HandlerMenuMulticastSetup(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Setup Multicast");
        }

        private void HandleMenuSetNetwork(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Setup Network");
        }
    }
}
