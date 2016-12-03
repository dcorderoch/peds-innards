using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// Project repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
    public class ProjectRepository : Repository<Project>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="context">The context is assigned by injection</param>
        public ProjectRepository(MyLearnContext context) : base(context) { }

        /// <summary>
        /// Get project by its id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>A project</returns>
        public Project GetProjectById(Guid projectId)
        {
            return DbSet.Find(projectId);
        }

        /// <summary>
        /// Get project by the student and course id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="courseId"></param>
        /// <returns>A project</returns>
        public Project GetProjectByStudentAndCourseId(Guid userId, Guid courseId)
        {
            return DbSet.Where(p => p.Course.CourseId.Equals(courseId) && p.Student.UserId.Equals(userId))
                    .ToList().FirstOrDefault();
        }

        /// <summary>
        /// Get student's project by the student id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>A list of projects</returns>
        public List<Project> GetStudentProjects(Guid userId)
        {
            return DbSet.Where(p => p.Student.UserId.Equals(userId)).ToList();
        }
    }
}
