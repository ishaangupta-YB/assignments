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
    public class EnrollmentRepository:IEnrollmentRepository
    {
        private readonly InMemDB db = InMemDB.Instance;

        public void Add(Enrollment enrollment)
        {
            db.Enrollments.Add(enrollment);
        }

        public void Delete(int enrollmentId)
        {
            var enrollment = GetById(enrollmentId);
            if (enrollment != null)
            {
                db.Enrollments.Remove(enrollment);
            }
        }

        public IEnumerable<Enrollment> GetAll()
        {
            return db.Enrollments;
        }

        public Enrollment GetById(int enrollmentId)
        {
            return db.Enrollments.FirstOrDefault(e => e.EnrollmentID == enrollmentId);
        }

        public void Update(Enrollment enrollment)
        {
            var existingEnrollment = GetById(enrollment.EnrollmentID);
            if (existingEnrollment != null)
            {
                existingEnrollment.StudentID = enrollment.StudentID;
                existingEnrollment.CourseID = enrollment.CourseID;
                existingEnrollment.EnrollmentDate = enrollment.EnrollmentDate;
            }
        }
    }
}
