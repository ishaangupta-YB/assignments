using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.services
{
    public class CourseService:ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly ITeacherRepository teacherRepository;

        public CourseService(ICourseRepository courseRepo, IEnrollmentRepository enrollmentRepo, ITeacherRepository teacherRepo)
        {
            courseRepository = courseRepo;
            enrollmentRepository = enrollmentRepo;
            teacherRepository = teacherRepo;
        }

        public void AssignTeacher(int courseId, int teacherId)
        {
            var course = courseRepository.GetById(courseId);
            if (course == null) Console.WriteLine("Course not found");
                //throw new CourseNotFoundException("Course not found");

            var teacher = teacherRepository.GetById(teacherId);
            if (teacher == null) Console.WriteLine("Teacher not found");
            //throw new TeacherNotFoundException("Teacher not found.");

            course.TeacherID = teacherId;
            courseRepository.Update(course);
        }
        public Course GetCourseById(int courseId)
        {
            return courseRepository.GetById(courseId);
        }

        public IEnumerable<Course> GetCoursesByTeacher(int teacherId)
        {
            return courseRepository.GetAll().Where(c => c.TeacherID == teacherId);
        }

        public void AddCourse(Course course)
        {
            courseRepository.Add(course);
        }
        public void UpdateCourse(int courseId, string courseName, string courseCode)
        {
            var course = courseRepository.GetById(courseId);
            if (course == null) Console.WriteLine("Course not found");
            //throw new CourseNotFoundException("Course not found.");

            course.CourseName = courseName;
            course.CourseCode = courseCode;

            courseRepository.Update(course);
        }
        public void DeleteCourse(int courseId)
        {
            courseRepository.Delete(courseId);
        }
    }
}
