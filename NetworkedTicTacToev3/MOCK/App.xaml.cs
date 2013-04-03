using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TicTacToeClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MessageHandler handler;

        public App()
            : base()
        {
            handler = new MOCKMessageHandler();
            handler.connectTo("127.0.0.1", 50000);
            handler.sendRequest("SET_NAME,BrendanRusso");
            Debug.WriteLine(handler.getResponse());
            handler.sendRequest("PUT,BrendanRusso,3,1");
            Debug.WriteLine(handler.getResponse());
        }
    }
}
