using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClientMiniCheckers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow clientMiniCheckers;
        public App()
            : base()
        {
            clientMiniCheckers = new MainWindow();
            clientMiniCheckers.Show();
            WireHandlers();
        }

        private void WireHandlers()
        {
            clientMiniCheckers.AddMenuLoginHandler(HandleMenuLogin);
            clientMiniCheckers.AddMenuLogoutHandler(HandleMenuLogout);
            clientMiniCheckers.AddMenuSetMulticastHandler(HandleMenuSetMulticast);
            clientMiniCheckers.AddMenuSetNetworkHandler(HandlerSetNetwork);
        }

        private void HandlerSetNetwork(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Set network");
        }

        private void HandleMenuSetMulticast(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Set multicast");
        }

        private void HandleMenuLogout(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Set logout");
        }

        private void HandleMenuLogin(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Set login");
        }
    }
}
