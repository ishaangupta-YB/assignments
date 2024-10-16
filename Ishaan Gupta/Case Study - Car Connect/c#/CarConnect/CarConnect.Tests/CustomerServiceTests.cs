using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.BusinessLayer.interfaces;
using CarConnect.BusinessLayer.services;
using CarConnect.Entity;
using CarConnect.Exceptions;
using Moq;
using NUnit.Framework;

namespace CarConnect.Tests
{
    [TestFixture]
    public class CustomerServiceTests
    {
        private ICustomerService customerService;
        private Mock<ICustomerRepository> mockCustomerRepository;
        private Customer testCustomer;

        [SetUp]
        public void Setup()
        {
            mockCustomerRepository = new Mock<ICustomerRepository>();
            customerService = new CustomerService(mockCustomerRepository.Object);

            testCustomer = new Customer
            {
                CustomerID = 1,
                FirstName = "ishaan",
                LastName = "gupta",
                Email = "ig@gmail.com",
                PhoneNumber = "1234567890",
                Address = "New Delhi",
                Username = "ishaangupta1201",
                Password = "password",
                RegistrationDate = System.DateTime.Now
            };

            mockCustomerRepository.Setup(r => r.GetById(1)).Returns(testCustomer);
        }

        [Test]
        public void UpdateCustomer_ValidData_UpdatesSuccessfully()
        {
            testCustomer.Email = "ishaangupta.new@gmail.com";
            testCustomer.PhoneNumber = "0987654321";

            customerService.UpdateCustomer(testCustomer);

            mockCustomerRepository.Verify(r => r.Update(It.Is<Customer>(c => c.Email == "ishaangupta.new@gmail.com" && c.PhoneNumber == "0987654321")), Times.Once);
        }

        [Test]
        public void UpdateCustomer_CustomerNotFound_ThrowsInvalidInputException()
        {
            var nonExistentCustomer = new Customer { CustomerID = 2 };

            mockCustomerRepository.Setup(r => r.GetById(2)).Returns((Customer)null);

            Assert.Throws<InvalidInputException>(() => customerService.UpdateCustomer(nonExistentCustomer));
        }
    }
}
