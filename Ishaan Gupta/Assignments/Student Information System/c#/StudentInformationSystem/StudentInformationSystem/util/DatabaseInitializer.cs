using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.util
{
    public static class DatabaseInitializer
    {
        public static void Initialize()
        {
            using (var connection = DBConn.GetConnection())
            {
                if (connection == null)
                {
                    Console.WriteLine("Database connection failed. Cannot initialize the database.");
                    return;
                }
                try
                {
                    string createTablesSql = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Students' AND xtype='U')
                        CREATE TABLE Students (
                            StudentID INT PRIMARY KEY IDENTITY(1,1),
                            FirstName NVARCHAR(50),
                            LastName NVARCHAR(50),
                            DateOfBirth DATE,
                            Email NVARCHAR(100),
                            PhoneNumber NVARCHAR(20)
                        );

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Courses' AND xtype='U')
                        CREATE TABLE Courses (
                            CourseID INT PRIMARY KEY IDENTITY(1,1),
                            CourseName NVARCHAR(100),
                            CourseCode NVARCHAR(50),
                            InstructorName NVARCHAR(100)
                        );

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Teachers' AND xtype='U')
                        CREATE TABLE Teachers (
                            TeacherID INT PRIMARY KEY IDENTITY(1,1),
                            FirstName NVARCHAR(50),
                            LastName NVARCHAR(50),
                            Email NVARCHAR(100)
                        );

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Enrollments' AND xtype='U')
                        CREATE TABLE Enrollments (
                            EnrollmentID INT PRIMARY KEY IDENTITY(1,1),
                            StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
                            CourseID INT FOREIGN KEY REFERENCES Courses(CourseID),
                            EnrollmentDate DATE
                        );

                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Payments' AND xtype='U')
                        CREATE TABLE Payments (
                            PaymentID INT PRIMARY KEY IDENTITY(1,1),
                            StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
                            Amount DECIMAL(18, 2),
                            PaymentDate DATE
                        );
                    ";

                    using (SqlCommand command = new SqlCommand(createTablesSql, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    //Console.WriteLine("Database initialized successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during database initialization: {ex.Message}");
                }
                finally
                {
                    connection.Close();  
                }
            }
        }
    }
}
