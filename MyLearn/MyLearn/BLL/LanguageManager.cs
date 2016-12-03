using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    /// <summary>
    /// Class in charge of handling languages within the application.
    /// </summary>
    public class LanguageManager
    {
        /// <summary>
        /// Method that obtains all languages present in MyLearn.
        /// </summary>
        /// <returns>List of languages.</returns>
        public List<Language> GetAllLanguages()
        {
            using (var context = new MyLearnContext())
            {
                List<Language> bllLanguages = new List<Language>();

                var languageRepo = new LanguageRepository(context);
                var langList = languageRepo.GetAll();
                foreach (var lang in langList)
                {
                    bllLanguages.Add(new Language() { LanguageId = lang.LenguageId.ToString(), LanguageName = lang.Name });
                }
                languageRepo.Dispose();

                return bllLanguages;
            }
        }
    }
}