using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.exceptions
{ 
    public class AdminNotFoundException : ApplicationException
    {
        public AdminNotFoundException(string message) : base(message)
        {
            Console.WriteLine(message);
        }
    }
}
