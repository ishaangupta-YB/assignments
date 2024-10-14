using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.dao.interfaces;
using CarConnect.entity;

namespace CarConnect.dao.services
{
    public class VehicleService:IVehicleService
    {
        private readonly IVehicleRepository vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepo)
        {
            vehicleRepository = vehicleRepo;
        }
        // getting vehicle by id
        public Vehicle GetVehicleById(int vehicleId)
        {
            return vehicleRepository.GetById(vehicleId);
        }
        // getting vehicle by availability
        public IEnumerable<Vehicle> GetAvailableVehicles()
        {
            return vehicleRepository.GetAll().Where(v => v.Availability);
        }
        // add vehicle
        public void AddVehicle(Vehicle vehicle)
        {
            vehicleRepository.Add(vehicle);
        }
        // update vehicle
        public void UpdateVehicle(Vehicle vehicle)
        {
            vehicleRepository.Update(vehicle);
        }
        // remove vehicle
        public void RemoveVehicle(int vehicleId)
        {
            vehicleRepository.Delete(vehicleId);
        }
    }
}
