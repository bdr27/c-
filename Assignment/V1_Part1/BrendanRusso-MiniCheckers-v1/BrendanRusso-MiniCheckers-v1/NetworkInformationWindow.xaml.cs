using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BrendanRusso_MiniCheckers_v1
{
    /// <summary>
    /// Interaction logic for NetworkInformationWindow.xaml
    /// </summary>
    public partial class NetworkInformationWindow : Window
    {
        public NetworkInformationWindow()
        {
            InitializeComponent();
        }

        public string GetIPAddress()
        {
            return AddressField.Text;
        }

        private void AddressField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkValidIPAddress(AddressField.Text))
            {
                btnOkayIP.IsEnabled = true;
            }
            else
            {
                btnOkayIP.IsEnabled = false;
            }
        }

        private bool checkValidIPAddress(string IPAddress)
        {
            string pattern;

            pattern = @"^\b(?:\d{1,3}\.){3}\d{1,3}\b$";
            // pattern = @"^[0-255]+.[0-255]+.[0-255]+.[0-255]+$";
            // pattern = @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)";
            if (Regex.Match(IPAddress, pattern).Success)
            {
                return true;
            }
            return false;
        }
    }
}
