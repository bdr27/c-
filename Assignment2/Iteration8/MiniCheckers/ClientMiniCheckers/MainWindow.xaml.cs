using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ClientMiniCheckers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> players;

        public MainWindow()
        {
            InitializeComponent();
            players = new ObservableCollection<string>();
            lbAvaliablePlayers.ItemsSource = players;
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

        public void AddPlayers(List<string> newPlayers)
        {
            Dispatcher.Invoke(() =>
                {
                    players.Clear();
                    foreach (var player in newPlayers)
                    {
                        players.Add(player);
                    }
                });
        }

        public void AddPlayersListViewHandler(System.Windows.Input.MouseButtonEventHandler handler)
        {
            lbAvaliablePlayers.MouseUp += handler;
        }

        public string GetSelectedPlayer()
        {
            return lbAvaliablePlayers.SelectedItem as string;
        }

        public void DisablePlayers()
        {
            Dispatcher.Invoke(() =>
         {
             lbAvaliablePlayers.IsEnabled = false;
         });
        }

        public void UpdateGameBoard(List<int> player1, List<int> player2)
        {
            Dispatcher.Invoke(() =>
            {
                board.updateView(player1, player2);
            });

        }

        public void UpdateColorTitle(string title)
        {
            Dispatcher.Invoke(() =>
                {
                    Title = "Brendan Russo - Client MiniCheckers v0.8" + title;
                });
        }

        public void EnablePlayers()
        {
            Dispatcher.Invoke(() =>
                {
                    lbAvaliablePlayers.IsEnabled = true;
                });
        }
    }
}
