using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.Models;

namespace MyLearn.Controllers
{
    public class RegisterController : ApiController
    {
        [RequireHttps]
        [HttpGet]
        public JsonResult<InfoEstudiante> RegisterStudent(RegisterEstudianteInfo userInformation)
        {
            var retVal = new InfoEstudiante();
            // Subject to Change
            return Json(retVal);
        }
        [RequireHttps]
        [HttpGet]
        public JsonResult<InfoProfesor> RegisterProfessor(RegisterProfessorInfo userInformation)
        {
            var retVal = new InfoProfesor();
            // Subject to Change
            return Json(retVal);
        }
        [RequireHttps]
        [HttpGet]
        public JsonResult<InfoEmpleador> RegisterEmployer(RegisterEmployerInfo userInformation)
        {
            var retVal = new InfoEmpleador();
            // Subject to Change
            return Json(retVal);
        }
    }
}
