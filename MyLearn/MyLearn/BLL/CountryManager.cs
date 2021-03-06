﻿using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    /// <summary>
    /// Class built in order to manage the countries of the system.
    /// </summary>
    public class CountryManager
    {
        /// <summary>
        /// Method that obtains all countries.
        /// </summary>
        /// <returns>List of countries in the application.</returns>
        public List<Country> GetAllCountries()
        {
            using (var context = new MyLearnContext())
            {
                CountryRepository countryRepo = new CountryRepository(context);
                List<MyLearnDAL.Models.Country> listOfCountries = countryRepo.GetAll();
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
}