﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;

namespace CarConnect.BusinessLayer.interfaces
{
    public interface ICustomerService
    {
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByUsername(string username);
        void RegisterCustomer(Customer customerData);
        void UpdateCustomer(Customer customerData);
        void DeleteCustomer(int customerId);
    }
}
