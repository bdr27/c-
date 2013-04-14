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
        private string playerName;

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
            playerWindow.NameMenuItemHandler(HandleNameMenuItem);
        }

        private void HandleIPAddressMenuItem(object sender, RoutedEventArgs e)
        {
            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = mainWindow;
            dialog.ShowDialog();
            networkIPAddress = dialog.GetIPAddress();
            mainWindow.setName.IsEnabled = true;
            mainWindow.GameStatus.Text = "Status: Please enter your name";
        }
        private void HandleNameMenuItem(object sender, RoutedEventArgs e)
        {
            NameInformationWindow dialog = new NameInformationWindow();
            dialog.Owner = mainWindow;
            dialog.ShowDialog();
            playerName = dialog.GetName();
            mainWindow.GameStatus.Text = "Status: Ready player 1";
        }
    }
}
