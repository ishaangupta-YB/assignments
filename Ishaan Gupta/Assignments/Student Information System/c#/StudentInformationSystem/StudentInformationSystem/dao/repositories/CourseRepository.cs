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
    public class CourseRepository:ICourseRepository
    {
        private readonly InMemDB db = InMemDB.Instance;

        public void Add(Course course)
        {
            db.Courses.Add(course);
        }
        public void Delete(int courseId)
        {
            var course = GetById(courseId);
            if (course != null)
            {
                db.Courses.Remove(course);
            }
        }

        public IEnumerable<Course> GetAll()
        {
            return db.Courses;
        }

        public Course GetById(int courseId)
        {
            return db.Courses.FirstOrDefault(c => c.CourseID == courseId);
        }

        public void Update(Course course)
        {
            var existingCourse = GetById(course.CourseID);
            if (existingCourse != null)
            {
                existingCourse.CourseName = course.CourseName;
                existingCourse.CourseCode = course.CourseCode;
                existingCourse.TeacherID = course.TeacherID;
            }
        }
    }
}
