using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Util;
using StudentInformationSystem.BusinessLayer.services;
using StudentInformationSystem.BusinessLayer.interfaces;

namespace StudentInformationSystem.MainModule
{
    public class MainMod
    {
        // Main entry point for application
        static void Main(string[] args)
        {
            // Initializes the database by creating necessary tables
            DatabaseInitializer.Initialize();

            // creating instance of SISImplementation to handle multiple operations
            ISISImplementation sis = new SISImplementation();


            bool exit = false;  // Variable to control the app exit

            Console.WriteLine("\n===== Student Information System (SIS) =====");
            while (!exit)
            {
                // Display the menu for user inputs
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Enroll Student in Course");
                Console.WriteLine("2. Assign Teacher to Course");
                Console.WriteLine("3. Make Payment");
                Console.WriteLine("4. Generate Enrollment Report");
                Console.WriteLine("5. Generate Payment Report");
                Console.WriteLine("6. Calculate Course Statistics");
                Console.WriteLine("7. Exit");
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                int choice;

                // to check if input is a valid or not
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number from the menu.");
                    continue;
                }

                // handling menu choices
                switch (choice)
                {
                    case 1:
                        sis.EnrollStudentInCourse();    // function to enroll student in a course
                        break;
                    case 2:
                        sis.AssignTeacherToCourse();    // function to assign teacher to a course
                        break;
                    case 3:
                        sis.MakePayment();              // function to record payment for a student
                        break;
                    case 4:
                        sis.GenerateEnrollmentReport(); // function to generate report of students enrolled in a specific courses
                        break;
                    case 5:
                        sis.GeneratePaymentReport();    // function to generate report of payments made by a specific student
                        break;
                    case 6:
                        sis.CalculateCourseStatistics(); // function to calculate statistics for a specific course, such as the number of enrollments and total payments
                        break;
                    case 7:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
            Console.WriteLine("Bye :)"); // final exit message
        }
    }
}
