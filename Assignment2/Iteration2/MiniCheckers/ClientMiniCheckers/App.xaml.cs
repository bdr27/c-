using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ClientMiniCheckers
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainWindow clientMiniCheckers;
        public App()
            : base()
        {
            clientMiniCheckers = new MainWindow();
            clientMiniCheckers.Show();
        }
    }
}
