using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
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

namespace TestGUIServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ObservableCollection<string> messages;
        private Thread backgroundThread;
        private Thread anotherBackthread;
        private string name = "";
        private bool running;

        public MainWindow()
        {
            InitializeComponent();
            messages = new ObservableCollection<string>();

            MessageView.ItemsSource = messages; // a XAML data binding

            //BindingOperations.EnableCollectionSynchronization(messages, new Object());

            backgroundThread = new Thread(HandleRequests);
            backgroundThread.IsBackground = true;
            backgroundThread.Start();

            anotherBackthread = new Thread(BoardbastMessages);
            anotherBackthread.IsBackground = true;
            anotherBackthread.Start();
        }

        private void BoardbastMessages()
        {
            var multicastIP = IPAddress.Parse("228.5.6.7");
            var udpClient = new UdpClient();
            udpClient.JoinMulticastGroup(multicastIP);
            var endPoint = new IPEndPoint(multicastIP, 50001);
            running = true;

            while (running)
            {
                Thread.Sleep(2000);

                //broadbast the current time to all client
                var nowText = DateTime.Now.ToShortTimeString();
                var bytes = Encoding.UTF8.GetBytes(nowText);
                udpClient.Send(bytes, bytes.Length, endPoint);
            }

            udpClient.DropMulticastGroup(multicastIP);
            udpClient.Close();
        }

        private void HandleRequests(object obj)
        {
            //setup the listener object
            var udpServer = new UdpClient(50000);

            //request handler loop
            var endPoint = new IPEndPoint(IPAddress.Any, 50000);
            while (true)
            {
                var bytes = udpServer.Receive(ref endPoint);
                var request = Encoding.UTF8.GetString(bytes);

                Dispatcher.Invoke(() =>
                {
                    messages.Add("request: " + request +
                        " from: " + endPoint.Address);
                });
                
                string response = "ERROR";

                if (Regex.Match(request, @"^SET_NAME,[a-zA-Z]+$").Success)
                {
                    response = "OKAY";
                    name = request.Split(',')[1];
                }
                else if (Regex.Match(request, @"^GET_NAME$").Success)
                {
                    if (name.Equals(""))
                    {
                        response = "NONE";
                    }
                    else
                    {
                        response = name;
                    }
                }
                    

                bytes = Encoding.UTF8.GetBytes(response);
                udpServer.Send(bytes, bytes.Length, endPoint);
            }
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            running = false;
            backgroundThread.Interrupt();
            anotherBackthread.Interrupt();
        }
    }
}
