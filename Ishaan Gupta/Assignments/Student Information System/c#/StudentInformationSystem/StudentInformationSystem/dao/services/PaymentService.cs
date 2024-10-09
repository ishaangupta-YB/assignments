using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;
using StudentInformationSystem.exceptions;

namespace StudentInformationSystem.dao.services
{
    public class PaymentService:IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly IStudentRepository studentRepository;

        public PaymentService(IPaymentRepository paymentRepo, IStudentRepository studentRepo)
        {
            paymentRepository = paymentRepo;
            studentRepository = studentRepo;
        }
        public Student GetStudent(int paymentId)
        {
            var payment = paymentRepository.GetById(paymentId);
            if (payment == null) throw new PaymentNotFoundException("Payment not found.");
            return studentRepository.GetById(payment.StudentID);
        }

        public decimal GetPaymentAmount(int paymentId)
        {
            var payment = paymentRepository.GetById(paymentId);
            if (payment == null) throw new PaymentNotFoundException("Payment not found.");
            return payment.Amount;
        }

        public DateTime GetPaymentDate(int paymentId)
        {
            var payment = paymentRepository.GetById(paymentId);
            if (payment == null) throw new PaymentNotFoundException("Payment not found.");
            return payment.PaymentDate;
        }
    }
}
