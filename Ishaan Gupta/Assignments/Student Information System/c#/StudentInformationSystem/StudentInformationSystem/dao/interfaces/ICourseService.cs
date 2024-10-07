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
        Course GetCourseById(int courseId);
        IEnumerable<Course> GetCoursesByTeacher(int teacherId);
        void AddCourse(Course course);
        void UpdateCourse(int courseId, string courseName, string courseCode);
        void DeleteCourse(int courseId);
    }
}
