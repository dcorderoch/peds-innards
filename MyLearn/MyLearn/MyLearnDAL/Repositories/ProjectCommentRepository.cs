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
        public List<ProjectComment> GetProjectComments(Guid ProjectId)
        {
            return DbSet.Where(c => c.Project.ProjectId.Equals(ProjectId)).ToList();
        }

    }
}
