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
        Student GetStudent(int studentId);
        decimal GetPaymentAmount(int paymentId);
        DateTime GetPaymentDate(int paymentId);
    }
}
