using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.entity;

namespace CarConnect.dao.interfaces
{
    public interface IVehicleRepository
    {
        void Add(Vehicle vehicle);
        void Delete(int vehicleId);
        IEnumerable<Vehicle> GetAll();
        Vehicle GetById(int vehicleId);
        void Update(Vehicle vehicle);
    }

}
