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
        private MainWindow player1Window;
        private MainWindow player2Window;
        private string networkIPAddress;
        private string playerName;

        public App()
            : base()
        {
            //TODO incorperate this with the view so when I click on a space it highlights red
            player1Window = new MainWindow();
            player2Window = new MainWindow();
            player1Window.Show();
            player2Window.Show();

            //wire the handlers
            wireHandlers(player1Window);
            wireHandlers(player2Window);

        }

        private void wireHandlers(MainWindow playerWindow)
        {
            playerWindow.AddIPAddressMenuItemHandler(HandleIPAddressMenuItem);
            playerWindow.NameMenuItemHandler(HandleNameMenuItem);
        }

        private void HandleIPAddressMenuItem(object sender, RoutedEventArgs e)
        {
            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            networkIPAddress = dialog.GetIPAddress();
            player1Window.setName.IsEnabled = true;
            player1Window.GameStatus.Text = "Status: Please enter your name";
        }
        private void HandleNameMenuItem(object sender, RoutedEventArgs e)
        {
            NameInformationWindow dialog = new NameInformationWindow();
            dialog.Owner = player1Window;
            dialog.ShowDialog();
            playerName = dialog.GetName();
            player1Window.GameStatus.Text = "Status: Ready player 1";
        }
    }
}
