using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;

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
        public Teacher GetTeacherById(int teacherId)
        {
            return teacherRepository.GetById(teacherId);
        }

        public void AddTeacher(Teacher teacher)
        {
            teacherRepository.Add(teacher);
        }
        public void UpdateTeacher(int teacherId, string firstName, string lastName, string email)
        {
            var teacher = teacherRepository.GetById(teacherId);
            if (teacher == null) Console.WriteLine("Teacher not found");
                //throw new TeacherNotFoundException("Teacher not found.");

            teacher.FirstName = firstName;
            teacher.LastName = lastName;
            teacher.Email = email;

            teacherRepository.Update(teacher);
        }

        public void DeleteTeacher(int teacherId)
        {
            teacherRepository.Delete(teacherId);
        }

        public IEnumerable<Course> GetAssignedCourses(int teacherId)
        {
            return courseRepository.GetAll().Where(c => c.TeacherID == teacherId);
        }
    }
}
