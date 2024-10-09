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
    public class PaymentRepository:IPaymentRepository
    {
        private readonly QueryBuilder queryBuilder;

        public PaymentRepository()
        {
            queryBuilder = new QueryBuilder();
        }

        public void Add(Payment payment)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"StudentID", payment.StudentID },
                    {"Amount", payment.Amount },
                    {"PaymentDate", payment.PaymentDate }
                };

                var query = queryBuilder.Insert("Payments", columnValues).Build();
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
        public void Delete(int paymentId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Delete("Payments").Where("PaymentID = @PaymentID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PaymentID", paymentId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Payment> GetAll()
        {
            var payments = new List<Payment>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Payments").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var payment = new Payment
                        {
                            PaymentID = (int)reader["PaymentID"],
                            StudentID = (int)reader["StudentID"],
                            Amount = (decimal)reader["Amount"],
                            PaymentDate = (DateTime)reader["PaymentDate"]
                        };
                        payments.Add(payment);
                    }
                }
            }
            return payments;
        }

        public Payment GetById(int paymentId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Payments").Where("PaymentID = @PaymentID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PaymentID", paymentId);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Payment
                        {
                            PaymentID = (int)reader["PaymentID"],
                            StudentID = (int)reader["StudentID"],
                            Amount = (decimal)reader["Amount"],
                            PaymentDate = (DateTime)reader["PaymentDate"]
                        };
                    }
                }
            }
            return null;
        }

        public void Update(Payment payment)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    {"StudentID", payment.StudentID },
                    {"Amount", payment.Amount },
                    {"PaymentDate", payment.PaymentDate }
                };

                var query = queryBuilder.Update("Payments", columnValues).Where("PaymentID = @PaymentID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.Parameters.AddWithValue("@PaymentID", payment.PaymentID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
