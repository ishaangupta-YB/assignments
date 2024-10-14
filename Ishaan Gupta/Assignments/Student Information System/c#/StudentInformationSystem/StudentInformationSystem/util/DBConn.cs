using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace StudentInformationSystem.util
{
    public class DBConn
    {
        // retrieving string from the app configuration
        private static readonly string connectionString;

        // constructor to initialize the connection string
        static DBConn()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SISDB"]?.ConnectionString;
            //Console.WriteLine(connectionString);    
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'SISDB' is not configured correctly.");
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

                // checks if the connection was established successfully
                if (con == null || con.State != System.Data.ConnectionState.Open)
                {
                    throw new InvalidOperationException("Failed to establish a database connection.");
                }

                return con;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            } 
        }
    }
}
