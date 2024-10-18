using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.Exceptions
{
    public class CustomerNotFoundException : Exception
    {
        public CustomerNotFoundException() : base() { }

        public CustomerNotFoundException(string message) : base(message) { }

        public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }
}
