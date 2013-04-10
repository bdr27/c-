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
    /// Interaction logic for SetNameWindow.xaml
    /// </summary>
    public partial class SetNameWindow : Window
    {
        public SetNameWindow()
        {
            InitializeComponent();
            OkayButton.Click += OkayButton_Click;
            NameTextBox.TextChanged += NameTextBox_TextChanged;
        }

        void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OkayButton.IsEnabled = NameTextBox.Text.Length > 0 ? true : false;
        }

        void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public string GetName()
        {
            return NameTextBox.Text;
        }
    }
}
