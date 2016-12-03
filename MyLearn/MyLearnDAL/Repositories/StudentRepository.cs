using System;
using System.Collections.Generic;
using System.Linq;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// Student repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
    public class StudentRepository : Repository<Student>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="context">The context is assigned by injection</param>
        public StudentRepository(MyLearnContext context) : base(context) { }

        /// <summary>
        /// Get a student by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A student</returns>
        public Student GetStudentById(Guid userId)
        {
            return DbSet.Find(userId);
        }

        /// <summary>
        /// Get course's students by the course id
        /// </summary>
        /// <param name="CourseId"></param>
        /// <returns></returns>
        public List<Student> GetCourseStudents(Guid CourseId)
        {
            return DbSet.Where(s => s.Courses.Any(c => c.CourseId.Equals(CourseId))).ToList();
        }
    }
}
