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
    // handles DB operations related to the Teacher table
    public class TeacherRepository : ITeacherRepository
    {
        private readonly QueryBuilder queryBuilder;
        // Constructor initializes the query builder for creating SQL queries
        public TeacherRepository()
        {
            queryBuilder = new QueryBuilder();
        }

        // method to add a new teacher record to the DB
        public void Add(Teacher teacher)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"FirstName", teacher.FirstName },
                    {"LastName", teacher.LastName },
                    {"Email", teacher.Email }
                };

                // Building the SQL query for inserting the teacher data
                var query = queryBuilder.Insert("Teachers", columnValues).Build();
                using (var command = new SqlCommand(query, connection))
                {
                    // Binding the parameter values
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.ExecuteNonQuery();
                }
            }
        }
        // method to delete teacher record from DB based on teacherId
        public void Delete(int teacherId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Delete("Teachers").Where("TeacherID = @TeacherID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeacherID", teacherId);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Retrieves a teacher record by its ID
        public Teacher GetById(int teacherId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Teachers").Where("TeacherID = @TeacherID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeacherID", teacherId);
                    var reader = command.ExecuteReader();

                    // Read and return the teacher data if found.
                    if (reader.Read())
                    {
                        return new Teacher
                        {
                            TeacherID = (int)reader["TeacherID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        // Update an existing teacher record in the DB
        public void Update(Teacher teacher)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"FirstName", teacher.FirstName },
                    {"LastName", teacher.LastName },
                    {"Email", teacher.Email }
                };

                var query = queryBuilder.Update("Teachers", columnValues).Where("TeacherID = @TeacherID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.Parameters.AddWithValue("@TeacherID", teacher.TeacherID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Retrieves all teacher records from the DB
        public IEnumerable<Teacher> GetAll()
        {
            var teachers = new List<Teacher>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Teachers").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var teacher = new Teacher
                        {
                            TeacherID = (int)reader["TeacherID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                        teachers.Add(teacher);
                    }
                }
            }
            return teachers;
        }
    }
}
