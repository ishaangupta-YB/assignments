using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.interfaces
{
    public interface IPaymentService
    {
        IEnumerable<Payment> GetPaymentsByStudent(int studentId);
        void AddPayment(Payment payment);
        void DeletePayment(int paymentId);
    }
}
