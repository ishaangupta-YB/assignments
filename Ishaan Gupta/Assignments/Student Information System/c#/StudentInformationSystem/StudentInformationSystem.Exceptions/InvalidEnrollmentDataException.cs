using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.Exceptions
{
    public class InvalidEnrollmentDataException : ApplicationException
    {
        public InvalidEnrollmentDataException(string message) : base(message)
        {
            Console.WriteLine(message);
        }
    }
}
