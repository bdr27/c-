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

namespace TicTacToeClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GameBoard.ShowPiece(0, true);
            GameBoard.SetPieceColor(0, Brushes.Red);
            GameBoard.ShowPiece(5, true);
            GameBoard.SetPieceColor(5, Brushes.Blue);
        }

        public void AddIPAddressMenuItemHandler(RoutedEventHandler handler)
        {
            AddIPAddressMenuItem.Click += handler;
            //Console.Write("I am here");
        }

        public void AddIPAddressMenuItemHandler(Action HandleIPAddressMenuItem)
        {
            throw new NotImplementedException();
        }

        public void AddSetNameMenuItemHandler(RoutedEventHandler handler)
        {
            setName.Click += handler;
        }

        public void enableIpSet()
        {
            AddIPAddressMenuItem.IsEnabled = true;
        }

        public void disableIpAddress()
        {
            AddIPAddressMenuItem.IsEnabled = false;
        }
        public void enableName()
        {
            setName.IsEnabled = true;
        }
        public void disableName()
        {
            setName.IsEnabled = false;
        }
    }
}
