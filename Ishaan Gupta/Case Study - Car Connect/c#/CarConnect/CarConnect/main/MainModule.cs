using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.dao.interfaces;
using CarConnect.dao.services;
using CarConnect.util;

namespace CarConnect.main
{
    internal class MainModule
    {
        static void Main(string[] args)
        {
            DatabaseInitializer.Initialize();
            ICarConnectImplementation cc = new CarConnectImplementation();
            bool exit = false;
            Console.WriteLine("\n===== Car Connect =====");
            while (!exit)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Customer Login");
                Console.WriteLine("2. Admin Login");
                Console.WriteLine("3. Register as Customer"); 
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                int choice;

                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number from the menu.");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        cc.CustomerLogin();
                        break;
                    case 2:
                        cc.AdminLogin();
                        break;
                    case 3:
                        cc.RegisterCustomer();
                        break; 
                    case 4:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
            Console.WriteLine("Bye :)");
        }
    }
}
