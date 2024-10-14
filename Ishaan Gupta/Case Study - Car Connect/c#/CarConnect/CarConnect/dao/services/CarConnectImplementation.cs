using System;
using System.Linq;
using CarConnect.dao.interfaces;
using CarConnect.dao.repositories;
using CarConnect.entity;
using CarConnect.exceptions;
using CarConnect.util;

namespace CarConnect.dao.services
{
    public class CarConnectImplementation: ICarConnectImplementation
    {
        private readonly ICustomerService customerService;
        private readonly IVehicleService vehicleService;
        private readonly IReservationService reservationService;
        private readonly IAdminService adminService;
        private readonly AuthenticationService authenticationService;
        private readonly ReportGenerator reportGenerator; 
        private Customer loggedInCustomer;
        private Admin loggedInAdmin;
        public CarConnectImplementation()
        {
            var customerRepository = new CustomerRepository();
            var vehicleRepository = new VehicleRepository();
            var reservationRepository = new ReservationRepository();
            var adminRepository = new AdminRepository();

            customerService = new CustomerService(customerRepository);
            vehicleService = new VehicleService(vehicleRepository);
            reservationService = new ReservationService(reservationRepository);
            adminService = new AdminService(adminRepository);   

            authenticationService = new AuthenticationService(customerService, adminService);
            reportGenerator = new ReportGenerator(reservationService, vehicleService);
        }
        public void CustomerLogin()
        {
            try
            {
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();

                Console.Write("Enter Password: ");
                string password = Console.ReadLine();
                loggedInCustomer = authenticationService.AuthenticateCustomer(username, password);

                if (loggedInCustomer == null) throw new AuthenticationException("Login failed. Invalid credentials.");
                Console.WriteLine("Customer login successful.");
                CustomerMenu();
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void AdminLogin()
        {
            try
            {
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();
                loggedInAdmin = authenticationService.AuthenticateAdmin(username, password);
                if (loggedInAdmin == null) throw new AuthenticationException("Login failed. Invalid credentials.");
                Console.WriteLine("Admin login successful."); 
                AdminMenu();
            }
            catch (AuthenticationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public void RegisterCustomer()
        {
            try
            {
                Console.Write("Enter First Name: ");
                string firstName = Console.ReadLine();
                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine();
                Console.Write("Enter Email: ");
                string email = Console.ReadLine();
                Console.Write("Enter Phone Number: ");
                string phoneNumber = Console.ReadLine();
                Console.Write("Enter Address: ");
                string address = Console.ReadLine();
                Console.Write("Enter Username: ");
                string username = Console.ReadLine();
                Console.Write("Enter Password: ");
                string password = Console.ReadLine();

                var customer = new Customer
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Address = address,
                    Username = username,
                    Password = password,
                    RegistrationDate = DateTime.Now
                };

                customerService.RegisterCustomer(customer);
                Console.WriteLine("Customer registered successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public void CustomerMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nCustomer Menu:");
                Console.WriteLine("1. Make a Reservation");
                Console.WriteLine("2. View Available Vehicles");
                Console.WriteLine("3. Logout");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        MakeReservation();
                        break;
                    case 2:
                        DisplayAvailableVehicles();
                        break;
                    case 3:
                        exit = true;
                        loggedInCustomer = null;
                        Console.WriteLine("Logging out...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
        public void AdminMenu()
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Update Vehicle");
                Console.WriteLine("3. Remove Vehicle");
                Console.WriteLine("4. Display All Data");
                Console.WriteLine("5. Generate Reports");
                Console.WriteLine("6. Logout");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();
                if (!int.TryParse(input, out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddVehicle();
                        break;
                    case 2:
                        UpdateVehicle();
                        break;
                    case 3:
                        RemoveVehicle();
                        break;
                    case 4:
                        //DisplayAllTablesData();
                        break;
                    case 5:
                        GenerateReports();
                        break;
                    case 6:
                        exit = true;
                        loggedInAdmin = null;
                        Console.WriteLine("Logging out...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }
        public void DisplayAvailableVehicles()
        {
            var vehicles = vehicleService.GetAvailableVehicles();
            Console.WriteLine("\nAvailable Vehicles:");
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"{vehicle.VehicleID} - {vehicle.Model} ({vehicle.Make}), {vehicle.Year}, Daily Rate: {vehicle.DailyRate}");
            }
        }
        public void AddVehicle()
        {
            try
            {
                Console.Write("Enter Model: ");
                string model = Console.ReadLine();
                Console.Write("Enter Make: ");
                string make = Console.ReadLine();
                Console.Write("Enter Year: ");
                int year = int.Parse(Console.ReadLine());
                Console.Write("Enter Color: ");
                string color = Console.ReadLine();
                Console.Write("Enter Registration Number: ");
                string registrationNumber = Console.ReadLine();
                Console.Write("Enter Daily Rate: ");
                decimal dailyRate = decimal.Parse(Console.ReadLine());

                var vehicle = new Vehicle
                {
                    Model = model,
                    Make = make,
                    Year = year,
                    Color = color,
                    RegistrationNumber = registrationNumber,
                    Availability = true,
                    DailyRate = dailyRate
                };

                vehicleService.AddVehicle(vehicle);
                Console.WriteLine("Vehicle added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void UpdateVehicle()
        {
            try
            {
                Console.Write("Enter Vehicle ID to update: ");
                int vehicleId = int.Parse(Console.ReadLine());

                var vehicle = vehicleService.GetVehicleById(vehicleId);
                if (vehicle == null)
                {
                    throw new VehicleNotFoundException("Vehicle not found.");
                }
                Console.Write("Enter Model (current: {0}): ", vehicle.Model);
                string model = Console.ReadLine();
                if (!string.IsNullOrEmpty(model))
                    vehicle.Model = model;

                Console.Write("Enter Make (current: {0}): ", vehicle.Make);
                string make = Console.ReadLine();
                if (!string.IsNullOrEmpty(make))
                    vehicle.Make = make;

                Console.Write("Enter Year (current: {0}): ", vehicle.Year);
                string yearInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(yearInput))
                    vehicle.Year = int.Parse(yearInput);

                Console.Write("Enter Color (current: {0}): ", vehicle.Color);
                string color = Console.ReadLine();
                if (!string.IsNullOrEmpty(color))
                    vehicle.Color = color;

                Console.Write("Enter Registration Number (current: {0}): ", vehicle.RegistrationNumber);
                string regNumber = Console.ReadLine();
                if (!string.IsNullOrEmpty(regNumber))
                    vehicle.RegistrationNumber = regNumber;

                Console.Write("Enter Daily Rate (current: {0}): ", vehicle.DailyRate);
                string rateInput = Console.ReadLine();
                if (!string.IsNullOrEmpty(rateInput))
                    vehicle.DailyRate = decimal.Parse(rateInput);

                vehicleService.UpdateVehicle(vehicle);
                Console.WriteLine("Vehicle updated successfully.");
            }
            catch (Exception ex) {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void RemoveVehicle()
        {
            try
            {
                Console.Write("Enter Vehicle ID to remove: ");
                int vehicleId = int.Parse(Console.ReadLine());

                vehicleService.RemoveVehicle(vehicleId);
                Console.WriteLine("Vehicle removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void GenerateReports() {
            try
            {
                Console.WriteLine("\n1. Reservation Report");
                Console.WriteLine("2. Vehicle Availability Report");
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();
                int choice;
                if (!int.TryParse(input, out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                    return;
                }

                switch (choice)
                {
                    case 1:
                        reportGenerator.GenerateReservationReport();
                        break;
                    case 2:
                        reportGenerator.GenerateVehicleAvailabilityReport();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void MakeReservation()
        {
            try
            {
                Console.Write("Enter Vehicle ID: ");
                int vehicleId = int.Parse(Console.ReadLine());
                Console.Write("Enter Start Date (yyyy-mm-dd): ");
                DateTime startDate = DateTime.Parse(Console.ReadLine());
                Console.Write("Enter End Date (yyyy-mm-dd): ");
                DateTime endDate = DateTime.Parse(Console.ReadLine());

                var vehicle = vehicleService.GetVehicleById(vehicleId);
                if (vehicle == null)
                {
                    throw new VehicleNotFoundException("Invalid vehicle ID.");
                }
                if (!vehicle.Availability)
                {
                    Console.WriteLine("Vehicle is not available for reservation.");
                    return;
                }
                if (endDate <= startDate)
                {
                    Console.WriteLine("End date must be after start date.");
                    return;
                }

                var reservation = new Reservation
                {
                    CustomerID = loggedInCustomer.CustomerID,
                    VehicleID = vehicle.VehicleID,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = "Confirmed"
                };

                reservation.TotalCost = reservationService.CalculateTotalCost(startDate, endDate, vehicle.DailyRate);
                reservationService.CreateReservation(reservation);
                vehicle.Availability = false;
                vehicleService.UpdateVehicle(vehicle);
                Console.WriteLine("Reservation created successfully.");
            }
            catch (VehicleNotFoundException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (ReservationException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
         
         
            //public void DisplayAllTablesData()
            //{
            //    Console.WriteLine("\nCustomers:");
            //    var customers = customerService.GetAllCustomers();
            //    foreach (var customer in customers) Console.WriteLine($"{customer.CustomerID} - {customer.FirstName} {customer.LastName}");

            //    Console.WriteLine("\nVehicles:");
            //    var vehicles = vehicleService.GetAvailableVehicles();
            //    foreach (var vehicle in vehicles) Console.WriteLine($"{vehicle.VehicleID} - {vehicle.Model} ({vehicle.Make}), {vehicle.Year}, {vehicle.DailyRate}");

            //    Console.WriteLine("\nReservations:");
            //    var reservations = reservationService.GetAll();
            //    foreach (var reservation in reservations) Console.WriteLine($"Reservation ID: {reservation.ReservationID}, Customer ID: {reservation.CustomerID}, Vehicle ID: {reservation.VehicleID}, Status: {reservation.Status}");

            //    Console.WriteLine("\nAdmins:");
            //    var admins = adminService.GetAllAdmins();
            //    foreach (var admin in admins) Console.WriteLine($"{admin.AdminID} - {admin.FirstName} {admin.LastName}, Role: {admin.Role}");
            //}
        }
    }
