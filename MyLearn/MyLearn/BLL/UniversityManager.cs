﻿using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    public class UniversityManager
    {
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