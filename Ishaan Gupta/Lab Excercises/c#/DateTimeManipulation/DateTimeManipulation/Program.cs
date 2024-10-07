using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateTimeManipulation
{
    internal class Program
    {
        static void CreateDate()
        {
            Console.WriteLine("Enter day:");
            int day = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter month:");
            int month = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter year:");
            int year = int.Parse(Console.ReadLine());
            try
            {
                DateTime newDate = new DateTime(year, month, day);
                Console.WriteLine($"Constructed Date: {newDate.ToString("dd-MM-yyyy")}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }

        }
        static void Difference()
        {
            Console.WriteLine("Enter the first date (dd-MM-yyyy):");
            string date1 = Console.ReadLine();

            Console.WriteLine("Enter the second date (dd-MM-yyyy):");
            string date2 = Console.ReadLine();
            try
            {
                DateTime fd = DateTime.ParseExact(date1, "dd-MM-yyyy", null);
                DateTime sd = DateTime.ParseExact(date2, "dd-MM-yyyy", null);

                TimeSpan diff = sd - fd;
                Console.WriteLine($"Difference between dates: {diff.TotalDays} days");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
        public static void Main(string[] args)
        {
            CreateDate();
            Difference();
        }
    }
}
