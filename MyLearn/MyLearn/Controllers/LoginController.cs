using System.Web.Http;
using System.Web.Http.Results;
using MyLearn.Models;
using MyLearn.InputModels;
using MyLearn.BLL;
namespace MyLearn.Controllers
{
    public class LoginController : ApiController
    {
        
        /// <summary>
        /// API Method to start login, returns a user type code to signal the API consumer what method to call next
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<UserCode> Login(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.GetUserTypeCode(userCredentials.UserName,userCredentials.Password);
            if (retVal == null)
            {
                retVal = new UserCode();
                retVal.UserTypeCode = -1;
            }
            return Json(retVal);
        }

        /// <summary>
        /// API Method to return all profile information to a Student when logging in
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<InfoEstudiante> LoginStudent(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.StudentLogin(userCredentials.UserName, userCredentials.Password);
            if (retVal == null)
            {
                retVal = new InfoEstudiante();
            }
            return Json(retVal);
        }

        /// <summary>
        /// API Method to return all profile information to a Professor when logging in
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<InfoProfesor> LoginProfessor(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.ProfessorLogin(userCredentials.UserName, userCredentials.Password);
            if (retVal == null)
            {
                retVal = new InfoProfesor();
            }
            return Json(retVal);
        }

        /// <summary>
        /// API Method to return all profile information to a Employer when logging in
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<InfoEmpleador> LoginEmployer(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.EmployerLogin(userCredentials.UserName, userCredentials.Password);
            if (retVal == null)
            {
                retVal = new InfoEmpleador();
            }
            return Json(retVal);
        }

        /// <summary>
        /// API Method to return all profile information to a Administrator when logging in
        /// </summary>
        /// <param name="userCredentials"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<InfoAdmin> LoginAdmin(LoginInfo userCredentials) {
            AccountManager LoginFromBLL = new AccountManager();
            var retVal = LoginFromBLL.AdminLogin(userCredentials.UserName, userCredentials.Password);
            if (retVal == null)
            {
                retVal = new InfoAdmin();
            }
            return Json(retVal);
        }
    }
}
