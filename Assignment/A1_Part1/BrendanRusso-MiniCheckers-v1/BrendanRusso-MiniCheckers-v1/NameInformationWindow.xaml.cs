using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BrendanRusso_MiniCheckers_v1
{
    /// <summary>
    /// Interaction logic for NameInformationWindow.xaml
    /// </summary>
    public partial class NameInformationWindow : Window
    {
        public NameInformationWindow()
        {
            InitializeComponent();
            btnOkayName.Click += btnOkayName_Click;
        }

        private void btnOkayName_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string GetName()
        {
            return NameField.Text;
        }

        private void NameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (checkValidName(NameField.Text))
            {
                btnOkayName.IsEnabled = true;
            }
            else
            {
                btnOkayName.IsEnabled = false;
            }
        }

        private bool checkValidName(string name)
        {
            string pattern = @"^[a-zA-Z]$";

            if (Regex.Match(name, pattern).Success)
            {
                return true;
            }
            return false;
        }
    }
}
