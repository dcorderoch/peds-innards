using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLearnDAL.Models;

namespace MyLearnDAL.Repositories
{
    public class CountryRepository : Repository<Country>
    {

        public Country GetCountryById(Guid countryId)
        {
            return DbSet.Find(countryId);
        }
    }
}
