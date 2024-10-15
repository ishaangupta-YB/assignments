# Car Connect

Full Working Project with all tasks done.

### SQL Queries to test on mock data

```sql
-- Inserting sample customers
INSERT INTO Customers (FirstName, LastName, Email, PhoneNumber, Address, Username, Password, RegistrationDate)
VALUES 
('Rajesh', 'Sharma', 'rajesh.sharma@gmail.com', '9876543210', '123 MG Road, Mumbai', 'rajesh123', 'password123', '2024-01-15'),
('Priya', 'Verma', 'priya.verma@gmail.com', '9876543211', '456 Ring Road, Delhi', 'priya456', 'password456', '2024-01-16'),
('Amit', 'Kumar', 'amit.kumar@gmail.com', '9876543212', '789 Residency Road, Bengaluru', 'amit789', 'password789', '2024-01-17'),
('Sneha', 'Patil', 'sneha.patil@gmail.com', '9876543213', '101 Residency Road, Pune', 'sneha101', 'password101', '2024-01-18'),
('Vikas', 'Singh', 'vikas.singh@gmail.com', '9876543214', '202 Residency Road, Chennai', 'vikas202', 'password202', '2024-01-19');

-- Inserting sample vehicles
INSERT INTO Vehicles (Model, Make, Year, Color, RegistrationNumber, Availability, DailyRate)
VALUES 
('Honda City', 'Honda', 2020, 'Red', 'MH01AB1234', 1, 3000),
('Hyundai Creta', 'Hyundai', 2021, 'White', 'DL01CD5678', 1, 3500),
('Maruti Swift', 'Maruti', 2019, 'Black', 'KA01EF9012', 1, 2500),
('Tata Harrier', 'Tata', 2022, 'Blue', 'TN01GH3456', 1, 4000),
('Mahindra XUV500', 'Mahindra', 2020, 'Silver', 'AP01IJ7890', 1, 4500);

-- Inserting sample reservations
INSERT INTO Reservations (CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
VALUES 
(1, 1, '2024-02-01', '2024-02-05', 15000, 'Confirmed'),
(2, 3, '2024-02-10', '2024-02-15', 12500, 'Confirmed'),
(3, 4, '2024-02-05', '2024-02-10', 20000, 'Confirmed'),
(4, 2, '2024-02-12', '2024-02-18', 21000, 'Confirmed'),
(5, 5, '2024-02-20', '2024-02-25', 22500, 'Confirmed');

-- Inserting sample admins
INSERT INTO Admins (FirstName, LastName, Email, PhoneNumber, Username, Password, Role, JoinDate)
VALUES 
('Rohit', 'Gupta', 'rohit.gupta@gmail.com', '9876543220', 'rohitadmin', 'adminpass123', 'Manager', '2024-01-01'),
('Kavita', 'Joshi', 'kavita.joshi@gmail.com', '9876543221', 'kavitaadmin', 'adminpass456', 'Supervisor', '2024-01-02');
```


