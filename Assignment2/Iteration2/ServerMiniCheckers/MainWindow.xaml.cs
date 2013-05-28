using System;
using System.Collections.Generic;
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

        public void AddMenuNetworkSetupItemHandler(RoutedEventHandler handler)
        {
            menuNetworkSetup.Click += handler;
        }

        public void AddMenuMulticastSetup(RoutedEventHandler handler)
        {
            menuMulticastSetup.Click += handler;
        }

        public void AddMenuStartServer(RoutedEventHandler handler)
        {
            menuStartServer.Click += handler;
        }

        public void AddMenuStopServer(RoutedEventHandler handler)
        {
            menuStopServer.Click += handler;
        }
    }
}
