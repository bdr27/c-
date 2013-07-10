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
        public static bool CheckLogin(string login)
        {
            if (Regex.Match(login, @"^LOGIN,[a-zA-Z0-9]+,[a-zA-Z0-9]+$").Success)
            {
                return true;
            }
            return false;
        }

        public static bool CheckValidIP(string ipAddress)
        {
            if (Regex.Match(ipAddress, @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$").Success)
            {
                return true;
            }
            return false;
        }

        public static bool CheckLogout(string logout)
        {
            if (Regex.Match(logout, @"^LOGOUT,[a-zA-Z0-9]+$").Success)
            {
                return true;
            }
            return false;
        }

        public static bool CheckValidUsers(string users)
        {
            if (Regex.Match(users, @"^USERS,[a-zA-Z0-9,]*$").Success)
            {
                return true;
            }
            return false;
        }

        public static bool CheckValidPlay(string response)
        {
            if (Regex.Match(response, @"^PLAY,[a-zA-Z0-9]+,[a-zA-Z0-9]+$").Success)
            {
                return true;
            }
            return false;
        }

        public static bool CheckValidGameStart(string gameStart)
        {
            if (Regex.Match(gameStart, @"GAMESTART,[a-zA-Z0-9]+,[a-zA-Z0-9]+$").Success)
            {
                return true;
            }
            return false;
        }

        public static bool CheckValidGameState(string gameState)
        {
            if (Regex.Match(gameState, @"GAMESTART,N[0-9][0-9]*|N[0-9][0-9]*$").Success)
            {
                return true;
            }
            return false;
        }
    }
}
