using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.dao.interfaces;
using CarConnect.entity;
using CarConnect.util;
using CarConnect.exceptions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace CarConnect.Tests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private AuthenticationService authenticationService;
        private Mock<ICustomerService> mockCustomerService;
        private Mock<IAdminService> mockAdminService;

        [SetUp]
        public void Setup()
        {
            mockCustomerService = new Mock<ICustomerService>();
            mockAdminService = new Mock<IAdminService>();

            authenticationService = new AuthenticationService(mockCustomerService.Object, mockAdminService.Object);

            var testCustomer = new Customer
            {
                CustomerID = 1,
                Username = "ishaang",
                Password = "ishaangpass"
            };

            mockCustomerService.Setup(s => s.GetCustomerByUsername("ishaang")).Returns(testCustomer);
        }

        [Test]
        public void AuthenticateCustomer_InvalidCredentials_ThrowsAuthenticationException()
        {
            string username = "ishaang";
            string password = "wrongpassword";

            Assert.Throws<AuthenticationException>(() =>
            {
                authenticationService.AuthenticateCustomer(username, password);
            });
        }

        [Test]
        public void AuthenticateCustomer_ValidCredentials_ReturnsCustomer()
        {
            string username = "ishaang";
            string password = "ishaangpass";

            var customer = authenticationService.AuthenticateCustomer(username, password);

            Assert.That(customer, Is.Not.Null);
            Assert.That(customer.CustomerID, Is.EqualTo(1));
            Assert.That(customer.Username, Is.EqualTo("ishaang"));
        }
    }
}
