using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputeAverageAge
{
    class Program
    {
        const Boolean DEBUG = false;
        static void Main(string[] args)
        {
            float sum = 0;
            int amount = 5;
            ArrayList ages = new ArrayList();            
            
            Console.WriteLine("Hello world!");
            
            //Gets 5 ages from the user
            for (int i = 0; i < amount; i++)
            {
                Console.Write("Enter age: ");
                int age = int.Parse(Console.ReadLine());
                sum += age;
                ages.Add(age);
                if (DEBUG)
                {
                    Console.WriteLine("sum is: " + sum);
                    Console.WriteLine("current age is: " + ages[i]);
                }
            }

            //sorts the ages to find the mean
            ages.Sort();
            float mean = sum / amount;

            Console.WriteLine("The sum is: " + sum);
            Console.WriteLine("mean is " + mean);
            Console.WriteLine("median is " + ages[amount/2]);
            Console.ReadKey();
        }
    }
}
