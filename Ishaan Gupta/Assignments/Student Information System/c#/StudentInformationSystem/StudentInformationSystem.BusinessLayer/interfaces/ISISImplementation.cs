using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.BusinessLayer.interfaces
{
    public interface ISISImplementation
    {
        void EnrollStudentInCourse();
        void AssignTeacherToCourse();
        void MakePayment();
        void GenerateEnrollmentReport();
        void GeneratePaymentReport();
        void CalculateCourseStatistics();
    }

}
