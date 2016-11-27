using System.Collections.Generic;
using MyLearn.Models;

namespace MyLearn.BLL
{
    public class TechnologyManager
    {
        public List<Technology> GetAllTechnologies()
        {
            List<Technology> allTechnologies = new List<Technology>();

            //code goes here 

            return allTechnologies;


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