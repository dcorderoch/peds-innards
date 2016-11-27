using System;
using System.Collections.Generic;
using System.Linq;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class UniversityRepository : Repository<University>
    {

        public University GetUniversityById(Guid universityId)
        {
            return DbSet.Find(universityId);
        }
    }
}
