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
            ReturnCode success = new ReturnCode();

            //create new comment in db


            success.ReturnStatus = 1;
            return success;
        }
    }
}