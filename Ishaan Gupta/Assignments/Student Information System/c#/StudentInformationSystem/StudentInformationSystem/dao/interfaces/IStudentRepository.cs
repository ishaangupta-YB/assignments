﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentInformationSystem.entity;

namespace StudentInformationSystem.dao.interfaces
{
    public interface IStudentRepository
    {
        void Add(Student student);
        void Update(Student student);
        void Delete(int StudentID);
        IEnumerable<Student> GetAll();
        Student GetById(int studentId); 
    }
}
