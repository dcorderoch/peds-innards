using System;
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
            var retVal = new InfoEstudiante();
            // Subject to Change
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<InfoProfesor> RegisterProfessor(RegisterProfessorInfo userInformation)
        {
            var retVal = new InfoProfesor();
            // Subject to Change
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<InfoEmpleador> RegisterEmployer(RegisterEmployerInfo userInformation)
        {
            var retVal = new InfoEmpleador();
            // Subject to Change
            return Json(retVal);
        }
    }
}
