using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ServerMiniCheckers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow serverMiniCheckers;
       // private object HandleSetNetworkMenuItem;

        public App()
            : base ()
        {
            serverMiniCheckers = new MainWindow();            
            WireHandlers();
            serverMiniCheckers.Show();
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
            Debug.WriteLine("Server Stopping");
        }

        private void HandleMenuStartServer(object sender, RoutedEventArgs e)
        {
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
