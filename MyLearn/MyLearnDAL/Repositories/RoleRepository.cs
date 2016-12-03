using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// Role repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
    public class RoleRepository : Repository<Role>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="context">The context is assigned by injection</param>
        public RoleRepository(MyLearnContext context) : base(context) { }
    }
}
