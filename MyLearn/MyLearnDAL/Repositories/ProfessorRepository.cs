using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// Professor repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
    public class ProfessorRepository : Repository<Professor>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="context">The context is assigned by injection</param>
        public ProfessorRepository(MyLearnContext context) : base(context) { }

        /// <summary>
        /// Get a professor by its id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>a proffesor </returns>
        public Professor GetProfessorById(Guid userId)
        {
            return DbSet.Find(userId);
        }
    }
}
