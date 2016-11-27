﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;

namespace MyLearn.Controllers
{
    public class LanguageController : ApiController
    {
        [HttpGet]
        public JsonResult<List<Language>> GetAll()
        {
            var langMngr = new LanguageManager();
            var retVal = langMngr.GetAllLanguages();
            return Json(retVal);
        }
    }
}