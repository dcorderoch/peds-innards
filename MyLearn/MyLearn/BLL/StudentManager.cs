﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.BLL
{
    public class StudentManager
    {
        public StudentProfileAsEmployer GetProfile(StudentIdentifier studentId)
        {
            var student = new StudentProfileAsEmployer();
            // code goes here
            return student;
        }
    }
}