using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using ServerMiniCheckers.Network;
using ServerMiniCheckers.Utility;

namespace ServerMiniCheckers.Modal
{
    /// <summary>
    /// Interaction logic for IPAddress.xaml
    /// </summary>
    public partial class IPAddressModal : Window
    {
        private IPAddressModalState state;

        public IPAddressModal()
        {
            InitializeComponent();
            state = IPAddressModalState.IPADDRESS;
        }

        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void SetStaticIP()
        {
            tbIPAddress.Text = getLocalIP();
            tbIPAddress.IsEnabled = false;
        }

        public string getLocalIP()
        {
            var IPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            var localIP = "";

            foreach (var ip in IPAddresses)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }

        public void SetMultiCast()
        {
            state = IPAddressModalState.MULTICAST;
            tbIPAddress.IsEnabled = true;
            sldPortNumber.Value = 50001;
        }

        public string GetIpAddress()
        {
            return tbIPAddress.Text;
        }

        public int GetPortNumber()
        {
            return (int) sldPortNumber.Value;
        }

        public NetworkDetails GetNetworkDetails()
        {
            return new NetworkDetails(GetIpAddress(), GetPortNumber());
        }

        private void tbIPAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkValidIPAddress(tbIPAddress.Text))
            {
                btnOkay.IsEnabled = true;
            }
        }

        private bool checkValidIPAddress(string iPAddress)
        {
            return CheckRegex.checkValidIP(iPAddress);
        }

        private enum IPAddressModalState
        {
            IPADDRESS, MULTICAST
        }

        private void btnLazyAss_Click(object sender, RoutedEventArgs e)
        {
            switch (state)
            {
                case IPAddressModalState.IPADDRESS:
                    tbIPAddress.Text = getLocalIP();
                    break;
                case IPAddressModalState.MULTICAST:
                    tbIPAddress.Text = "228.5.6.7";
                    break;
            }
        }
    }
}
