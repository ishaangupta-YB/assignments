using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.interfaces
{
    public interface IPaymentRepository
    {
        Payment GetById(int paymentId);
        IEnumerable<Payment> GetAll();
        void Add(Payment payment);
        void Update(Payment payment);
        void Delete(int paymentId);
    }
}
