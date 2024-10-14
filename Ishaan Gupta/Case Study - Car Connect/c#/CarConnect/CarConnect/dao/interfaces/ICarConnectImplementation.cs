using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarConnect.dao.interfaces
{
    public interface ICarConnectImplementation
    {
        void RegisterCustomer();
        void AdminLogin();
        void CustomerLogin();
    }
}
