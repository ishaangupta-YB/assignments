using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.dao.interfaces;
using CarConnect.entity;
using CarConnect.util;

namespace CarConnect.dao.repositories
{
    public class VehicleRepository:IVehicleRepository
    {
        private readonly QueryBuilder queryBuilder;

        public VehicleRepository()
        {
            queryBuilder = new QueryBuilder();
        }

        public void Add(Vehicle vehicle)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    { "Model", vehicle.Model },
                    { "Make", vehicle.Make },
                    { "Year", vehicle.Year },
                    { "Color", vehicle.Color },
                    { "RegistrationNumber", vehicle.RegistrationNumber },
                    { "Availability", vehicle.Availability },
                    { "DailyRate", vehicle.DailyRate }
                };

                var query = queryBuilder.Insert("Vehicles", columnValues).Build();
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

        public void Delete(int vehicleId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Delete("Vehicles").Where("VehicleID = @VehicleID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicleId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Vehicle> GetAll()
        {
            var vehicles = new List<Vehicle>();
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Vehicles").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var vehicle = new Vehicle
                        {
                            VehicleID = (int)reader["VehicleID"],
                            Model = reader["Model"].ToString(),
                            Make = reader["Make"].ToString(),
                            Year = (int)reader["Year"],
                            Color = reader["Color"].ToString(),
                            RegistrationNumber = reader["RegistrationNumber"].ToString(),
                            Availability = (bool)reader["Availability"],
                            DailyRate = (decimal)reader["DailyRate"]
                        };
                        vehicles.Add(vehicle);
                    }
                }
            }
            return vehicles;
        }

        public Vehicle GetById(int vehicleId)
        {
            using (var connection = DBConn.GetConnection())
            {
                var query = queryBuilder.Select("Vehicles").Where("VehicleID = @VehicleID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@VehicleID", vehicleId);
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        return new Vehicle
                        {
                            VehicleID = (int)reader["VehicleID"],
                            Model = reader["Model"].ToString(),
                            Make = reader["Make"].ToString(),
                            Year = (int)reader["Year"],
                            Color = reader["Color"].ToString(),
                            RegistrationNumber = reader["RegistrationNumber"].ToString(),
                            Availability = (bool)reader["Availability"],
                            DailyRate = (decimal)reader["DailyRate"]
                        };
                    }
                }
            }
            return null;
        }

        public void Update(Vehicle vehicle)
        {
            using (var connection = DBConn.GetConnection())
            {
                var columnValues = new Dictionary<string, object>
                {
                    { "Model", vehicle.Model },
                    { "Make", vehicle.Make },
                    { "Year", vehicle.Year },
                    { "Color", vehicle.Color },
                    { "RegistrationNumber", vehicle.RegistrationNumber },
                    { "Availability", vehicle.Availability },
                    { "DailyRate", vehicle.DailyRate }
                };

                var query = queryBuilder.Update("Vehicles", columnValues).Where("VehicleID = @VehicleID").Build();
                using (var command = new SqlCommand(query, connection))
                {
                    foreach (var column in columnValues)
                    {
                        command.Parameters.AddWithValue($"@{column.Key}", column.Value);
                    }
                    command.Parameters.AddWithValue("@VehicleID", vehicle.VehicleID);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
