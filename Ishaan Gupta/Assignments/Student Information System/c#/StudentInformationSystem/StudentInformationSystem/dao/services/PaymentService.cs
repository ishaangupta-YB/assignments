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
    // business logic for handling payment-related operations
    public class PaymentService:IPaymentService
    {
        // Repositories for interacting with the payment and student entities.
        private readonly IPaymentRepository paymentRepository;
        private readonly IStudentRepository studentRepository;

        // Constructor to init the repositories
        public PaymentService(IPaymentRepository paymentRepo, IStudentRepository studentRepo)
        {
            paymentRepository = paymentRepo;
            studentRepository = studentRepo;
        }

        // Retrieve the student who made a specific payment
        public Student GetStudent(int paymentId)
        {
            var payment = paymentRepository.GetById(paymentId);
            if (payment == null) throw new PaymentNotFoundException("Payment not found.");
            return studentRepository.GetById(payment.StudentID);
        }

        // Retrieve the amount of a specific payment
        public decimal GetPaymentAmount(int paymentId)
        {
            var payment = paymentRepository.GetById(paymentId);
            if (payment == null) throw new PaymentNotFoundException("Payment not found.");
            return payment.Amount;
        }

        // Retrieve the date of a specific payment
        public DateTime GetPaymentDate(int paymentId)
        {
            var payment = paymentRepository.GetById(paymentId);
            if (payment == null) throw new PaymentNotFoundException("Payment not found.");
            return payment.PaymentDate;
        }
    }
}
