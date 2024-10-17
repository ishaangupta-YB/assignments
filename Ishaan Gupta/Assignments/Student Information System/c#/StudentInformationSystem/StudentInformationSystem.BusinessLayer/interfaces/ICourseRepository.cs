using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.interfaces
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
