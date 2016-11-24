using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.Models;
using MyLearn.InputModels;
namespace MyLearn.Controllers
{
    public class LoginController : ApiController
    {
        //[RequireHttps]
        [HttpGet]
        public JsonResult<UserCode> Login(LoginInfo userCredentials) {
            var retVal = new UserCode();
            retVal.UserTypeCode = 1; // Subject to Change
            return Json(retVal);
        }
        //[RequireHttps]
        [HttpGet]
        public JsonResult<InfoEstudiante> LoginStudent(LoginInfo userCredentials)
        {
            var retVal = new InfoEstudiante();
            // Subject to Change
            return Json(retVal);
        }
        //[RequireHttps]
        [HttpGet]
        public JsonResult<InfoProfesor> LoginProfessor(LoginInfo userCredentials)
        {
            var retVal = new InfoProfesor();
            // Subject to Change
            return Json(retVal);
        }
        //[RequireHttps]
        [HttpGet]
        public JsonResult<InfoEmpleador> LoginEmployer(LoginInfo userCredentials)
        {
            var retVal = new InfoEmpleador();
            // Subject to Change
            return Json(retVal);
        }
        //[RequireHttps]
        [HttpGet]
        public JsonResult<InfoAdmin> LoginAdmin(LoginInfo userCredentials)
        {
            var retVal = new InfoAdmin();
            // Subject to Change
            return Json(retVal);
        }
    }
}
