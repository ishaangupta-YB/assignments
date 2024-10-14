using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarConnect.dao.interfaces;
using CarConnect.entity;

namespace CarConnect.dao.services
{
    public class AdminService:IAdminService
    {
        private readonly IAdminRepository adminRepository;

        public AdminService(IAdminRepository adminRepo)
        {
            adminRepository = adminRepo;
        }

        public Admin GetAdminById(int adminId)
        {
            return adminRepository.GetById(adminId);
        }

        public Admin GetAdminByUsername(string username)
        {
            return adminRepository.GetByUsername(username);
        }

        public void RegisterAdmin(Admin admin)
        {
            adminRepository.Add(admin);
        }

        public void UpdateAdmin(Admin admin)
        {
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
