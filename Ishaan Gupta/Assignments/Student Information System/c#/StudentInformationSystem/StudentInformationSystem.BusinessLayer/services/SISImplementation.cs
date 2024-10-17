using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.BusinessLayer.interfaces;
using StudentInformationSystem.BusinessLayer.repositories;

namespace StudentInformationSystem.BusinessLayer.services
{
    public class SISImplementation : ISISImplementation
    {
        // Repositories for interacting with different tables in the DB
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;
        private readonly ITeacherRepository teacherRepository;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IPaymentRepository paymentRepository;

        // Services that contains business logic
        private readonly IStudentService studentService;
        private readonly ICourseService courseService;
        private readonly ITeacherService teacherService;
        private readonly IEnrollmentService enrollmentService;
        private readonly IPaymentService paymentService;
        public SISImplementation()
        {
            // Initializing repositories and services
            studentRepository = new StudentRepository();
            courseRepository = new CourseRepository();
            teacherRepository = new TeacherRepository();
            enrollmentRepository = new EnrollmentRepository();
            paymentRepository = new PaymentRepository();

            studentService = new StudentService(studentRepository, enrollmentRepository, courseRepository, paymentRepository);
            courseService = new CourseService(courseRepository, enrollmentRepository, teacherRepository);
            teacherService = new TeacherService(teacherRepository, courseRepository);
            enrollmentService = new EnrollmentService(enrollmentRepository, studentRepository, courseRepository);
            paymentService = new PaymentService(paymentRepository, studentRepository);
            //DisplayAllTablesData();
        }

        // method to enroll a student in a course
        public void EnrollStudentInCourse()
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

        // method to assign a teacher to a course
        public void AssignTeacherToCourse()
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

        // method to make a payment for a student
        public void MakePayment()
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int studentId = int.Parse(Console.ReadLine());

                Console.Write("Enter Payment Amount: ");
                decimal amount = decimal.Parse(Console.ReadLine());

                Console.Write("Enter Payment date (leave empty for today's date): ");
                string dateInput = Console.ReadLine();

                DateTime? paymentDate = null;
                if (!string.IsNullOrWhiteSpace(dateInput))
                {
                    if (!DateTime.TryParse(dateInput, out DateTime parsedDate))
                    {
                        throw new FormatException("Invalid date format.");
                    }
                    paymentDate = parsedDate;
                }

                studentService.MakePayment(studentId, amount, paymentDate);

