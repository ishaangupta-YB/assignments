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
    public class ReservationRepository : IReservationRepository
    {
        private readonly QueryBuilder queryBuilder;

        public ReservationRepository()
        {
            queryBuilder = new QueryBuilder();
        }

        public void Add(Reservation reservation)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    { "CustomerID", reservation.CustomerID },
                    { "VehicleID", reservation.VehicleID },
                    { "StartDate", reservation.StartDate },
                    { "EndDate", reservation.EndDate },
                    { "TotalCost", reservation.TotalCost },
                    { "Status", reservation.Status }
                };

                var query = queryBuilder.Insert("Reservations", columnValues).Build();
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

        public void Delete(int reservationId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Delete("Reservations").Where("ReservationID = @ReservationID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            var reservations = new List<Reservation>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Reservations").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var reservation = new Reservation
                        {
                            ReservationID = (int)reader["ReservationID"],
                            CustomerID = (int)reader["CustomerID"],
                            VehicleID = (int)reader["VehicleID"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (decimal)reader["TotalCost"],
                            Status = reader["Status"].ToString()
                        };
                        reservations.Add(reservation);
                    }
                }
            }
            return reservations;
        }

        public Reservation GetById(int reservationId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Reservations").Where("ReservationID = @ReservationID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReservationID", reservationId);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Reservation
                        {
                            ReservationID = (int)reader["ReservationID"],
                            CustomerID = (int)reader["CustomerID"],
                            VehicleID = (int)reader["VehicleID"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (decimal)reader["TotalCost"],
                            Status = reader["Status"].ToString()
                        };
                    }
                }
            }
            return null;
        }

        public IEnumerable<Reservation> GetReservationsByCustomerId(int customerId)
        {
            var reservations = new List<Reservation>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Reservations").Where("CustomerID = @CustomerID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CustomerID", customerId);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var reservation = new Reservation
                        {
                            ReservationID = (int)reader["ReservationID"],
                            CustomerID = (int)reader["CustomerID"],
                            VehicleID = (int)reader["VehicleID"],
                            StartDate = (DateTime)reader["StartDate"],
                            EndDate = (DateTime)reader["EndDate"],
                            TotalCost = (decimal)reader["TotalCost"],
                            Status = reader["Status"].ToString()
                        };
                        reservations.Add(reservation);
                    }
                }
            }
            return reservations;
        }
        public void Update(Reservation reservation)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    { "CustomerID", reservation.CustomerID },
                    { "VehicleID", reservation.VehicleID },
                    { "StartDate", reservation.StartDate },
                    { "EndDate", reservation.EndDate },
                    { "TotalCost", reservation.TotalCost },
                    { "Status", reservation.Status }
                };

                var query = queryBuilder.Update("Reservations", columnValues).Where("ReservationID = @ReservationID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.Parameters.AddWithValue("@ReservationID", reservation.ReservationID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
