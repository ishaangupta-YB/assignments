using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.BusinessLayer.interfaces;
using StudentInformationSystem.Entity;
using StudentInformationSystem.Exceptions;

namespace StudentInformationSystem.BusinessLayer.services
{
    // business logic for handling enrollment-related operations
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        // Constructor to init the repositories
        public EnrollmentService(IEnrollmentRepository enrollmentRepo, IStudentRepository studentRepo, ICourseRepository courseRepo)
        {
            enrollmentRepository = enrollmentRepo;
            studentRepository = studentRepo;
            courseRepository = courseRepo;
        }

        // Retrieve the student with a specific enrollment
        public Student GetStudent(int enrollmentId)
        {
            var enrollment = enrollmentRepository.GetById(enrollmentId);
            if (enrollment == null) throw new EnrollmentNotFoundException("Enrollment not found.");
            var student = studentRepository.GetById(enrollment.StudentID);
            if (student == null) throw new StudentNotFoundException("Student not found.");
            return student;
        }

        // Retrieve the course with a specific enrollment
        public Course GetCourse(int enrollmentId)
        {
            var enrollment = enrollmentRepository.GetById(enrollmentId);
            if (enrollment == null) throw new EnrollmentNotFoundException("Enrollment not found.");
            var course = courseRepository.GetById(enrollment.CourseID);
            if (course == null) throw new CourseNotFoundException("Course not found.");
            return course;
        }
    }
}
