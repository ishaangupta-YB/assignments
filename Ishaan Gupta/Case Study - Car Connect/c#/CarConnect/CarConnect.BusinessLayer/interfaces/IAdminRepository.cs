using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.Entity;

namespace CarConnect.BusinessLayer.interfaces
{
    public interface IAdminRepository
    {
        void Add(Admin admin);
        void Delete(int adminId);
        IEnumerable<Admin> GetAll();
        Admin GetById(int adminId);
        Admin GetByUsername(string username);
        void Update(Admin admin);
    }
}
