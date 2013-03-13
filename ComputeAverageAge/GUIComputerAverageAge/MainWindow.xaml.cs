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
        }

        private void btnCompute_Click(object sender, RoutedEventArgs e)
        {
            //Gets the average age from a string of ages
            double averageAge = getAverageAge(AgeValues.Text);
            //Prints result to text
            Result.Text = "Result: " + averageAge;
        }

        /*
         * Works out the average age based on a string of ages.
         */
        private double getAverageAge(String listOfAges){
            double totalAge = 0;
            double sum = 0;
            String[] arrayOfAges = listOfAges.Split(' ');

            //Goes through eage age in the array of ages and checks if it's int parsable
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
