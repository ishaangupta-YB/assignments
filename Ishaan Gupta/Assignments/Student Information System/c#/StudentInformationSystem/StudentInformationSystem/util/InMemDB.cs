using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.util
{
    public class InMemDB
    {
        private static InMemDB instance = null;

        public List<Student> Students { get; private set; }
        public List<Course> Courses { get; private set; }
        public List<Teacher> Teachers { get; private set; }
        public List<Enrollment> Enrollments { get; private set; }
        public List<Payment> Payments { get; private set; }

        private InMemDB()
        {
            Students = new List<Student>();
            Courses = new List<Course>();
            Teachers = new List<Teacher>();
            Enrollments = new List<Enrollment>();
            Payments = new List<Payment>();
        }

        public static InMemDB Instance
        {
            get
            {
                 if (instance == null)
                 {
                     instance = new InMemDB();
                 }
                 return instance;
            }
        }
    }
}
