using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.BusinessLayer.interfaces;
using CarConnect.Exceptions;
using CarConnect.Entity;
using CarConnect.Util;

namespace CarConnect.BusinessLayer.services
{
    public class AuthenticationService
    {
        private readonly ICustomerService customerService;
        private readonly IAdminService adminService;

        public AuthenticationService(ICustomerService customerService, IAdminService adminService)
        {
            this.customerService = customerService;
            this.adminService = adminService;
        }
        public Customer AuthenticateCustomer(string username, string password)
        {
            try
            {
                var customer = customerService.GetCustomerByUsername(username);
                if (customer == null || customer.Password != password)
                    throw new AuthenticationException("Invalid username or password.");
                return customer;
            }
            catch (CustomerNotFoundException)
            {
                throw new AuthenticationException("Invalid username or password.");
            } 
        }

        public Admin AuthenticateAdmin(string username, string password)
        {
            try
            {
                var admin = adminService.GetAdminByUsername(username);
                if (admin == null || admin.Password != password)
                    throw new AuthenticationException("Invalid username or password.");
                return admin;
            }
            catch (AdminNotFoundException)
            {
                throw new AuthenticationException("Invalid username or password.");
            }
        }
    }
}
