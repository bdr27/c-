using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyCollections;

namespace TESTConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var sortedSet = new SortedSet<int>();
            sortedSet.Add(10);
            Console.WriteLine(sortedSet);

            Console.WriteLine(sortedSet.Add(10).ToString());

            sortedSet.Add(5);
            sortedSet.Add(6);
            sortedSet.Add(20);
            sortedSet.Add(13);
            sortedSet.Add(14);

            Console.WriteLine(sortedSet);

            Console.WriteLine(sortedSet.Contains(3));
            Console.WriteLine(sortedSet.Contains(5));
            Console.WriteLine(sortedSet.Contains(10));
            Console.WriteLine(sortedSet.Contains(10));
            Console.WriteLine(sortedSet.Contains(5));
            Console.WriteLine(sortedSet.Contains(6));
            Console.WriteLine(sortedSet.Contains(20));
            Console.WriteLine(sortedSet.Contains(13));
            Console.WriteLine(sortedSet.Contains(14));

            Console.ReadKey();
        }
    }
}
