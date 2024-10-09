using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.exceptions
{
    public class EnrollmentNotFoundException : ApplicationException
    {
        public EnrollmentNotFoundException(string message) : base(message)
        {
            Console.WriteLine(message);
        }
    }
}
