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

namespace BrendanRusso_MiniCheckers_v1
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

        public void AddIPAddressMenuItemHandler(RoutedEventHandler handler)
        {
            IPAddressMenuItem.Click += handler;
            Console.Write("Added IP address");
        }

        public void NameMenuItemHandler(RoutedEventHandler handler)
        {
            setName.Click += handler;
            Console.WriteLine("Added set name");
        }

        public void changeGameState(State state, int playerNumber)
        {
            string message = "Status: ";
            switch(state)
            {
                case State.WAIT_VALID_IP:
                    message += "Waiting for valid IP";
                    break;
                case State.WAIT_PLAYER1_NAME:
                    if (playerNumber == 1)
                    {
                        message += "Please enter your name";
                    }
                    else
                    {
                        message += "Waiting for player1 to enter name";
                    }
                    break;
                case State.WAIT_PLAYER2_NAME:
                    if (playerNumber == 2)
                    {
                        message += "Please enter your name";
                    }
                    else
                    {
                        message += "Waiting for player2 to enter name";
                    }
                    break;
                case State.PLAYER1_MOVING:
                    if (playerNumber == 1)
                    {
                        message += "Please make your move";
                    }
                    else
                    {
                        message += "Waiting for player1 to move";
                    }
                    break;
                case State.PLAYER2_MOVING:
                    if (playerNumber == 2)
                    {
                        message += "Please make your move";
                    }
                    else
                    {
                        message += "Waiting for player2 to move";
                    }
                    break;
            }
            GameStatus.Text = message;
        }
    }
}
