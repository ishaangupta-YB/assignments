using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.exceptions;
using StudentInformationSystem.util;

namespace StudentInformationSystem.dao.services
{
    // business logic for handling student-related operations
    public class StudentService:IStudentService
    {
        // Repositories for interacting with the student, enrollment, course, and payment entities
        private readonly IStudentRepository studentRepository;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly ICourseRepository courseRepository;
        private readonly IPaymentRepository paymentRepository;
        
        // Constructor to init the repositories used by the service
        public StudentService(IStudentRepository studentRepo, IEnrollmentRepository enrollmentRepo, ICourseRepository courseRepo, IPaymentRepository paymentRepo)
        {
            studentRepository = studentRepo;
            enrollmentRepository = enrollmentRepo;
            courseRepository = courseRepo;
            paymentRepository = paymentRepo;
        }

        // Enroll a student in a course by checking if both exist and preventing duplicate enrollments
        public void EnrollInCourse(int studentId, int courseId)
        {
            var student = studentRepository.GetById(studentId);
            if (student == null)  throw new StudentNotFoundException("Student not found");

            var course = courseRepository.GetById(courseId);
            if (course == null)  throw new CourseNotFoundException("Course not found");

            var existingEnrollment = enrollmentRepository.GetAll().FirstOrDefault(e => e.StudentID == studentId && e.CourseID == courseId);
            if (existingEnrollment != null)  throw new DuplicateEnrollmentException("Student is already enrolled in this course.");

            var enrollment = new Enrollment
            {
                StudentID = studentId,
                CourseID = courseId,
                EnrollmentDate = DateTime.Now
            };
            enrollmentRepository.Add(enrollment);
        }

        // Update student information such as name, date of birth, email, and phone number
        public void UpdateStudentInfo(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber)
        {
            var student = studentRepository.GetById(studentId);
            if (student == null)  throw new StudentNotFoundException("Student not found.");

            if (string.IsNullOrWhiteSpace(email))  throw new InvalidStudentDataException("Email cannot be empty.");
            student.FirstName = firstName;
            student.LastName = lastName;
            student.DateOfBirth = dateOfBirth;
            student.Email = email;
            student.PhoneNumber = phoneNumber;
            studentRepository.Update(student);
        }

        // Make payment for student (added validation ensuring payment is valid)
        public void MakePayment(int studentId, decimal amount, DateTime? paymentDate = null)
        {
            var student = studentRepository.GetById(studentId);
            if (student == null)  throw new StudentNotFoundException("Student not found.");
            if (amount <= 0) throw new PaymentValidationException("Payment amount must be positive.");
            var payment = new Payment
            {
                StudentID = studentId,
                Amount = amount,
                PaymentDate = paymentDate ?? DateTime.Now
            };
            paymentRepository.Add(payment);
        }

        // Display student information based on the studentID
        public void DisplayStudentInfo(int studentId)
        {
            var student = studentRepository.GetById(studentId);
            if (student == null)  throw new StudentNotFoundException("Student not found.");
            Console.WriteLine($"Student: {student.FirstName} {student.LastName}, Email: {student.Email}, Phone: {student.PhoneNumber}");
        }

        // Get the list of courses in which a student is enrolled.
        public IEnumerable<Course> GetEnrolledCourses(int studentId)
        {
            var enrollments = enrollmentRepository.GetAll().Where(e => e.StudentID == studentId);
            var courseIds = enrollments.Select(e => e.CourseID).Distinct();
            return courseRepository.GetAll().Where(c => courseIds.Contains(c.CourseID));
        }

        // Get the payment history for specific student
        public IEnumerable<Payment> GetPaymentHistory(int studentId)
        {
            var student = studentRepository.GetById(studentId);
            if (student == null) throw new StudentNotFoundException("Student not found.");
            return paymentRepository.GetAll().Where(p => p.StudentID == studentId);
        } 
    }
}
