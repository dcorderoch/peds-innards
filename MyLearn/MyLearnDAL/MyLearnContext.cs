using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using MyLearnDAL.Models;

namespace MyLearnDAL
{
    using System.Data.Entity;

    public class MyLearnContext : DbContext
    {

        public MyLearnContext()
            : base("name=MyLearnContext")
        {
        }

        // Add a DbSet 



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }


}