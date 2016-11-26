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
            return DbSet.Where(c => c.StudentCourses.Any(s => s.UserId.Equals(UserId))).ToList();
        }
    }
}
