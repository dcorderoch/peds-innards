using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// Project repository repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
    public class ProjectCommentRepository : Repository<ProjectComment>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="context">The context is assigned by injection</param>
        public ProjectCommentRepository(MyLearnContext context) : base(context) { }

        /// <summary>
        /// Get prject comments by the project id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns>A list of project comments</returns>
        public List<ProjectComment> GetProjectCommentByProjectId(Guid projectId)
        {
            return DbSet.Where(pc => pc.ProjectId.Equals(projectId)).OrderBy(pc => pc.Date).ToList();
        }
    }
}
