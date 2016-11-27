﻿using System;
using System.Collections.Generic;
using System.Linq;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class StudentRepository : Repository<Student>
    {
        public List<Student> GetCourseStudents(Guid CourseId)
        {
            return DbSet.Where(s => s.StudentCourses.Any(c => c.CourseId.Equals(CourseId))).ToList();
        }
    }
}