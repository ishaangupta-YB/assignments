using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CarConnect.exceptions;

namespace CarConnect.util
{
    public class DBConn
    {
        private static readonly string connectionString;
        static DBConn()
        {
            connectionString = ConfigurationManager.ConnectionStrings["CarConnectDB"]?.ConnectionString;
            //Console.WriteLine(connectionString);    
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new DatabaseConnectionException("Connection string 'CarConnectDB' is not configured correctly.");
            }
        }
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
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
