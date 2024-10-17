using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.interfaces
{
    public interface ITeacherService
    {
        void DisplayTeacherInfo(int teacherId);
        void UpdateTeacherInfo(int teacherId, string firstName, string lastName, string email);
        IEnumerable<Course> GetAssignedCourses(int teacherId);
    }
}
