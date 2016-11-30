using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class ProfessorRepository : Repository<Professor>
    {
        public ProfessorRepository(MyLearnContext context) : base(context) { }

        public Professor GetProfessorById(Guid userId)
        {
            return DbSet.Find(userId);
        }
    }
}
