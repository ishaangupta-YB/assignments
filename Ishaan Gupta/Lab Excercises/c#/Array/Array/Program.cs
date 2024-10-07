using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Array
{
    class Product
    {
        private string ProductCode { get; set; }
        private string ProductName { get; set; }
        private string Category { get; set; }
        public void ListProducts()
        {
            List<Product> products = new List<Product>
        {
            new Product { ProductCode = "P001", ProductName = "Laptop", Category = "Electronics" },
            new Product { ProductCode = "P002", ProductName = "PC", Category = "Electronics" },
            new Product { ProductCode = "P003", ProductName = "Book", Category = "Education" },
            new Product { ProductCode = "P004", ProductName = "Chair", Category = "Furniture" }
        };
            Console.WriteLine("List of Products:");
            foreach (var product in products) Console.WriteLine($"Code: {product.ProductCode}, Name: {product.ProductName}, Category: {product.Category}");
        }
    }
    internal class Program
    {
        static void SumSingleDimArray(int[] a)
        {
            int sum = 0;
            foreach (int x in a) sum += x;
            Console.WriteLine("sum is {0}", sum);
        }
        static void SumDiagonalElements(int[,] a)
        {
            int sum = 0;
            for (int i = 0; i < a.GetLength(0); i++) sum += a[i, i];
            Console.WriteLine("sum is {0}", sum);
        }
        static void SearchElementInJaggedArray(int[][] a, int x)
        {
            bool found = false;
            foreach (var row in a)
            {
                foreach (var e in row)
                {
                    if (x == e)
                    {
                        found = true;
                        break;
                    }
                }
            }
            if (found) Console.WriteLine($"{x} is in the jagged array.");
            else Console.WriteLine($"{x} is not in the jagged array.");

        }
        static void SumEachRow(int[,] a)
        {
            Console.WriteLine("Sum of each row:");
            for (int i = 0; i < a.GetLength(0); i++)
            {
                int rowSum = 0;
                for (int j = 0; j < a.GetLength(1); j++) rowSum += a[i, j];
                Console.WriteLine($"Row {i + 1}: {rowSum}");
            }
        }

        static void CountryTelephonePrefixes()
        {
            Hashtable ht = new Hashtable
        {
            { "India", "+91" },
            { "USA", "+1" },
            { "UK", "+44" },
            { "Nepal", "+977" }
        };
            Console.WriteLine("Country and their mobile telephone prefixes:");
            foreach (DictionaryEntry e in ht) Console.WriteLine($"Country: {e.Key}, Prefix: {e.Value}");
        }
        public static void Main(string[] args)
        {
            Console.WriteLine("\nTask 14: Sum of single-dimensional int array");
            int[] a = { 1, 4, 5, 8 };
            SumSingleDimArray(a);

            Console.WriteLine("\nTask 15: Sum of diagonal elements");
            int[,] b = {
            { 10, 40, 50 },
            { 60, 20, 70 },
            { 70, 90, 30 }
        };
            SumDiagonalElements(b);

            Console.WriteLine("\nTask 16: Search element in jagged array");
            int[][] ja = {
            new int[] { 1, 2, 3 },
            new int[] { 4, 5, 6, 7 },
            new int[] { 8, 9 }
        };
            Console.WriteLine("\nEnter an element to search in the jagged array:");
            int e = int.Parse(Console.ReadLine());
            SearchElementInJaggedArray(ja, e);

            Console.WriteLine("\nTask 17: Sum of each row for a multi-dimensional array");
            int[,] c = {
            { 10, 40, 50 },
            { 60, 20, 70 },
            { 80, 90, 30 },
            { 100, 150, 200 }
        };
            SumEachRow(c);

            Console.WriteLine("\nTask 18: List<Product> using Generic List");
            Product p = new Product();
            p.ListProducts();

            Console.WriteLine("\nTask 19: Country and mobile prefixes using Hashtable");
            CountryTelephonePrefixes();
        }
    }
}
