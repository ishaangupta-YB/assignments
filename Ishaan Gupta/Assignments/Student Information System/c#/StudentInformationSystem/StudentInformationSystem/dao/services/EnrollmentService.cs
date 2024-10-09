using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.exceptions;

namespace StudentInformationSystem.dao.services
{
    public class EnrollmentService:IEnrollmentService
    {
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly IStudentRepository studentRepository;
        private readonly ICourseRepository courseRepository;

        public EnrollmentService(IEnrollmentRepository enrollmentRepo, IStudentRepository studentRepo, ICourseRepository courseRepo)
        {
            enrollmentRepository = enrollmentRepo;
            studentRepository = studentRepo;
            courseRepository = courseRepo;
        }
        public Student GetStudent(int enrollmentId)
        {
            var enrollment = enrollmentRepository.GetById(enrollmentId);
            if (enrollment == null) throw new EnrollmentNotFoundException("Enrollment not found.");
            var student = studentRepository.GetById(enrollment.StudentID);
            if (student == null) throw new StudentNotFoundException("Student not found.");
            return student;
        }
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
