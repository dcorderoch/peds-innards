using System;
using System.Collections.Generic;
using System.Linq;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class StudentRepository : Repository<Student>
    {
        public StudentRepository(MyLearnContext context) : base(context) { }

        public Student GetStudentById(Guid userId)
        {
            return DbSet.Find(userId);
        }
        public List<Student> GetCourseStudents(Guid CourseId)
        {
            return DbSet.Where(s => s.Courses.Any(c => c.CourseId.Equals(CourseId))).ToList();
        }
    }
}
