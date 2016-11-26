using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class StudentCourseRepository : Repository<StudentCourse>
    {
        public void JoinCourse(Course course, Student student)
        {
            DbSet.Add(new StudentCourse()
            {
                Course = course,
                CourseId = course.CourseId,
                Student = student,
                UserId = student.UserId
            });
        }
    }
}
