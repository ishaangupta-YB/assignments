using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Looping
{
    internal class Program
    {
        static void ReverseOrder()
        {
            Console.WriteLine("Numbers in reverse order from 50 to 1:");
            for (int i = 50; i > 0; i--) Console.Write(i + " ");
            Console.WriteLine();
        }
        static void OddNumbersDoWhile()
        {
            int i = 1;
            do
            {
                if (i % 2 != 0) Console.Write(i + " ");
                i++;
            } while (i < 51);
            Console.WriteLine();

        }
        static void EvenNumbersDoWhile()
        {
            int i = 1;
            do
            {
                if (i % 2 == 0) Console.Write(i + " ");
                i++;
            } while (i < 51);
            Console.WriteLine();

        }
        static void PrintTable(int n)
        {
            Console.WriteLine($"table for {n}:");
            for (int i = 1; i <= 10; i++) Console.WriteLine($"{n} x {i} = {n * i}");
            Console.WriteLine();
        }
        static void EvenNumbersInArray(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] % 2 == 0) Console.Write(a[i] + " ");
            }
            Console.WriteLine();
        }
        public static void Main(string[] args)
        {
            ReverseOrder();
            OddNumbersDoWhile();
            EvenNumbersDoWhile();
            PrintTable(3);
            int[] a = { 1, 3, 4, 5, 6, 8, 9, 12, 22 };
            EvenNumbersInArray(a);
        }
    }
}
