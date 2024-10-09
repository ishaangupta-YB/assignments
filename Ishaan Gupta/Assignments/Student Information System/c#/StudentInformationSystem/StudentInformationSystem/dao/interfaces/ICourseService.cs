using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.interfaces
{
    public interface ICourseService
    {
        void AssignTeacher(int courseId, int teacherId);
        void DisplayCourseInfo(int courseId);
        IEnumerable<Enrollment> GetEnrollments(int courseId);
        Teacher GetTeacher(int courseId);
        void UpdateCourseInfo(int courseId, string courseName, string courseCode, string instructorName);

    }
}
