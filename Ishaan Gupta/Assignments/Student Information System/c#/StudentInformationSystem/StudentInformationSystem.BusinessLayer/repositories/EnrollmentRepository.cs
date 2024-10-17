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
    // handles db operations related to the Enrollment table
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly QueryBuilder queryBuilder;
        public EnrollmentRepository()
        {
            queryBuilder = new QueryBuilder();
        }

        // Adds a new enrollment record to the db
        public void Add(Enrollment enrollment)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"StudentID", enrollment.StudentID },
                    {"CourseID", enrollment.CourseID },
                    {"EnrollmentDate", enrollment.EnrollmentDate }
                };

                var query = queryBuilder.Insert("Enrollments", columnValues).Build();
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
        // Deletes an enrollment record from the db based on the enrollment ID.
        public void Delete(int enrollmentId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Delete("Enrollments").Where("EnrollmentID = @EnrollmentID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EnrollmentID", enrollmentId);
                    command.ExecuteNonQuery();
                }
            }
        }
        // Retrieves all enrollment records from the db
        public IEnumerable<Enrollment> GetAll()
        {
            var enrollments = new List<Enrollment>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Enrollments").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var enrollment = new Enrollment
                        {
                            EnrollmentID = (int)reader["EnrollmentID"],
                            StudentID = (int)reader["StudentID"],
                            CourseID = (int)reader["CourseID"],
                            EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                        };
                        enrollments.Add(enrollment);
                    }
                }
            }
            return enrollments;
        }

        // Retrieves an enrollment record by its ID.
        public Enrollment GetById(int enrollmentId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Enrollments").Where("EnrollmentID = @EnrollmentID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EnrollmentID", enrollmentId);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Enrollment
                        {
                            EnrollmentID = (int)reader["EnrollmentID"],
                            StudentID = (int)reader["StudentID"],
                            CourseID = (int)reader["CourseID"],
                            EnrollmentDate = (DateTime)reader["EnrollmentDate"]
                        };
                    }
                }
            }
            return null;
        }
        // Updates an existing enrollment record in the db
        public void Update(Enrollment enrollment)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"StudentID", enrollment.StudentID },
                    {"CourseID", enrollment.CourseID },
                    {"EnrollmentDate", enrollment.EnrollmentDate }
                };

                var query = queryBuilder.Update("Enrollments", columnValues).Where("EnrollmentID = @EnrollmentID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.Parameters.AddWithValue("@EnrollmentID", enrollment.EnrollmentID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
