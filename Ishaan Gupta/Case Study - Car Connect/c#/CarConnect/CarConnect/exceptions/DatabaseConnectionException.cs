﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.exceptions
{ 
    public class DatabaseConnectionException : ApplicationException
    {
        public DatabaseConnectionException(string message) : base(message)
        {
            Console.WriteLine(message);
        }
    }
}
