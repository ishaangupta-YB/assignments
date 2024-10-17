using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.BusinessLayer.interfaces;
using StudentInformationSystem.Entity;
using StudentInformationSystem.Util;

namespace StudentInformationSystem.BusinessLayer.repositories
{
    // handles db operations related to the Course table
    public class CourseRepository : ICourseRepository
    {
        private readonly QueryBuilder queryBuilder;
        public CourseRepository()
        {
            queryBuilder = new QueryBuilder();
        }

        // Adds a new course record to the db
        public void Add(Course course)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"CourseName", course.CourseName },
                    {"CourseCode", course.CourseCode },
                    {"InstructorName", course.InstructorName }
                };

                var query = queryBuilder.Insert("Courses", columnValues).Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        // Deletes a course record from the db based on the course ID.
        public void Delete(int courseId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Delete("Courses").Where("CourseID = @CourseID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    command.ExecuteNonQuery();
                }
            }
        }
        // Retrieves all course records from the db
        public IEnumerable<Course> GetAll()
        {
            var courses = new List<Course>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Courses").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var course = new Course
                        {
                            CourseID = (int)reader["CourseID"],
                            CourseName = reader["CourseName"].ToString(),
                            CourseCode = reader["CourseCode"].ToString(),
                            InstructorName = reader["InstructorName"].ToString()
                        };
                        courses.Add(course);
                    }
                }
            }
            return courses;
        }

        // Retrieves a course record by its ID
        public Course GetById(int courseId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Courses").Where("CourseID = @CourseID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CourseID", courseId);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Course
                        {
                            CourseID = (int)reader["CourseID"],
                            CourseName = reader["CourseName"].ToString(),
                            CourseCode = reader["CourseCode"].ToString(),
                            InstructorName = reader["InstructorName"].ToString()
                        };
                    }
                }
            }
            return null;
        }
        // Updates an existing course record in the db
        public void Update(Course course)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"CourseName", course.CourseName },
                    {"CourseCode", course.CourseCode },
                    {"InstructorName", course.InstructorName }
                };

                var query = queryBuilder.Update("Courses", columnValues).Where("CourseID = @CourseID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.Parameters.AddWithValue("@CourseID", course.CourseID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
