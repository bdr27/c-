using System.Diagnostics;
using System.Threading;
using System.Windows;
using ClientMiniCheckers.Modal;
using ClientMiniCheckers.Utility;
using ClientMiniCheckers.Network;
using ServerMiniCheckers.Network;
using ServerMiniCheckers.Modal;

namespace ClientMiniCheckers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow clientMiniCheckers;
        private NetworkDetails sendAddress;
        private NetworkDetails multicastAddress;
        private MessageHandler udpHandler;
        private Thread requestResponseThread;
        private Thread multicastRequestThread;
        private MessageSent messageSent;
        private string username = "";

        public App()
            : base()
        {
            messageSent = MessageSent.NONE;
            clientMiniCheckers = new MainWindow();
            udpHandler = new ClientUDPMessageHandler();
            clientMiniCheckers.Show();
            WireHandlers();
            setupRequestResponseBackgroundThead();
        }

        private void setupRequestResponseBackgroundThead()
        {
            requestResponseThread = new Thread(new ThreadStart(RequestResponseBackground_Thread));
            requestResponseThread.IsBackground = true;
            multicastRequestThread = new Thread(new ThreadStart(MulticastRequestBackgroud_Thread));
            multicastRequestThread.IsBackground = true;
        }

        private void RequestResponseBackground_Thread()
        {
            while (true)
            {
                var response = udpHandler.GetResponse();
                var action = "";
                if (response.Equals("OKAY") || response.Equals("DONE"))
                {
                    switch (messageSent)
                    {
                        case MessageSent.LOGIN:
                            clientMiniCheckers.UpdateStatus("Logged on as " + username);
                            clientMiniCheckers.MenuLoginSuccessful();
                            action = "Login";
                            break;
                        case MessageSent.LOGOUT:
                            clientMiniCheckers.UpdateStatus("Logged out");
                            clientMiniCheckers.MenuLogoutSuccessful();
                            username = "";
                            action = "Logout";
                            break;
                        case MessageSent.NONE:
                            action = "wrong message";
                            break;
                        default:
                            action = "Wrong message";
                            break;
                    }
                }
                else if (response.Equals("ERROR")) 
                {
                    switch (messageSent)
                    {
                        case MessageSent.LOGIN:
                            ShowLoginErrorDialog();                            
                            action = "Login";
                            break;
                        case MessageSent.LOGOUT:
                            action = "Logout";
                            break;
                        case MessageSent.NONE:
                            action = "wrong message";
                            break;
                        default:
                            action = "Wrong message";
                            break;
                    }
                }
                Debug.WriteLine(action + ": " + response);
                messageSent = MessageSent.NONE;
            }
        }

        private void ShowLoginErrorDialog()
        {
            username = "";
            Dispatcher.Invoke(() =>
                {
                    var dialog = new LoginErrorModal();
                    dialog.Owner = clientMiniCheckers;
                    dialog.ShowDialog();
                });
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
            udpHandler.ConnectTo(sendAddress.GetIPAddress(), sendAddress.GetPortNumber());
            SetUserLogin();

            Debug.WriteLine("Sending to: " + sendAddress);
        }

        private void HandleMenuSetMulticast(object sender, RoutedEventArgs e)
        {
            var dialog = new IPAddressModal();
            dialog.Owner = clientMiniCheckers;
            dialog.SetMultiCast();
            dialog.ShowDialog();
            multicastAddress = dialog.GetNetworkDetails();
            SetUserLogin();

            Debug.WriteLine("Set multicast to: " + multicastAddress);
        }

        private void HandleMenuLogout(object sender, RoutedEventArgs e)
        {
            messageSent = MessageSent.LOGOUT;
            udpHandler.SendRequest(string.Format("LOGOUT,{0}", username));
        }

        private void HandleMenuLogin(object sender, RoutedEventArgs e)
        {
            username = "";
            var dialog = new LoginModal();
            dialog.Owner = clientMiniCheckers;
            dialog.ShowDialog();
            messageSent = MessageSent.LOGIN;
            username = dialog.GetUsername();
            udpHandler.SendRequest(string.Format("LOGIN,{0},{1}", username, dialog.GetPassword()));
        }

        private void SetUserLogin()
        {
            if (sendAddress == null || multicastAddress == null)
            {

                clientMiniCheckers.DisableLogin();
            }
            else
            {
                udpHandler.ConnectTo(sendAddress.GetIPAddress(), sendAddress.GetPortNumber());
                requestResponseThread.Start();
                clientMiniCheckers.EnableLogin();
            }
        }
    }
}
