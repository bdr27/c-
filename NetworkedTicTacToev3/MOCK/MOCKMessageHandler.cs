﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TicTacToeClient
{
    class MOCKMessageHandler : MessageHandler
    {
        private GameState state;
        private string response;

        public MOCKMessageHandler()
        {
            state = GameState.WAIT_PLAYER1_NAME;
        }

        public void connectTo(string address, int port)
        {
            Debug.WriteLine(state.ToString());
            //throw new NotImplementedException();
        }

        public void sendRequest(string request)
        {
            if (Regex.Match(request, @"^SET_NAME,[a-zA-Z]+$").Success)
            {
                response = "OKAY";
            }
            else if (Regex.Match(request, @"^PUT,[a-zA-Z]+,[0-9]+,[0-9]+$").Success)
            {
                response = "OKAY";
            }
            else
            {
                response = "FAILED";
            }
        }

        public string getResponse()
        {
            return response;
        }
    }
}
