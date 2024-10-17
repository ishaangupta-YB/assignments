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
    // handles db operations related to the Student table
    public class StudentRepository : IStudentRepository
    {
        private readonly QueryBuilder queryBuilder;
        public StudentRepository()
        {
            queryBuilder = new QueryBuilder();
        }

        // Adds a new student record to the db
        public void Add(Student student)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"FirstName", student.FirstName },
                    {"LastName", student.LastName },
                    {"DateOfBirth", student.DateOfBirth },
                    {"Email", student.Email },
                    {"PhoneNumber", student.PhoneNumber }
                };

                var query = queryBuilder.Insert("Students", columnValues).Build();
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

        // Delete a student record from the db based on the student ID
        public void Delete(int studentId)
        {
            var student = GetById(studentId);
            if (student != null)
            {
                using (var connection = DBConn.GetConnection())
                {
                    var query = queryBuilder.Delete("Students").Where("StudentID = @StudentID").Build();
                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StudentID", studentId);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        // Retrieves all student records from the db
        public IEnumerable<Student> GetAll()
        {
            var students = new List<Student>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Students").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var student = new Student
                        {
                            StudentID = (int)reader["StudentID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString()
                        };
                        students.Add(student);
                    }
                }
            }
            return students;
        }

        // Retrieves a student record by its ID.
        public Student GetById(int studentId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Students").Where("StudentID = @StudentID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StudentID", studentId);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Student
                        {
                            StudentID = (int)reader["StudentID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            DateOfBirth = (DateTime)reader["DateOfBirth"],
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        // Updates an existing student record in the db
        public void Update(Student student)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"FirstName", student.FirstName },
                    {"LastName", student.LastName },
                    {"DateOfBirth", student.DateOfBirth },
                    {"Email", student.Email },
                    {"PhoneNumber", student.PhoneNumber }
                };

                var query = queryBuilder.Update("Students", columnValues).Where("StudentID = @StudentID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.Parameters.AddWithValue("@StudentID", student.StudentID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
