using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    /// <summary>
    /// User repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
    public class UserRepository : Repository<User>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="context">The context is assigned by injection</param>
        public UserRepository(MyLearnContext context) : base(context) { }

        /// <summary>
        /// Get an user for the given id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>An user</returns>
        public User GetUserById(Guid userId)
        {
            return DbSet.Find(userId);
        }
        /// <summary>
        /// Get an user for the given email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>An user</returns>
        public User GetUserByEmail(string email)
        {
            return DbSet.SingleOrDefault(u => u.Email.Equals(email));
        }
    }
}
