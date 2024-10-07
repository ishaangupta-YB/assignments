using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.interfaces
{
    public interface ITeacherService
    {
        Teacher GetTeacherById(int teacherId);
        void AddTeacher(Teacher teacher);
        void UpdateTeacher(int teacherId, string firstName, string lastName, string email);
        void DeleteTeacher(int teacherId);
        IEnumerable<Course> GetAssignedCourses(int teacherId);
    }
}
