using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.dao.interfaces;
using StudentInformationSystem.entity;

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
        public IEnumerable<Payment> GetPaymentsByStudent(int studentId)
        {
            return paymentRepository.GetAll().Where(p => p.StudentID == studentId);
        }

        public void AddPayment(Payment payment)
        {
            var student = studentRepository.GetById(payment.StudentID);
            if (student == null) Console.WriteLine("Student not found");
                //throw new StudentNotFoundException("Student not found.");
            paymentRepository.Add(payment);
        }

        public void DeletePayment(int paymentId)
        {
            paymentRepository.Delete(paymentId);
        }
    }
}
