using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.BusinessLayer.interfaces;
using CarConnect.BusinessLayer.services;
using CarConnect.Util;

namespace CarConnect.MainModule
{
    internal class MainMod
    {
        // main entry point for our application
        static void Main(string[] args)
        {
            // Initialize the db by creating necessary tables
            DatabaseInitializer.Initialize();

            // creating instance of CarConnectImplementation to handle multiple operations
            ICarConnectImplementation cc = new CarConnectImplementation();
            bool exit = false;
            Console.WriteLine("\n===== Car Connect =====");
            while (!exit)
            {
                // Display the menu for user inputs
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

                // handling menu choices
                switch (choice)
                {
                    case 1:
                        cc.CustomerLogin();         // Log in as a customer
                        break;
                    case 2:
                        cc.AdminLogin();            // Log in as a admin
                        break;
                    case 3:
                        cc.RegisterCustomer();      // register customer
                        break;
                    case 4:
                        exit = true;                // Exit the application
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
            // final exit message
            Console.WriteLine("Bye :)");
        }
    }
}
