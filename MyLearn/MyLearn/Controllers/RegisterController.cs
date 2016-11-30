using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;

namespace MyLearn.Controllers
{
    public class RegisterController : ApiController
    {
        [HttpPost]
        public JsonResult<InfoEstudiante> RegisterStudent(RegisterEstudianteInfo userInformation)
        {
            var regMngr = new RegisterManager();
            var retVal = regMngr.StudentRegister(userInformation);
            if (retVal == null)
            {
                retVal = new InfoEstudiante();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<InfoProfesor> RegisterProfessor(RegisterProfessorInfo userInformation)
        {
            var regMngr = new RegisterManager();
            var retVal = regMngr.ProfessorRegister(userInformation);
            if (retVal == null)
            {
                retVal = new InfoProfesor();
            }
            return Json(retVal);
        }
        [HttpPost]
        public JsonResult<InfoEmpleador> RegisterEmployer(RegisterEmployerInfo userInformation)
        {
            var regMngr = new RegisterManager();
            var retVal = regMngr.EmployerRegister(userInformation);
            if (retVal == null)
            {
                retVal = new InfoEmpleador();
            }
            return Json(retVal);
        }
    }
}
