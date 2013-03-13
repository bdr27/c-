using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToeClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow player1Window;
        private MainWindow player2Window;

        public App()
            : base()
        {
            player1Window = new MainWindow();
            player2Window = new MainWindow();

            player1Window.Title = "Player 1 Window";
            player2Window.Title = "Player 2 Window";

            player2Window.Show();
            

            NetworkInformationWindow dialog = new NetworkInformationWindow();
            dialog.Owner = player2Window;
            dialog.ShowDialog();

            player1Window.Show();
        }
    }
}
