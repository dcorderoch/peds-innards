﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class RegisterController : ApiController
    {
        [HttpPost]
        public JsonResult<InfoEstudiante> RegisterStudent(RegisterEstudianteInfo userInformation)
        {
            var regMngr = new RegisterManager();
            var retVal = regMngr.StudentRegister(userInformation);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<InfoProfesor> RegisterProfessor(RegisterProfessorInfo userInformation)
        {
            var regMngr = new RegisterManager();
            var retVal = regMngr.ProfessorRegister(userInformation);
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<InfoEmpleador> RegisterEmployer(RegisterEmployerInfo userInformation)
        {
            var regMngr = new RegisterManager();
            var retVal = regMngr.EmployerRegister(userInformation);
            return Json(retVal);
        }
    }
}
