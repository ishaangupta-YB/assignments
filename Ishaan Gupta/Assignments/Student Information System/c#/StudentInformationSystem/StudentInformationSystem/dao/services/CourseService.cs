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
    // business logic for handling course-related operations
    public class CourseService:ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IEnrollmentRepository enrollmentRepository;
        private readonly ITeacherRepository teacherRepository;

        // Constructor to init the repositories
        public CourseService(ICourseRepository courseRepo, IEnrollmentRepository enrollmentRepo, ITeacherRepository teacherRepo )
        {
            courseRepository = courseRepo;
            enrollmentRepository = enrollmentRepo;
            teacherRepository = teacherRepo;
        }

        // Assign a teacher to a course by updating the instructor's name in the course record
        public void AssignTeacher(int courseId, int teacherId)
        {
            var course = courseRepository.GetById(courseId);
            if (course == null) throw new CourseNotFoundException("Course not found");

            var teacher = teacherRepository.GetById(teacherId);
            if (teacher == null)  throw new TeacherNotFoundException("Teacher not found.");
            course.InstructorName = $"{teacher.FirstName} {teacher.LastName}";
            courseRepository.Update(course);
        }

        // Update course information such as name, code, and instructorName
        public void UpdateCourseInfo(int courseId, string courseName, string courseCode,string instructorName)
        {
            var course = courseRepository.GetById(courseId);
            if (course == null) throw new CourseNotFoundException("Course not found.");

            course.CourseName = courseName;
            course.CourseCode = courseCode;
            course.InstructorName = instructorName;
            courseRepository.Update(course);
        }

        // Display course information based on the course ID
        public void DisplayCourseInfo(int courseId)
        {
            var course = courseRepository.GetById(courseId);
            if (course == null) throw new CourseNotFoundException("Course not found.");
            Console.WriteLine($"Course ID: {course.CourseID} \nCourse Name: {course.CourseName} \nCourse Code: {course.CourseCode} \nInstructor: {course.InstructorName}");
        }

        // Retrieve all enrollments for a specific course
        public IEnumerable<Enrollment> GetEnrollments(int courseId)
        {
            var enrollments = enrollmentRepository.GetAll().Where(e => e.CourseID == courseId).ToList();
            if (enrollments == null) throw new EnrollmentNotFoundException("Enrollment not found.");
            return enrollments;
        }

        // Retrieve the teacher assigned to a specific course
        public Teacher GetTeacher(int courseId)
        {
            var course = courseRepository.GetById(courseId);
            if (course == null) throw new CourseNotFoundException("Course not found.");
            return teacherRepository.GetAll().FirstOrDefault(t => $"{t.FirstName} {t.LastName}" == course.InstructorName);
        }
    }
}
