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
    // handles db operations related to the Payment table.
    public class PaymentRepository : IPaymentRepository
    {
        private readonly QueryBuilder queryBuilder;

        public PaymentRepository()
        {
            queryBuilder = new QueryBuilder();
        }

        // Adds a new payment record to the db
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

        // Delete a payment record from the db based on the payment ID.
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
        // Retrieves all payment records from the db
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

        // Retrieves a payment record by its ID.
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

        // Updates an existing payment record in the db
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
