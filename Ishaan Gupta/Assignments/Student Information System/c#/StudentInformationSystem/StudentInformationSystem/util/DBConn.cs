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
        private static readonly string connectionString;
        static DBConn()
        {
            connectionString = ConfigurationManager.ConnectionStrings["SISDB"]?.ConnectionString;
            //Console.WriteLine(connectionString);    
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("Connection string 'SISDB' is not configured correctly.");
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
