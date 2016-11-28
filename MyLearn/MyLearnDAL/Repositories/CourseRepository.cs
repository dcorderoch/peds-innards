using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    class CourseRepository : Repository<Course>
    {

        public Course GetCoursebyId(Guid courseId)
        {
            return DbSet.Find(courseId);
        }

        public List<Course> GetUniversityCourses(Guid UniversityId)
        {
            return DbSet.Where(c => c.University.UniversityId.Equals(UniversityId)).ToList();
        }

        public List<Course> GetProfessorCourses(Guid UserId)
        {
            return DbSet.Where(c => c.Professor.UserId.Equals(UserId)).ToList();
        }

        public List<Course> GetStudentCourses(Guid UserId)
        {
            return DbSet.Where(c => c.Students.Any(s => s.UserId.Equals(UserId))).ToList();
        }

        public List<Course> GetActiveStudentCourses(Guid UserId)
        {
            return DbSet.Where(c => c.Students.Any(s => s.UserId.Equals(UserId)) && c.IsActive.Equals(1)).ToList();
        }

        public List<Course> GetInactiveStudentCourses(Guid UserId)
        {
            return DbSet.Where(c => c.Students.Any(s => s.UserId.Equals(UserId)) && c.IsActive.Equals(0)).ToList();
        }
    }
}
