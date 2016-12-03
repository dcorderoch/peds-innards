using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.BLL;
using MyLearn.Models;
using MyLearn.InputModels;

namespace MyLearn.Controllers
{
    public class RegisterController : ApiController
    {
        private string GoogleAuthURI = GoogleService.Constants._MyLearnAuthURL;
        /// <summary>
        /// API Method to register a new Student to de the MyLearn Database
        /// </summary>
        /// <param name="userInformation"></param>
        /// <returns></returns>
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
        /// <summary>
        /// API Method to register a new Professor to de the MyLearn Database
        /// </summary>
        /// <param name="userInformation"></param>
        /// <returns></returns>
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
        /// <summary>
        /// /// API Method to register a new Employer to de the MyLearn Database
        /// </summary>
        /// <param name="userInformation"></param>
        /// <returns></returns>
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
