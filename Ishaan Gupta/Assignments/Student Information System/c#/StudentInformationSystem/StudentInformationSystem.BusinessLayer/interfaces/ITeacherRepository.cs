using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.Entity;

namespace StudentInformationSystem.BusinessLayer.interfaces
{
    public interface ITeacherRepository
    {
        Teacher GetById(int teacherId);
        IEnumerable<Teacher> GetAll();
        void Add(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(int teacherId);
    }

}
