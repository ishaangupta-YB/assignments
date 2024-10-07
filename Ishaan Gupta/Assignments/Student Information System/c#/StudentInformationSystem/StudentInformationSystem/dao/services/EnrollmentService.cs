using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;

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
        public IEnumerable<Enrollment> GetEnrollmentsByCourse(int courseId)
        {
            return enrollmentRepository.GetAll().Where(e => e.CourseID == courseId);
        }

        public IEnumerable<Enrollment> GetEnrollmentsByStudent(int studentId)
        {
            return enrollmentRepository.GetAll().Where(e => e.StudentID == studentId);
        }

        public void AddEnrollment(Enrollment enrollment)
        {
            enrollmentRepository.Add(enrollment);
        }

        public void DeleteEnrollment(int enrollmentId)
        {
            enrollmentRepository.Delete(enrollmentId);
        }

    }
}
