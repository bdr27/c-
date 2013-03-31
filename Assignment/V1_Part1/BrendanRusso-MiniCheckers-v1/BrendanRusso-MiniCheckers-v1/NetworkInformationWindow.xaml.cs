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
            checkValidIPAddress(AddressField.Text);
        }

        private void checkValidIPAddress(string IPAddress)
        {
        }
    }
}
