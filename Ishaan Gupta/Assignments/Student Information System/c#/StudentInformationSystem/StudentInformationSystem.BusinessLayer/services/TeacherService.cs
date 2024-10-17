using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.BusinessLayer.interfaces;
using StudentInformationSystem.Entity;
using StudentInformationSystem.Exceptions;

namespace StudentInformationSystem.BusinessLayer.services
{
    // business logic for handling teacher-related operations
    public class TeacherService : ITeacherService
    {
        // Repositories for interacting with the teacher and course entities.
        private readonly ITeacherRepository teacherRepository;
        private readonly ICourseRepository courseRepository;

        // Constructor to init the repositories.
        public TeacherService(ITeacherRepository teacherRepo, ICourseRepository courseRepo)
        {
            teacherRepository = teacherRepo;
            courseRepository = courseRepo;
        }

        // Update teacher info 
        public void UpdateTeacherInfo(int teacherId, string firstName, string lastName, string email)
        {
            var teacher = teacherRepository.GetById(teacherId);
            if (teacher == null) throw new TeacherNotFoundException("Teacher not found.");
            teacher.FirstName = firstName;
            teacher.LastName = lastName;
            teacher.Email = email;
            teacherRepository.Update(teacher);
        }

        // Display teacher information based on the teacher ID
        public void DisplayTeacherInfo(int teacherId)
        {
            var teacher = teacherRepository.GetById(teacherId);
            if (teacher == null) throw new TeacherNotFoundException("Teacher not found.");
            Console.WriteLine($"Teacher: {teacher.FirstName} {teacher.LastName}, Email: {teacher.Email}");
        }

        // returns the list of courses assigned to a specific teacher
        public IEnumerable<Course> GetAssignedCourses(int teacherId)
        {
            var teacher = teacherRepository.GetById(teacherId);
            if (teacher == null) throw new TeacherNotFoundException("Teacher not found.");
            var assignedCourses = new List<Course>();
            var courses = courseRepository.GetAll();
            foreach (var course in courses)
            {
                // Retrieve courses assigned to this teacher by matching the instructor's name
                if (course.InstructorName == $"{teacher.FirstName} {teacher.LastName}") assignedCourses.Add(course);
            }
            return assignedCourses;

        }
    }
}
