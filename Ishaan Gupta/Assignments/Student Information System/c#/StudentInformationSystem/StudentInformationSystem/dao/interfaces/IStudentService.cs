using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.interfaces
{
    public interface IStudentService
    {
        void EnrollInCourse(int studentId, int courseId);
        void UpdateStudentInfo(int studentId, string firstName, string lastName, DateTime dateOfBirth, string email, string phoneNumber);
        void MakePayment(int studentId, decimal amount, DateTime? paymentDate);
        IEnumerable<Course> GetEnrolledCourses(int studentId);
        IEnumerable<Payment> GetPaymentHistory(int studentId);
        void DisplayStudentInfo(int studentId);
    }
}
