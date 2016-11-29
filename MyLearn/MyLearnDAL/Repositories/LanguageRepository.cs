﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class LanguageRepository : Repository<Language>
    {
        public Language GetLanguageById(Guid languageId)
        {
            return DbSet.Find(languageId);
        }
    }
}
