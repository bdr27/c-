using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServerMiniCheckers.Utility;

namespace ServerMiniCheckers
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

        public void AddMenuNetworkSetupHandler(RoutedEventHandler handler)
        {
            menuNetworkSetup.Click += handler;
        }

        public void AddMenuMulticastSetupHandler(RoutedEventHandler handler)
        {
            menuMulticastSetup.Click += handler;
        }

        public void AddMenuStartServerHandler(RoutedEventHandler handler)
        {
            menuStartServer.Click += handler;
        }

        public void AddMenuStopServerHandler(RoutedEventHandler handler)
        {
            menuStopServer.Click += handler;
        }

        public void LoadLeaderboard(List<Score> highScores)
        {
            var message = "";
            foreach (var score in highScores)
            {
                message = message + score.ToString() + '\n';
                Debug.WriteLine(score.ToString());
            }
            serverStatus.UpdateTbLeaderboard(message);
        }

        public void UpdateMenuState(ServerState serverState)
        {
            switch (serverState)
            {
                case ServerState.RUNNING:
                    menuStartServer.IsEnabled = false;
                    menuStopServer.IsEnabled = true;
                    break;
                case ServerState.STOPPED:
                    menuStopServer.IsEnabled = false;
                    menuStartServer.IsEnabled = true;
                    break;
            }
        }
    }
}
