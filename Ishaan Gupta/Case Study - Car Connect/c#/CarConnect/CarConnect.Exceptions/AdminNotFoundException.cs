using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    public class AdminNotFoundException : Exception
    {
        public AdminNotFoundException() : base() { }

        public AdminNotFoundException(string message) : base(message) { }

        public AdminNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
