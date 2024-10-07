using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.util;

namespace StudentInformationSystem.dao.repositories
{
    public class PaymentRepository:IPaymentRepository
    {
        private readonly InMemDB db = InMemDB.Instance;

        public void Add(Payment payment)
        {
            db.Payments.Add(payment);
        }
        public void Delete(int paymentId)
        {
            var payment = GetById(paymentId);
            if (payment != null)
            {
                db.Payments.Remove(payment);
            }
        }

        public IEnumerable<Payment> GetAll()
        {
            return db.Payments;
        }

        public Payment GetById(int paymentId)
        {
            return db.Payments.FirstOrDefault(p => p.PaymentID == paymentId);
        }

        public void Update(Payment payment)
        {
            var existingPayment = GetById(payment.PaymentID);
            if (existingPayment != null)
            {
                existingPayment.StudentID = payment.StudentID;
                existingPayment.Amount = payment.Amount;
                existingPayment.PaymentDate = payment.PaymentDate;
            }
        }
    }
}
