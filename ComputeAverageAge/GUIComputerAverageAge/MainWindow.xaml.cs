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
using System.Collections;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUIComputerAverageAge
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           // Result.Text = "Hello World";
        }

        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            double averageAge = getAverageAge(AgeValues.Text);
            Result.Text = "Result: " + averageAge;
        }
        private double getAverageAge(String listOfAges){
            double totalAge = 0;
            double sum = 0;
            String[] arrayOfAges = listOfAges.Split(' ');

            foreach (String age in arrayOfAges)
            {
                try
                {
                    totalAge += int.Parse(age);
                    ++sum;
                }
                catch (Exception e)
                {
                    Console.WriteLine("invalid charater");
                }
            }

            return totalAge / sum;
        }
    }
}
