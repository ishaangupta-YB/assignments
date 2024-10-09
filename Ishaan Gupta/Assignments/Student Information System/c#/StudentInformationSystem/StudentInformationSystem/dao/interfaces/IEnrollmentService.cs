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
        Course GetCourse(int enrollmentId);
        Student GetStudent(int enrollmentId);
    }
}
