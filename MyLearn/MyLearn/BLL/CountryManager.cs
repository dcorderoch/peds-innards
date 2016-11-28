using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class CountryManager
    {
        public List<Country> GetAllCountries()
        {
            CountryRepository countryRepo = new CountryRepository();
            List <MyLearnDAL.Models.Country> listOfCountries= countryRepo.GetAll();
            List<Country> retCountries = new List<Country>();
            foreach (MyLearnDAL.Models.Country dalCountry in listOfCountries)
            {
                Country country = new Country();
                country.CountryId = dalCountry.CountryId.ToString();
                country.CountryName = dalCountry.Name;
                retCountries.Add(country);    
            }
            countryRepo.Dispose();
            return retCountries;
        }
    }
}