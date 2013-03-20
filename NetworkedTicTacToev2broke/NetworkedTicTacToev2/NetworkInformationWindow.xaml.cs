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

namespace NetworkedTicTacToev2
{
    /// <summary>
    /// Interaction logic for NetworkInformationWindow.xaml
    /// </summary>
    public partial class NetworkInformationWindow : Window
    {
        public NetworkInformationWindow()
        {
            InitializeComponent();
            btnOkay.Click += btnOkay_Click;
            enableButton(btnOkay, txtBoxIPAddress.Text);
        }

        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void txtBoxIPAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            String ipAddress = txtBoxIPAddress.Text;
            enableButton(btnOkay, ipAddress);
        }

        private void enableButton(Button button, String textBlock)
        {
            if (textBlock.Equals(""))
            {
                button.IsEnabled = false;
            }
            else
            {
                button.IsEnabled = true;
            }
        }
    }
}