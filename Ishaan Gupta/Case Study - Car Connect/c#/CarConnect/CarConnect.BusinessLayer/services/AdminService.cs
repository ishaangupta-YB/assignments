using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.BusinessLayer.interfaces;
using CarConnect.BusinessLayer.repositories;
using CarConnect.Entity;
using CarConnect.Exceptions;

namespace CarConnect.BusinessLayer.services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepo)
        {
            adminRepository = adminRepo;
        }

        public Admin GetAdminById(int adminId)
        {
            var admin = adminRepository.GetById(adminId);
            if (admin == null) throw new AdminNotFoundException("admin not found");
            return admin;
        }

        public Admin GetAdminByUsername(string username)
        {
            var admin = adminRepository.GetByUsername(username);
            if (admin == null) throw new AdminNotFoundException("admin not found");
            return admin; 
        }

        public void RegisterAdmin(Admin admin)
        {
            adminRepository.Add(admin);
        }

        public void UpdateAdmin(Admin admin)
        {
            var a = adminRepository.GetById(admin.AdminID);
            if (a == null) throw new AdminNotFoundException("admin not found");
            adminRepository.Update(admin);
        }

        public void DeleteAdmin(int adminId)
        {
            adminRepository.Delete(adminId);
        }

        public IEnumerable<Admin> GetAllAdmins()
        {
            return adminRepository.GetAll();
        }
    }
}
