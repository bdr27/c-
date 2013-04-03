using System;
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

namespace TicTacToeClient
{
    /// <summary>
    /// Interaction logic for NameInformationWindow.xaml
    /// </summary>
    public partial class NameInformationWindow : Window
    {
        public NameInformationWindow()
        {
            InitializeComponent();
            OkayButton.Click += OkayButton_Click;
            NameField.TextChanged += NameField_TextChanged;
        }

        private void NameField_TextChanged(object sender, TextChangedEventArgs e)
        {
            OkayButton.IsEnabled = NameField.Text.Length > 0;
        }

        private void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string getName()
        {

            return NameField.Text;
        }
    }
}
