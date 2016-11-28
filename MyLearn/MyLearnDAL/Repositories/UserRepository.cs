using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class UserRepository : Repository<User>
    {

        public User GetUserById(Guid userId)
        {
            return DbSet.Find(userId);
        }
        public User GetUserByEmail(string email)
        {
            return DbSet.First(u => u.Email.Equals(email));
        }
    }
}
