using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.interfaces
{
    public interface IEnrollmentService
    {
        IEnumerable<Enrollment> GetEnrollmentsByCourse(int courseId);
        IEnumerable<Enrollment> GetEnrollmentsByStudent(int studentId);
        void AddEnrollment(Enrollment enrollment);
        void DeleteEnrollment(int enrollmentId);
    }
}
