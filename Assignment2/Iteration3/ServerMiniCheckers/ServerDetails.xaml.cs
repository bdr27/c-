﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerMiniCheckers
{
    /// <summary>
    /// Interaction logic for ServerDetails.xaml
    /// </summary>
    public partial class ServerDetails : UserControl
    {
        public ServerDetails()
        {
            InitializeComponent();
        }

        public void UpdateTbRequestResponse(string update)
        {
            svRequestResponse.Content = update + "\n" + tbRequestResponse.Text;
            
        }

        public void UpdateTbLeaderboard(string update)
        {
            svLeaderboard.Content = update;
        }
    }
}
