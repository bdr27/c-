using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Network
{
    public class NetworkDetails
    {
        string ipAddress;
        int portNumber;

        public NetworkDetails(string ipAddress, int portNumber)
        {
            this.ipAddress = ipAddress;
            this.portNumber = portNumber;
        }

        public void SetIPAddress(string ipAddress)
        {
            this.ipAddress = ipAddress;
        }

        public void SetPortNumber(int portNumber)
        {
            this.portNumber = portNumber;
        }

        public int GetPortNumber()
        {
            return portNumber;
        }

        public string GetIPAddress()
        {
            return ipAddress;
        }

        public override string ToString()
        {
            return "IP Address: " + ipAddress + " Port : " + portNumber;
        }
    }
}
