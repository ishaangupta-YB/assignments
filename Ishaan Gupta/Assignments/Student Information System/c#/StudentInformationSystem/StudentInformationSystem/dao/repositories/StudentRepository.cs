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
    public class StudentRepository:IStudentRepository
    {
        private readonly InMemDB db = InMemDB.Instance;
        public void Add(Student student)
        {
            db.Students.Add(student);
        }

        public void Delete(int studentId)
        {
            var student = GetById(studentId);
            if (student != null)
            {
                db.Students.Remove(student);
            }
        }

        public IEnumerable<Student> GetAll()
        {
            return db.Students;
        }
        public Student GetById(int studentId)
        {
            return db.Students.FirstOrDefault(s => s.StudentID == studentId);
        }
        public void Update(Student student)
        {
            var existingStudent = GetById(student.StudentID);
            if (existingStudent != null)
            { 
                existingStudent.FirstName = student.FirstName;
                existingStudent.LastName = student.LastName;
                existingStudent.DateOfBirth = student.DateOfBirth;
                existingStudent.Email = student.Email;
                existingStudent.PhoneNumber = student.PhoneNumber;
            }
        }
    }
}
