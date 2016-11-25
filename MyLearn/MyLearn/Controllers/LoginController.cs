using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.Models;
using MyLearn.InputModels;
using MyLearn.BLL;
namespace MyLearn.Controllers
{
    public class LoginController : ApiController
    {
        //[RequireHttps]
        [HttpPost]
        public JsonResult<UserCode> Login(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.GetUserTypeCode(userCredentials.UserName,userCredentials.Password);
            return Json(retVal);
        }
        //[RequireHttps]
        [HttpPost]
        public JsonResult<InfoEstudiante> LoginStudent(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.StudentLogin(userCredentials.UserName, userCredentials.Password);
            return Json(retVal);
        }
        //[RequireHttps]
        [HttpPost]
        public JsonResult<InfoProfesor> LoginProfessor(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.ProfessorLogin(userCredentials.UserName, userCredentials.Password);
            return Json(retVal);
        }
        //[RequireHttps]
        [HttpPost]
        public JsonResult<InfoEmpleador> LoginEmployer(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.EmployerLogin(userCredentials.UserName, userCredentials.Password);
            return Json(retVal);
        }
        //[RequireHttps]
        [HttpPost]
        public JsonResult<InfoAdmin> LoginAdmin(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.AdminLogin(userCredentials.UserName, userCredentials.Password);
            return Json(retVal);
        }
    }
}
