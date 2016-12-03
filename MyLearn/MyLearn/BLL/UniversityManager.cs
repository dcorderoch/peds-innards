using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    /// <summary>
    /// Class built in order to manage universities in MyLearn.
    /// </summary>
    public class UniversityManager
    {
        /// <summary>
        /// Obtains a list of universities that are present in the platform.
        /// </summary>
        /// <returns>List of all universities.</returns>
        public List<University> GetAllUniversities()
        {
            using (var context = new MyLearnContext())
            {
                List<University> bllUniversities = new List<University>();

                var universityRepo = new UniversityRepository(context);
                var uniList = universityRepo.GetAll();
                foreach (var uni in uniList)
                {
                    bllUniversities.Add(new University() { UniversityId = uni.UniversityId.ToString(), UniversityName = uni.Name });
                }
                universityRepo.Dispose();

                return bllUniversities;
            }
        }
    }
}