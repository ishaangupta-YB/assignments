using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.util;

namespace StudentInformationSystem.dao.repositories
{
    public class TeacherRepository:ITeacherRepository
    {
        private readonly InMemDB db = InMemDB.Instance;
        public void Add(Teacher teacher)
        {
            db.Teachers.Add(teacher);
        }
        public void Delete(int teacherId)
        {
            var teacher = GetById(teacherId);
            if (teacher != null)
            {
                db.Teachers.Remove(teacher);
            }
        }
        public Teacher GetById(int teacherId)
        {
            return db.Teachers.FirstOrDefault(t => t.TeacherID == teacherId);
        }

        public void Update(Teacher teacher)
        {
            var existingTeacher = GetById(teacher.TeacherID);
            if (existingTeacher != null)
            {
                existingTeacher.FirstName = teacher.FirstName;
                existingTeacher.LastName = teacher.LastName;
                existingTeacher.Email = teacher.Email;
            }
        }
        public IEnumerable<Teacher> GetAll()
        {
            return db.Teachers;
        }
    }
}
