using System;
using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class TechnologyManager
    {
        public List<Technology> GetAllTechnologies()
        {
            using (var context = new MyLearnContext())
            {
                var bllTechList = new List<Technology>();
                var technologyRepo = new TechnologyRepository(context);
                var techList = technologyRepo.GetAll();
                foreach (var tech in techList)
                {
                    bllTechList.Add(new Technology() { TechnologyId = tech.TecnologyId.ToString(), TechnologyName = tech.Name });
                }
                technologyRepo.Dispose();
                return bllTechList;
            }
        }


        public ReturnCode CreateTechnology(string technologyName)
        {
            using (var context = new MyLearnContext())
            {
                ReturnCode success = new ReturnCode();
                var technologyRepo = new TechnologyRepository(context);
                try
                {
                    var newTechnology = new MyLearnDAL.Models.Technology();

                    newTechnology.TecnologyId = Guid.NewGuid();
                    newTechnology.Name = technologyName;

                    technologyRepo.Add(newTechnology);
                    technologyRepo.SaveChanges();
                    success.ReturnStatus = 1;

                }
                catch (Exception)
                {  
                    success.ReturnStatus =0;
                }
                
                technologyRepo.Dispose();
                return success;
            }
        }

        public List<MyLearnDAL.Models.Technology> GetTechnologies(List<string> technologies)
        {
            using (var context = new MyLearnContext())
            {
                List<MyLearnDAL.Models.Technology> resultList = new List<MyLearnDAL.Models.Technology>();
                var technologyRepo = new TechnologyRepository(context);
                foreach (var strTechnology in technologies)
                {
                    var technology = technologyRepo.GetTechnologybyId(Guid.Parse(strTechnology));
                    if (technology != null)
                    {
                        resultList.Add(technology);
                    }
                    else
                    {
                        var wasCreated = CreateTechnology(strTechnology);
                        if (wasCreated.ReturnStatus == 1)
                        {
                            var tech = technologyRepo.GetTechnologybyId(Guid.Parse(strTechnology));
                            resultList.Add(tech);
                        }
                    }
                }
                return resultList;
            }
        }

    }
}