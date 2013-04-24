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

namespace BrendanRusso_V3_PartOne
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            setStatusWaitingForConnection();
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

        public void setStatusWaitingForConnection()
        {
            GameStatus.Text = "Status: Waiting for connection...";
        }

        public void setStatusEnterYourName()
        {
            GameStatus.Text = "Status: Please enter your name";
        }

        public void setStatusOpponmentName()
        {
            GameStatus.Text = "Status: Opponment entering name";
        }

        public void setStatusYourMove()
        {
            GameStatus.Text = "Status: Your move";
        }

        public void setStatusOpponentsMove()
        {
            GameStatus.Text = "Status: Opponent is moving";
        }

        public void setStatusInvalidName()
        {
            GameStatus.Text = "Status: Invalid name";
        }
    }
}
