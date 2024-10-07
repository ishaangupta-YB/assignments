using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringHandling
{
    internal class Program
    {
        static void SearchString()
        {
            Console.WriteLine("Enter the string:");
            string inputString = Console.ReadLine();
            Console.WriteLine("Enter the word to search:");
            string word = Console.ReadLine();
            if (inputString.Contains(word)) Console.WriteLine($"The word '{word}' is in the string.");
            else Console.WriteLine($"The word '{word}' is not in the string.");

        }
        static void CommaOperator()
        {
            Console.WriteLine("Enter the comma-separated string:");
            string inputString = Console.ReadLine();
            string[] values = inputString.Split(',');
            Console.WriteLine("The values are:");
            foreach (var value in values) Console.WriteLine(value.Trim());
        }
        public static void Main(string[] args)
        {
            SearchString();
            CommaOperator();
        }
    }
}
