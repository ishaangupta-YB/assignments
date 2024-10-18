using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CarConnect.Exceptions;
using NLog;

namespace CarConnect.Util
{
    public class DBConn
    {
        private static readonly string connectionString;

        // constructor to initialize the connection string
        static DBConn()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["CarConnectDB"]?.ConnectionString;
                //Console.WriteLine(connectionString);
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    throw new DatabaseConnectionException("Connection string 'CarConnectDB' is not configured correctly.");
                }
            }
            catch (DatabaseConnectionException ex)
            {
                LoggerService.LogError("Database connection string is not configured correctly.", ex);
                throw;
            }
            catch (Exception ex)
            {
                LoggerService.LogError("Unexpected error initializing database connection.", ex);
                throw new DatabaseConnectionException("An error occurred while initializing the database connection.");
            }
        }

        // Method to establish and return a new SQL connection
        public static SqlConnection GetConnection()
        {
            SqlConnection con = null;
            try
            {
                con = new SqlConnection(connectionString);
                con.Open();
                if (con == null || con.State != System.Data.ConnectionState.Open)
                {
                    throw new DatabaseConnectionException("Failed to establish a database connection.");
                }

                return con;
            }
            catch (DatabaseConnectionException ex)
            {
                LoggerService.LogError("Failed to establish a database connection.", ex);
                throw; // Re-throw to be handled by calling code
            }
            catch (Exception ex)
            {
                LoggerService.LogError("An unexpected error occurred while connecting to the database.", ex);
                throw new DatabaseConnectionException("An error occurred while connecting to the database.");
            }
        }
    }
}
