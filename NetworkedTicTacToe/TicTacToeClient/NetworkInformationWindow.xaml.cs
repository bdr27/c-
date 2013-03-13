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
using System.Windows.Shapes;

namespace TicTacToeClient
{
    /// <summary>
    /// Interaction logic for NetworkInformationWindow.xaml
    /// </summary>
    public partial class NetworkInformationWindow : Window
    {
        public NetworkInformationWindow()
        {
            InitializeComponent();
            btnOkay.IsEnabled = false;
            btnOkay.Click += btnOkay_Click;
        }

        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtBoxIPAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            String ipAddress = txtBoxIPAddress.Text;

            //Makes sure it can't be an empty string or only contain spaces
            if (ipAddress.Equals(""))
            {
                btnOkay.IsEnabled = false;
            }
            else
            {
                btnOkay.IsEnabled = true;
            }
        }
    }
}
