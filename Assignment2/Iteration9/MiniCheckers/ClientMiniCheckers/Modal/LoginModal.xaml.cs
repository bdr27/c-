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
using System.Windows.Shapes;

namespace ClientMiniCheckers.Modal
{
    /// <summary>
    /// Interaction logic for LoginModal.xaml
    /// </summary>
    public partial class LoginModal : Window
    {
        public LoginModal()
        {
            InitializeComponent();
        }

        private void btnOkay_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string GetPassword()
        {
            return pbPassword.Password;
        }

        public string GetUsername()
        {
            return tbUsername.Text;
        }
    }
}