                Console.WriteLine("Payment recorded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Method to generate report of students enrolled in a specific courses
        public void GenerateEnrollmentReport()
        {
            try
            {
                Console.Write("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                var enrollments = courseService.GetEnrollments(courseId);
                Console.WriteLine($"Enrollment Report for Course ID {courseId}:");
                foreach (var enrollment in enrollments)
                {
                    var student = enrollmentService.GetStudent(enrollment.EnrollmentID);
                    Console.WriteLine($"- {student.FirstName} {student.LastName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // Method to generate payment report for a student
        public void GeneratePaymentReport()
        {
            try
            {
                Console.Write("Enter Student ID: ");
                int studentId = int.Parse(Console.ReadLine());
                // Retrieve payment history for the specified student
                var payments = studentService.GetPaymentHistory(studentId);
                if (payments == null || payments.Count() == 0)
                {
                    Console.WriteLine($"No payments found");
                    return;
                }
                Console.WriteLine($"Payment Report for Student ID {studentId}:");
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

        // Method to calculate and display course statistics
        public void CalculateCourseStatistics()
        {
            try
            {
                Console.Write("Enter Course ID: ");
                int courseId = int.Parse(Console.ReadLine());

                var enrollments = courseService.GetEnrollments(courseId);
                int enrollmentCount = enrollments.Count();
                decimal totalPayments = 0;

                foreach (var enrollment in enrollments)
                {
                    // Sum of the payments made by each enrolled student
                    var payments = paymentRepository.GetAll().Where(p => p.StudentID == enrollment.StudentID).ToList();
                    totalPayments += payments.Sum(p => p.Amount);
                }

                Console.WriteLine($"Course Statistics for Course ID {courseId}:");
                Console.WriteLine($"- Number of Enrollments: {enrollmentCount}");
                Console.WriteLine($"- Total Payments: {totalPayments}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        // for my use only, debugging purpose 

        //public void DisplayAllTablesData()
        //{
        //    Console.WriteLine("Students:");
        //    var students = studentRepository.GetAll();
        //    foreach (var student in students) Console.WriteLine($"{student.StudentID} - {student.FirstName} {student.LastName}");

        //    Console.WriteLine("\nCourses:");
        //    var courses = courseRepository.GetAll();
        //    foreach (var course in courses) Console.WriteLine($"{course.CourseID} - {course.CourseName} ({course.CourseCode}) \t {course.InstructorName}");

        //    Console.WriteLine("\nTeachers:");
        //    var teachers = teacherRepository.GetAll();
        //    foreach (var teacher in teachers)  Console.WriteLine($"{teacher.TeacherID} - {teacher.FirstName} {teacher.LastName}");

        //    Console.WriteLine("\nEnrollments:");
        //    var enrollments = enrollmentRepository.GetAll();
        //    foreach (var enrollment in enrollments)  Console.WriteLine($"Enrollment ID: {enrollment.EnrollmentID}, StudentID: {enrollment.StudentID}, CourseID: {enrollment.CourseID}");

        //    Console.WriteLine("\nPayments:");
        //    var payments = paymentRepository.GetAll();
        //    foreach (var payment in payments)  Console.WriteLine($"Payment ID: {payment.PaymentID}, StudentID: {payment.StudentID}, Amount: {payment.Amount}, Date: {payment.PaymentDate}");
        //}

        // I have used this for app testing using mock data whenever DB is not in use
        //static void InitData(IStudentService studentService, ITeacherService teacherService, ICourseService courseService)
        //{
        //    studentService.AddStudent(new Student
        //    {
        //        StudentID = IDGenerator.GetNextStudentID(),
        //        FirstName = "Ishaan",
        //        LastName = "Gupta",
        //        DateOfBirth = new DateTime(2000, 1, 1),
        //        Email = "ishaangupta@gmail.com",
        //        PhoneNumber = "9876543210"
        //    });
        //    studentService.AddStudent(new Student
        //    {
        //        StudentID = IDGenerator.GetNextStudentID(),
        //        FirstName = "Akash",
        //        LastName = "Singh",
        //        DateOfBirth = new DateTime(1999, 5, 15),
        //        Email = "akashsingh@gmail.com",
        //        PhoneNumber = "9876543211"
        //    });

        //    studentService.AddStudent(new Student
        //    {
        //        StudentID = IDGenerator.GetNextStudentID(),
        //        FirstName = "Vinay",
        //        LastName = "Solanki",
        //        DateOfBirth = new DateTime(2001, 3, 10),
        //        Email = "vinaysolanki@gmail.com",
        //        PhoneNumber = "9876543212"
        //    });

        //    studentService.AddStudent(new Student
        //    {
        //        StudentID = IDGenerator.GetNextStudentID(),
        //        FirstName = "Mrunali",
        //        LastName = "Rajkule",
        //        DateOfBirth = new DateTime(2000, 12, 25),
        //        Email = "mrunalirajkule@gmail.com",
        //        PhoneNumber = "9876543213"
        //    });

        //    studentService.AddStudent(new Student
        //    {
        //        StudentID = IDGenerator.GetNextStudentID(),
        //        FirstName = "Rohit",
        //        LastName = "Sharma",
        //        DateOfBirth = new DateTime(1990, 7, 20),
        //        Email = "rohitsharma@gmail.com",
        //        PhoneNumber = "9876543214"
        //    });

        //    teacherService.AddTeacher(new Teacher
        //    {
        //        TeacherID = IDGenerator.GetNextTeacherID(),
        //        FirstName = "Varsha",
        //        LastName = "Patil",
        //        Email = "varshapatil@gmail.com"
        //    });

        //    teacherService.AddTeacher(new Teacher
        //    {
        //        TeacherID = IDGenerator.GetNextTeacherID(),
        //        FirstName = "Suresh",
        //        LastName = "Kumar",
        //        Email = "sureshkumar@gmail.com"
        //    });

        //    teacherService.AddTeacher(new Teacher
        //    {
        //        TeacherID = IDGenerator.GetNextTeacherID(),
        //        FirstName = "Shubhra",
        //        LastName = "Rana",
        //        Email = "SubhraR@gmail.com"
        //    });

        //    teacherService.AddTeacher(new Teacher
        //    {
        //        TeacherID = IDGenerator.GetNextTeacherID(),
        //        FirstName = "Raj",
        //        LastName = "Malhotra",
        //        Email = "rajmalhotra@gmail.com"
        //    });

        //    teacherService.AddTeacher(new Teacher
        //    {
        //        TeacherID = IDGenerator.GetNextTeacherID(),
        //        FirstName = "Kavita",
        //        LastName = "Sharma",
        //        Email = "kavitasharma@gmail.com"
        //    });

        //    courseService.AddCourse(new Course
        //    {
        //        CourseID = IDGenerator.GetNextCourseID(),
        //        CourseName = "Mathematics",
        //        CourseCode = "MATH101"
        //    });

        //    courseService.AddCourse(new Course
        //    {
        //        CourseID = IDGenerator.GetNextCourseID(),
        //        CourseName = "Physics",
        //        CourseCode = "PHY101"
        //    });

        //    courseService.AddCourse(new Course
        //    {
        //        CourseID = IDGenerator.GetNextCourseID(),
        //        CourseName = "CS",
        //        CourseCode = "CS101"
        //    });
        //}
    }
}
