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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly QueryBuilder queryBuilder;
        public CustomerRepository()
        {
            queryBuilder = new QueryBuilder();
        }
        public void Add(Customer customer)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    { "FirstName", customer.FirstName },
                    { "LastName", customer.LastName },
                    { "Email", customer.Email },
                    { "PhoneNumber", customer.PhoneNumber },
                    { "Address", customer.Address },
                    { "Username", customer.Username },
                    { "Password", customer.Password },
                    { "RegistrationDate", customer.RegistrationDate }
                };

                var query = queryBuilder.Insert("Customers", columnValues).Build();
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
        public Customer GetByUsername(string username)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = "SELECT * FROM Customers WHERE Username = @Username";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Address = reader["Address"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            RegistrationDate = (DateTime)reader["RegistrationDate"]
                        };
                    }
                }
            }
            return null;
        }
        public void Delete(int customerId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Delete("Customers").Where("CustomerID = @CustomerID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Customer> GetAll()
        {
            var customers = new List<Customer>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Customers").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var customer = new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Address = reader["Address"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            RegistrationDate = (DateTime)reader["RegistrationDate"]
                        };
                        customers.Add(customer);
                    }
                }
            }
            return customers;
        }

        public Customer GetById(int customerId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Customers").Where("CustomerID = @CustomerID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerId);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Customer
                        {
                            CustomerID = (int)reader["CustomerID"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = reader["PhoneNumber"].ToString(),
                            Address = reader["Address"].ToString(),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            RegistrationDate = (DateTime)reader["RegistrationDate"]
                        };
                    }
                }
            }
            return null;
        }

        public void Update(Customer customer)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    { "FirstName", customer.FirstName },
                    { "LastName", customer.LastName },
                    { "Email", customer.Email },
                    { "PhoneNumber", customer.PhoneNumber },
                    { "Address", customer.Address },
                    { "Username", customer.Username },
                    { "Password", customer.Password }
                };

                var query = queryBuilder.Update("Customers", columnValues).Where("CustomerID = @CustomerID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.Parameters.AddWithValue("@CustomerID", customer.CustomerID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
