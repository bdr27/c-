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
            lbAvaliablePlayers.Items.Add("John");
            lbAvaliablePlayers.Items.Add("Sarah");
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
    }
}
