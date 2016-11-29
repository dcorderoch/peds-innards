using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

namespace MyLearnDAL
{
    class MyLearnContextInitializer : DropCreateDatabaseIfModelChanges<MyLearnContext>
    {
        protected override void Seed(MyLearnContext context)
        {


        }
    }
}
