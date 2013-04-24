using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace BrendanRusso_V3_PartOne
{
    /// <summary>
    /// Interaction logic for NetworkInformationWindow.xaml
    /// </summary>
    public partial class NetworkInformationWindow : Window
    {
        public NetworkInformationWindow()
        {
            InitializeComponent();
            btnOkayIP.Click += btnOkayIP_Click;
            slidePort.ValueChanged += slidePort_ValueChanged;
        }

        private void slidePort_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblPort.Text = "port: " + (int)slidePort.Value;
        }

        private void btnOkayIP_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private bool checkValidIPAddress(string iPAddress)
        {
            string[] secIPAddress = iPAddress.Split('.');

            bool validAddress = false;
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

            if(Regex.Match(iPAddress, pattern).Success)
            {
                validAddress = true;
            }
            return validAddress;
        }

        public string GetIPAddress()
        {
            return AddressField.Text;
        }

        public int getPortNumber()
        {
            return (int) slidePort.Value;
        }
    }
}
