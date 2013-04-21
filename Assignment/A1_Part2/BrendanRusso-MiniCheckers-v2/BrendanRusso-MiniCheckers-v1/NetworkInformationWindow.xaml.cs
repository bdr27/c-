using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
            btnOkayIP.Click += btnOkayIP_Click;
        }

        private void btnOkayIP_Click(object sender, RoutedEventArgs e)
        {
            Close();
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

        private bool checkValidIPAddress(string iPAddress)
        {
            string[] secIPAddress = iPAddress.Split('.');
            
            bool validAddress = false;
            
            if (secIPAddress.Length == 4)
            {
                validAddress = true;
                for (int i = 0; i < 4; i++)
                {
                    int section;
                    //Checks if it can be an int
                    try
                    {
                        section = Int32.Parse(secIPAddress[i]);
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                        validAddress = false;
                        break;
                    }

                    //Checks for leading 0's
                    if (secIPAddress[i].Length > 1 && secIPAddress[0].Equals("0"))
                    {
                        validAddress = false;
                        break;
                    }
                    
                    //Check if it's in range
                    if (section > 255 || section < 0)
                    {
                        validAddress = false;
                        break;
                    }
                }
            }
            return validAddress;
           
            //string pattern = @"^2[0-4][0-9]|25[0-5]+$";
            //pattern = @"^\b(?:\d{1,3}\.){3}\d{1,3}\b$";
            // pattern = @"^[0-255]+.[0-255]+.[0-255]+.[0-255]+$";
            // pattern = @"(?<First>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Second>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Third>2[0-4]\d|25[0-5]|[01]?\d\d?)\.(?<Fourth>2[0-4]\d|25[0-5]|[01]?\d\d?)";
            //if (Regex.Match(iPAddress, pattern).Success)
            //{
            //    return true;
            //}
            //return false;
        }
    }
}
