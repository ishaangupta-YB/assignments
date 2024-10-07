using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.dao.repositories;
using StudentInformationSystem.dao.services;
using StudentInformationSystem.entity;
using StudentInformationSystem.util;

namespace StudentInformationSystem.main
{
    public class MainModule
    {
         
        static void Main(string[] args)
        {
            IStudentRepository studentRepository = new StudentRepository();
            ICourseRepository courseRepository = new CourseRepository();
            ITeacherRepository teacherRepository = new TeacherRepository();
            IEnrollmentRepository enrollmentRepository = new EnrollmentRepository();
            IPaymentRepository paymentRepository = new PaymentRepository();

            IStudentService studentService = new StudentService(studentRepository, enrollmentRepository, courseRepository, paymentRepository);
            ICourseService courseService = new CourseService(courseRepository, enrollmentRepository, teacherRepository);
            ITeacherService teacherService = new TeacherService(teacherRepository, courseRepository);
            IEnrollmentService enrollmentService = new EnrollmentService(enrollmentRepository, studentRepository, courseRepository);
            IPaymentService paymentService = new PaymentService(paymentRepository, studentRepository);

            InitData(studentService, teacherService, courseService);
            bool exit = false;
            while (!exit) {
                Console.WriteLine("\n===== Student Information System (SIS) =====");
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
                        EnrollStudentInCourse(studentService);
                        break;
                    case 2:
                        AssignTeacherToCourse(courseService, teacherService);
                        break;
                    case 3:
                        MakePayment(studentService);
                        break; 
                    case 4:
                        GenerateEnrollmentReport(courseService, enrollmentService, studentService);
                        break;
                    case 5:
                        GeneratePaymentReport(studentService, paymentService);
                        break;
                    case 6:
                        CalculateCourseStatistics(courseService, enrollmentService, studentService, paymentService);
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
        static void InitData(IStudentService studentService, ITeacherService teacherService, ICourseService courseService)
        {
            studentService.AddStudent(new Student
            {
                StudentID = IDGenerator.GetNextStudentID(),
                FirstName = "Ishaan",
                LastName = "Gupta",
                DateOfBirth = new DateTime(2000, 1, 1),
                Email = "ishaangupta@gmail.com",
                PhoneNumber = "9876543210"
            });
            studentService.AddStudent(new Student
            {
                StudentID = IDGenerator.GetNextStudentID(),
                FirstName = "Akash",
                LastName = "Singh",
                DateOfBirth = new DateTime(1999, 5, 15),
                Email = "akashsingh@gmail.com",
                PhoneNumber = "9876543211"
            });

            studentService.AddStudent(new Student
            {
                StudentID = IDGenerator.GetNextStudentID(),
                FirstName = "Vinay",
                LastName = "Solanki",
                DateOfBirth = new DateTime(2001, 3, 10),
                Email = "vinaysolanki@gmail.com",
                PhoneNumber = "9876543212"
            });

            studentService.AddStudent(new Student
            {
                StudentID = IDGenerator.GetNextStudentID(),
                FirstName = "Mrunali",
                LastName = "Rajkule",
                DateOfBirth = new DateTime(2000, 12, 25),
                Email = "mrunalirajkule@gmail.com",
                PhoneNumber = "9876543213"
            });

            studentService.AddStudent(new Student
            {
                StudentID = IDGenerator.GetNextStudentID(),
                FirstName = "Rohit",
                LastName = "Sharma",
                DateOfBirth = new DateTime(1990, 7, 20),
                Email = "rohitsharma@gmail.com",
                PhoneNumber = "9876543214"
            });

            teacherService.AddTeacher(new Teacher
            {
                TeacherID = IDGenerator.GetNextTeacherID(),
                FirstName = "Varsha",
                LastName = "Patil",
                Email = "varshapatil@gmail.com"
            });

            teacherService.AddTeacher(new Teacher
            {
                TeacherID = IDGenerator.GetNextTeacherID(),
                FirstName = "Suresh",
                LastName = "Kumar",
                Email = "sureshkumar@gmail.com"
            });

            teacherService.AddTeacher(new Teacher
            {
                TeacherID = IDGenerator.GetNextTeacherID(),
                FirstName = "Shubhra",
                LastName = "Rana",
                Email = "SubhraR@gmail.com"
            });

            teacherService.AddTeacher(new Teacher
            {
                TeacherID = IDGenerator.GetNextTeacherID(),
                FirstName = "Raj",
                LastName = "Malhotra",
                Email = "rajmalhotra@gmail.com"
            });

            teacherService.AddTeacher(new Teacher
            {
                TeacherID = IDGenerator.GetNextTeacherID(),
                FirstName = "Kavita",
                LastName = "Sharma",
                Email = "kavitasharma@gmail.com"
            });

            courseService.AddCourse(new Course
            {
                CourseID = IDGenerator.GetNextCourseID(),
                CourseName = "Mathematics",
                CourseCode = "MATH101"
            });

            courseService.AddCourse(new Course
            {
                CourseID = IDGenerator.GetNextCourseID(),
                CourseName = "Physics",
                CourseCode = "PHY101"
            });

            courseService.AddCourse(new Course
            {
                CourseID = IDGenerator.GetNextCourseID(),
                CourseName = "CS",
                CourseCode = "CS101"
            });
        }
        static void EnrollStudentInCourse(IStudentService studentService)
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int studentId = int.Parse(Console.ReadLine());

                Console.Write("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                studentService.EnrollInCourse(studentId, courseId);
                Console.WriteLine("Student enrolled successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        static void AssignTeacherToCourse(ICourseService courseService, ITeacherService teacherService)
        {
            try
            {
                Console.Write("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                Console.Write("Enter Teacher ID: ");
                int teacherId = int.Parse(Console.ReadLine());

                courseService.AssignTeacher(courseId, teacherId);
                Console.WriteLine("Teacher assigned to course successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void MakePayment(IStudentService studentService)
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int studentId = int.Parse(Console.ReadLine());

                Console.Write("Enter Payment Amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                studentService.MakePayment(studentId, amount, DateTime.Now);
                Console.WriteLine("Payment recorded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        static void GenerateEnrollmentReport(ICourseService courseService, IEnrollmentService enrollmentService, IStudentService studentService)
        {
            try
            {
                Console.Write("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                var enrollments = enrollmentService.GetEnrollmentsByCourse(courseId);
                Console.WriteLine("Enrollment Report:");
                foreach (var enrollment in enrollments)
                {
                    var student = studentService.GetStudentById(enrollment.StudentID);
                    Console.WriteLine($"- {student.FirstName} {student.LastName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void GeneratePaymentReport(IStudentService studentService, IPaymentService paymentService)
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int studentId = int.Parse(Console.ReadLine());

                var payments = studentService.GetPaymentHistory(studentId);
                Console.WriteLine("Payment Report:");
                foreach (var payment in payments)
                {
                    Console.WriteLine($"- Amount: {payment.Amount}, Date: {payment.PaymentDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        static void CalculateCourseStatistics(ICourseService courseService, IEnrollmentService enrollmentService, IStudentService studentService, IPaymentService paymentService)
        {
            try
            {
                Console.Write("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                var enrollments = enrollmentService.GetEnrollmentsByCourse(courseId);
                int numberOfEnrollments = enrollments.Count();

                decimal totalPayments = 0;
                foreach (var enrollment in enrollments)
                {
                    var payments = paymentService.GetPaymentsByStudent(enrollment.StudentID);
                    totalPayments += payments.Sum(p => p.Amount);
                }

                Console.WriteLine($"Course Statistics:");
                Console.WriteLine($"- Number of Enrollments: {numberOfEnrollments}");
                Console.WriteLine($"- Total Payments Received: {totalPayments}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        static void DisplayData() {
            InMemDB db = InMemDB.Instance;
            Console.WriteLine("Students:");
            foreach (var student in db.Students)
            {
                Console.WriteLine($"ID: {student.StudentID}, Name: {student.FirstName} {student.LastName}");
            }
            Console.WriteLine("\nCourses:");
            foreach (var course in db.Courses)
            {
                Console.WriteLine($"Course ID: {course.CourseID}, Course Name: {course.CourseName},Course Code: {course.CourseCode},Teacher ID: {course.TeacherID}");
            }
            Console.WriteLine("\nTeachers:");
            foreach (var teacher in db.Teachers)
            {
                Console.WriteLine($"Teacher ID: {teacher.TeacherID}, Name: {teacher.FirstName} {teacher.LastName}");
            }
            Console.WriteLine("\nEnrollments:");
            foreach (var enrollment in db.Enrollments)
            {
                Console.WriteLine($"Enrollment ID: {enrollment.EnrollmentID}, Student ID: {enrollment.StudentID}, Course ID: {enrollment.CourseID}");
            }
            Console.WriteLine("\nPayments:");
            foreach (var payment in db.Payments)
            {
                Console.WriteLine($"Payment ID: {payment.PaymentID}, Student ID: {payment.StudentID}, Amount: {payment.Amount}");
            }
        }
    }
}
