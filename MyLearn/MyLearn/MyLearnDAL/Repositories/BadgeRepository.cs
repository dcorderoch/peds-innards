﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class BadgeRepository : Repository<Badge>
    {
        public List<Badge> GetProjectBadges(Guid ProjectId)
        {
            return DbSet.Where(b => b.Project.ProjectId.Equals(ProjectId)).ToList();
        }
    }
}