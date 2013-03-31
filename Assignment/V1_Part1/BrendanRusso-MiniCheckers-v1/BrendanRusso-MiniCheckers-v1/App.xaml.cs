using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BrendanRusso_MiniCheckers_v1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow mainWindow;
        private string networkIPAddress;

        public App()
            : base()
        {
            mainWindow = new MainWindow();
            mainWindow.Show();

            //wire the handlers
            wireHandlers(mainWindow);
        }

        private void wireHandlers(MainWindow playerWindow)
        {
            playerWindow.AddIPAddressMenuItemHandler(HandleIPAddressMenuItem);
        }

        private void HandleIPAddressMenuItem(object sender, RoutedEventArgs e)
        {
            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = mainWindow;
            dialog.ShowDialog();
            networkIPAddress = dialog.GetIPAddress();
        }
    }
}
