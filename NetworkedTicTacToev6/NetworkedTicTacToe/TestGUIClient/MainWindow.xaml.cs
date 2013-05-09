using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace TestGUIClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UdpClient udpClient;
        Thread backgroundthread;
        private bool running;
        public MainWindow()
        {
            InitializeComponent();
            udpClient = new UdpClient("127.0.0.1", 50000);
            backgroundthread = new Thread(HandleBroadcasts);
            backgroundthread.IsBackground = true;
            backgroundthread.Start();
        }

        private void HandleBroadcasts()
        {
            var localIP = new IPEndPoint(IPAddress.Any, 50001);
            var udpClient = new UdpClient();
            udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpClient.ExclusiveAddressUse = false;
            udpClient.Client.Bind(localIP);

            var multicastingIP = IPAddress.Parse("228.5.6.7");
            udpClient.JoinMulticastGroup(multicastingIP);

            running = true;
            while (running)
            {
                var bytes = udpClient.Receive(ref localIP);
                var message = Encoding.UTF8.GetString(bytes);

                Dispatcher.Invoke(() =>
                {
                    lblResponse.Content = message;
                });
            }

            udpClient.DropMulticastGroup(multicastingIP);
            udpClient.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var request = tbRequest.Text;

            //send the request as bytes
            var bytes = Encoding.UTF8.GetBytes(request);
            udpClient.Send(bytes, bytes.Length);

            //get response and display it
            var endPoint = new IPEndPoint(IPAddress.Any, 50000);
            bytes = udpClient.Receive(ref endPoint);
            var response = Encoding.UTF8.GetString(bytes);
            lblResponse.Content = response;
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            udpClient.Close();
            running = false;
        }
    }
}
