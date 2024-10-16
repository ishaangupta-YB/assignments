using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using CarConnect.BusinessLayer.services;
using CarConnect.BusinessLayer.repositories;
using CarConnect.BusinessLayer.interfaces;
using CarConnect.Entity;

namespace CarConnect.Tests
{
    [TestFixture]
    public class VehicleServiceTests
    {
        private IVehicleService vehicleService;
        private Mock<IVehicleRepository> mockVehicleRepository;

        [SetUp]
        public void Setup()
        {
            mockVehicleRepository = new Mock<IVehicleRepository>();
            vehicleService = new VehicleService(mockVehicleRepository.Object);
        }

        [Test]
        public void AddVehicle_ValidData_AddsSuccessfully()
        {
            var vehicle = new Vehicle
            {
                VehicleID = 1,
                Model = "Civic",
                Make = "Honda",
                Year = 2021,
                Color = "Blue",
                RegistrationNumber = "DL01AB1234",
                Availability = true,
                DailyRate = 5000
            };

            vehicleService.AddVehicle(vehicle);

            mockVehicleRepository.Verify(r => r.Add(It.Is<Vehicle>(v => v.VehicleID == 1 && v.Make == "Honda")), Times.Once);
        }


        [Test]
        public void UpdateVehicle_ValidData_UpdatesSuccessfully()
        {
            var vehicle = new Vehicle
            {
                VehicleID = 1,
                Model = "Civic",
                Make = "Honda",
                Year = 2021,
                Color = "Blue",
                RegistrationNumber = "DL01AB1234",
                Availability = true,
                DailyRate = 5000
            };

            mockVehicleRepository.Setup(r => r.GetById(1)).Returns(vehicle);

            vehicle.Color = "White";
            vehicle.DailyRate = 7000;

            vehicleService.UpdateVehicle(vehicle);
            mockVehicleRepository.Verify(r => r.Update(It.Is<Vehicle>(v => v.Color == "White" && v.DailyRate == 7000)), Times.Once);
        }

        [Test]
        public void GetAvailableVehicles_ReturnsAvailableVehicles()
        {
            var vehicles = new List<Vehicle>
            {
                new Vehicle
                {
                    VehicleID = 1,
                    Model = "Civic",
                    Make = "Honda",
                    Year = 2021,
                    Color = "Blue",
                    RegistrationNumber = "DL01AB1234",
                    Availability = true,
                    DailyRate = 5000
                },
                new Vehicle
                {
                    VehicleID = 2,
                    Model = "Accord",
                    Make = "Honda",
                    Year = 2020,
                    Color = "Black",
                    RegistrationNumber = "DL02AB1235",
                    Availability = false,
                    DailyRate = 6000
                }
            };

            mockVehicleRepository.Setup(r => r.GetAll()).Returns(vehicles);

            var availableVehicles = vehicleService.GetAvailableVehicles();

            Assert.That(availableVehicles.All(v => v.Availability), Is.True, "All returned vehicles should be available.");
        }
         
    }
}
