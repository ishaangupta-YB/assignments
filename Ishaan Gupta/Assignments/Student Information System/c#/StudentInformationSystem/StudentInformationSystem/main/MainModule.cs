using System;
using System.Data;
using StudentInformationSystem.dao.services;
using StudentInformationSystem.util;

namespace StudentInformationSystem.main
{
    public class MainModule
    {    
        static void Main(string[] args)
        {
            DatabaseInitializer.Initialize();
            ISISImplementation sis = new SISImplementation(); 
            bool exit = false; 
            Console.WriteLine("\n===== Student Information System (SIS) =====");
            while (!exit) {
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
                
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number from the menu.");
                    continue;
                }
                switch (choice) {
                    case 1:
                        sis.EnrollStudentInCourse();
                        break;
                    case 2:
                        sis.AssignTeacherToCourse();
                        break;
                    case 3:
                        sis.MakePayment();
                        break; 
                    case 4:
                        sis.GenerateEnrollmentReport();
                        break;
                    case 5:
                        sis.GeneratePaymentReport();
                        break;
                    case 6:
                        sis.CalculateCourseStatistics();
                        break;
                    case 7:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
            Console.WriteLine("Bye :)");
        } 
    }
}
