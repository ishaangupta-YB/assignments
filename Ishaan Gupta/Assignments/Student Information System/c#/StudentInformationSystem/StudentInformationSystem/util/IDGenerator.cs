using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentInformationSystem.util
{
    public static class IDGenerator
    {
        private static int studentID = 1;
        private static int courseID = 1;
        private static int teacherID = 1;
        private static int enrollmentID = 1;
        private static int paymentID = 1;

        public static int GetNextStudentID() => studentID++;
        public static int GetNextCourseID() => courseID++;
        public static int GetNextTeacherID() => teacherID++;
        public static int GetNextEnrollmentID() => enrollmentID++;
        public static int GetNextPaymentID() => paymentID++;
    }
}
