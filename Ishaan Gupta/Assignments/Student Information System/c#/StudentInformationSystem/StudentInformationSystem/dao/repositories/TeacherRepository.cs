using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.util;

namespace StudentInformationSystem.dao.repositories
{
    public class TeacherRepository:ITeacherRepository
    {
        private readonly QueryBuilder queryBuilder;
        public TeacherRepository()
        {
            queryBuilder = new QueryBuilder();
        }
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

                var query = queryBuilder.Insert("Teachers", columnValues).Build();
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
        public Teacher GetById(int teacherId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Teachers").Where("TeacherID = @TeacherID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeacherID", teacherId);
                    var reader = command.ExecuteReader();
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
