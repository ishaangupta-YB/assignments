using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.exceptions;

namespace StudentInformationSystem.dao.services
{
    public class TeacherService:ITeacherService
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly ICourseRepository courseRepository;

        public TeacherService(ITeacherRepository teacherRepo, ICourseRepository courseRepo)
        {
            teacherRepository = teacherRepo;
            courseRepository = courseRepo;
        }
       
        public void UpdateTeacherInfo(int teacherId, string firstName, string lastName, string email)
        {
            var teacher = teacherRepository.GetById(teacherId);
            if (teacher == null)  throw new TeacherNotFoundException("Teacher not found.");
            teacher.FirstName = firstName;
            teacher.LastName = lastName;
            teacher.Email = email;
            teacherRepository.Update(teacher);
        }
        public void DisplayTeacherInfo(int teacherId)
        {
            var teacher = teacherRepository.GetById(teacherId);
            if (teacher == null)  throw new TeacherNotFoundException("Teacher not found.");
            Console.WriteLine($"Teacher: {teacher.FirstName} {teacher.LastName}, Email: {teacher.Email}");
        }
        public IEnumerable<Course> GetAssignedCourses(int teacherId)
        {
            var teacher = teacherRepository.GetById(teacherId);
            if (teacher == null) throw new TeacherNotFoundException("Teacher not found.");
            var assignedCourses = new List<Course>();
            var courses = courseRepository.GetAll();
            foreach (var course in courses)
            {
                if (course.InstructorName == $"{teacher.FirstName} {teacher.LastName}") assignedCourses.Add(course);
            }
            return assignedCourses;

        }
    }
}
