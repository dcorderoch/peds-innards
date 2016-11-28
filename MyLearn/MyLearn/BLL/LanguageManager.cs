using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class LanguageManager
    {
        public List<Language> GetAllLanguages()
        {
            List<Language> bllLanguages = new List<Language>();

            var languageRepo = new LanguageRepository();
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