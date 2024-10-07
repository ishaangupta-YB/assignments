using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.interfaces
{
    public interface ICourseRepository
    {
        Course GetById(int courseId);
        IEnumerable<Course> GetAll();
        void Add(Course course);
        void Update(Course course);
        void Delete(int courseId);
    }
}
