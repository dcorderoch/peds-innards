using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class ProjectCommentRepository : Repository<ProjectComment>
    {
        public List<ProjectComment> GetProjectCommentByProjectId(Guid projectId)
        {
            return DbSet.Where(pc => pc.ProjectId.Equals(projectId)).ToList();
        }
    }
}
