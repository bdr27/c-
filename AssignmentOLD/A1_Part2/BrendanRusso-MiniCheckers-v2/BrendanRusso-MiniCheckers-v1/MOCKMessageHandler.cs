using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BrendanRusso_MiniCheckers_v1
{
    public class MOCKMessageHandler : MessageHandler
    {
        private string response;

        public void sendRequest(string request)
        {
            
        }

        public void connectTo(string ipAddress, int portNumber)
        {
            response = "ERROR";

            string pattern = @"^2[0-4][0-9]|25[0-5]$";
            if (Regex.Match(ipAddress, pattern).Success)
            {
                response = "VALID";
            }
        }

        public string getRequest()
        {
            return response;
        }
    }
}
