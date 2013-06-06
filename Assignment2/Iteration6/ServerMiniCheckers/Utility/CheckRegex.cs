using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerMiniCheckers.Utility
{
    public class CheckRegex
    {
        public static bool checkLogin(string login)
        {
            if (Regex.Match(login, @"^LOGIN,[a-zA-Z0-9]+,[a-zA-Z0-9]+$").Success)
            {
                return true;
            }
            return false;
        }

        public static bool checkValidIP(string ipAddress)
        {
            if (Regex.Match(ipAddress, @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$").Success)
            {
                return true;
            }
            return false;
        }

        public static bool checkLogout(string logout)
        {
            if (Regex.Match(logout, @"^LOGOUT,[a-zA-Z0-9]+$").Success)
            {
                return true;
            }
            return false;
        }
    }
}
