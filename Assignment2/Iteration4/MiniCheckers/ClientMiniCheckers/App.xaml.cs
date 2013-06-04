using System.Diagnostics;
using System.Windows;
using ClientMiniCheckers.Modal;
using ServerMiniCheckers.Modal;
using ServerMiniCheckers.Network;

namespace ClientMiniCheckers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow clientMiniCheckers;
        NetworkDetails sendAddress;
        NetworkDetails multicastAddress;

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
            var dialog = new IPAddressModal();
            dialog.Owner = clientMiniCheckers;
            dialog.ShowDialog();
            sendAddress = dialog.GetNetworkDetails();

            Debug.WriteLine("Sending to: " + sendAddress);
        }

        private void HandleMenuSetMulticast(object sender, RoutedEventArgs e)
        {
            var dialog = new IPAddressModal();
            dialog.Owner = clientMiniCheckers;
            dialog.SetMultiCast();
            dialog.ShowDialog();
            multicastAddress = dialog.GetNetworkDetails();

            Debug.WriteLine("Set multicast to: " + multicastAddress);
        }

        private void HandleMenuLogout(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Set logout");
        }

        private void HandleMenuLogin(object sender, RoutedEventArgs e)
        {
            var dialog = new LoginModal();
            dialog.Owner = clientMiniCheckers;
            dialog.ShowDialog();

            var secondDialog = new LoginErrorModal();
            secondDialog.Owner = clientMiniCheckers;
            secondDialog.ShowDialog();
        }
    }
}
