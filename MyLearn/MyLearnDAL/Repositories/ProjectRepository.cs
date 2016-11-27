using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class ProjectRepository : Repository<Project>
    {
        public Project GetProjectById(Guid projectId)
        {
            return DbSet.Find(projectId);
        }

        public List<Project> GetStudentProjects(Guid userId)
        {
            return DbSet.Where(p => p.Student.UserId.Equals(userId)).ToList();
        }
    }
}
