using System;
using System.Collections.Generic;
using System.Linq;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class UniversityRepository : Repository<University>
    {
        public UniversityRepository(MyLearnContext context) : base(context) { }

        public University GetUniversityById(Guid universityId)
        {
            return DbSet.Find(universityId);
        }
    }
}
