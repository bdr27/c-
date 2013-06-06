using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ServerMiniCheckers.Utility;

namespace ServerMiniCheckers.Network
{
    public class UDPListener : BroadcastListener
    {
        DBHandler database;
        UdpClient udpServer;
        IPEndPoint endPoint;
        string response = "ERROR";
        #region BroadcastListener Members

        public void SetupListener(int port)
        {
            database = new MOCKDBHandler();
            udpServer = new UdpClient(50000);
            endPoint = new IPEndPoint(IPAddress.Any, port);
        }

        public void StartListener()
        {
            var bytes = udpServer.Receive(ref endPoint);
            var request = Encoding.UTF8.GetString(bytes);
            response = "ERROR";
            if (request.Contains("LOGIN") && 3 == request.Split(',').Count())
            {
                var username = request.Split(',')[1];
                var password = request.Split(',')[2];

                if (database.IsValidLogin(username, password))
                {
                    response = "OKAY";
                }

                Debug.WriteLine("Username: " + username + " Password: " + password);
            }

            Debug.WriteLine("The request: " + request);
        }

        public void SendResponse()
        {
            var bytes = Encoding.UTF8.GetBytes(response);
            udpServer.Send(bytes, bytes.Length, endPoint);
        }

        public string GetMessage()
        {
            return response;
        }

        public void Close()
        {
            udpServer.Close();
        }

        #endregion
    }
}
