using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.interfaces
{
    public interface IEnrollmentRepository
    {
        Enrollment GetById(int enrollmentId);
        IEnumerable<Enrollment> GetAll();
        void Add(Enrollment enrollment);
        void Update(Enrollment enrollment);
        void Delete(int enrollmentId);
    }
}
