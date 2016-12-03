using System;
using System.Collections.Generic;
using System.Linq;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// University repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
    public class UniversityRepository : Repository<University>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="context">The context is assigned by injection</param>
        public UniversityRepository(MyLearnContext context) : base(context) { }

        /// <summary>
        /// Get an university for the given id
        /// </summary>
        /// <param name="universityId"></param>
        /// <returns>An university</returns>
        public University GetUniversityById(Guid universityId)
        {
            return DbSet.Find(universityId);
        }
    }
}
