using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.BusinessLayer.interfaces;
using CarConnect.Entity;
using CarConnect.Util;

namespace CarConnect.BusinessLayer.repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly QueryBuilder queryBuilder;

        public AdminRepository()
        {
            queryBuilder = new QueryBuilder();
        }

        public void Add(Admin admin)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    { "FirstName", admin.FirstName },
                    { "LastName", admin.LastName },
                    { "Email", admin.Email },
                    { "PhoneNumber", admin.PhoneNumber },
                    { "Username", admin.Username },
                    { "Password", admin.Password },
                    { "Role", admin.Role },
                    { "JoinDate", admin.JoinDate }
                };

                var query = queryBuilder.Insert("Admins", columnValues).Build();
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

        public void Delete(int adminId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Delete("Admins").Where("AdminID = @AdminID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AdminID", adminId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Admin> GetAll()
        {
            var admins = new List<Admin>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Admins").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var admin = new Admin
                        {
                            AdminID = (int)reader["AdminID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString(),
                            JoinDate = (DateTime)reader["JoinDate"]
                        };
                        admins.Add(admin);
                    }
                }
            }
            return admins;
        }

        public Admin GetById(int adminId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Admins").Where("AdminID = @AdminID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@AdminID", adminId);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Admin
                        {
                            AdminID = (int)reader["AdminID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString(),
                            JoinDate = (DateTime)reader["JoinDate"]
                        };
                    }
                }
            }
            return null;
        }

        public Admin GetByUsername(string username)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Admins").Where("Username = @Username").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Admin
                        {
                            AdminID = (int)reader["AdminID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString(),
                            JoinDate = (DateTime)reader["JoinDate"]
                        };
                    }
                }
            }
            return null;
        }

        public void Update(Admin admin)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    { "FirstName", admin.FirstName },
                    { "LastName", admin.LastName },
                    { "Email", admin.Email },
                    { "PhoneNumber", admin.PhoneNumber },
                    { "Username", admin.Username },
                    { "Password", admin.Password },
                    { "Role", admin.Role }
                };

                var query = queryBuilder.Update("Admins", columnValues).Where("AdminID = @AdminID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.Parameters.AddWithValue("@AdminID", admin.AdminID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
