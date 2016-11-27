using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class EmployerRepository : Repository<Employer>
    {

        public Employer GetEmployerById(Guid userId)
        {
            return DbSet.Find(userId);
        }
    }
}
