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

            OkayButton.Click += OkayButton_Click;
            AddressField.TextChanged += AddressField_TextChanged;
        }

        void AddressField_TextChanged(object sender, TextChangedEventArgs e)
        {
            OkayButton.IsEnabled = AddressField.Text.Length > 0;
        }

        void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string GetIPAddress()
        {
            return AddressField.Text;
        }
    }
}
