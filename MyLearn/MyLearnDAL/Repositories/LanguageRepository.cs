using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

namespace MyLearnDAL.Repositories
{ 

    /// <summary>
    /// Language repository, it provides CRUD and more complex operations to be called by the upper layer.
    /// </summary>
public class LanguageRepository : Repository<Language>
    {
    /// <summary>
    /// Class constructor
    /// </summary>
    /// <param name="context">The context is assigned by injection</param>
    public LanguageRepository(MyLearnContext context) : base(context) { }
    }
}
