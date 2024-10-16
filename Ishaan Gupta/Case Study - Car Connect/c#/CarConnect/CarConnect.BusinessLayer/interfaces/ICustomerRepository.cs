using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;

namespace CarConnect.BusinessLayer.interfaces
{
    public interface ICustomerRepository
    {
        Customer GetById(int customerId);
        void Add(Customer customer);
        IEnumerable<Customer> GetAll();
        void Update(Customer customer);
        Customer GetByUsername(string username);
        void Delete(int customerId);
    }
}
