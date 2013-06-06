using System.Windows;

namespace ClientMiniCheckers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void AddMenuSetNetworkHandler(RoutedEventHandler handler)
        {
            menuSetNetwork.Click += handler;
        }

        public void AddMenuSetMulticastHandler(RoutedEventHandler handler)
        {
            menuSetMulticast.Click += handler;
        }

        public void AddMenuLoginHandler(RoutedEventHandler handler)
        {
            menuLogin.Click += handler;
        }

        public void AddMenuLogoutHandler(RoutedEventHandler handler)
        {
            menuLogout.Click += handler;
        }

        public void DisableLogin()
        {
            menuLogin.IsEnabled = false;
        }

        public void EnableLogin()
        {
            menuLogin.IsEnabled = true;
        }

        public void UpdateStatus(string message)
        {
            Dispatcher.Invoke(() =>
                {
                    tbStatus.Text = "Status: " + message;
                });
        }

        public void MenuLoginSuccessful()
        {
            Dispatcher.Invoke(() =>
                {
                    menuLogin.IsEnabled = false;
                    menuLogout.IsEnabled = true;
                });
        }

        public void MenuLogoutSuccessful()
        {
            Dispatcher.Invoke(() =>
                {
                    menuLogin.IsEnabled = true;
                    menuLogout.IsEnabled = false;
                });
        }
    }
}
