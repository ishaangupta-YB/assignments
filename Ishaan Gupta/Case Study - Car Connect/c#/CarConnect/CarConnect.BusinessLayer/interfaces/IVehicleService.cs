using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;

namespace CarConnect.BusinessLayer.interfaces
{
    public interface IVehicleService
    {
        Vehicle GetVehicleById(int vehicleId);
        IEnumerable<Vehicle> GetAvailableVehicles();
        void AddVehicle(Vehicle vehicleData);
        void UpdateVehicle(Vehicle vehicleData);
        void RemoveVehicle(int vehicleId);
    }
}
