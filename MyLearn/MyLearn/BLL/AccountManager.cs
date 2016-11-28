using System;
using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using Language = MyLearnDAL.Models.Language;

namespace MyLearn.BLL
{
    /// <summary>
    /// Class whose intention is to validate a user's log in.
    /// </summary>
    public class AccountManager
    {
        public UserCode GetUserTypeCode(string username, string password)
        {
            //Admin:0 Student:1 Professor:2 Employer:3 Error: -1
            UserCode userTypeCode = new UserCode();
            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetUserByEmail(username);
            if (user != null && user.Password == password)
            {
                int code = user.RoleId; 
                userRepo.Dispose();
                userTypeCode.UserTypeCode = code;    
            }
            
            return userTypeCode;
        }

        private Guid GetUserId(string username)
        {
            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetUserByEmail(username);
            return user.UserId;
        }
        /// <summary>
        /// Method that returns to the REST API the corresponding user's information if and only if the parameters given match a user in the DB.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>User information.</returns>
        public InfoEstudiante StudentLogin(string username, string password)
        {
            InfoEstudiante student = new InfoEstudiante();
            StudentRepository studentRepo = new StudentRepository();
            Guid studentId = GetUserId(username);
            Student dalStudent = studentRepo.GetStudentById(studentId); 
            
            if (dalStudent != null && dalStudent.Password == password)
            {
                student.UserId = studentId.ToString();
                student.NombreContacto = dalStudent.Name;
                student.ApellidoContacto = dalStudent.LastName;
                student.Ubicacion = dalStudent.Country.ToString();
                student.Email = dalStudent.Email;
                student.Telefono = dalStudent.PhoneNum;
                student.Fecha_Registro = dalStudent.InDate.ToLongDateString();
                student.Password = dalStudent.Password;
                student.TipoRepositorioArchivos = dalStudent.TRepo.ToString();
                student.Foto = dalStudent.Photo.ToString();
                student.Universidad = dalStudent.University.Name;
                student.UniversityId = dalStudent.UniversityId.ToString();
                student.EnlaceRepositorioCodigo = dalStudent.RepoLink;
                student.EnlaceACurriculum = dalStudent.ResumeLink;
                student.PromedioProyectos = (float) dalStudent.AvgProjects;
                student.PromedioCursos = (float) dalStudent.AvgCourses;
                
                List<string> languageList = new List<string>();
                foreach (Language language in dalStudent.Languages)
                {
                    languageList.Add(language.Name);
                }
                student.Idiomas = languageList;
                student.CursosAprobados = dalStudent.NumSuceedCourses;
                student.CursosReprobados = dalStudent.NumFailedCourses;
                student.ProyectosExitosos = dalStudent.NumSuceedProjects;
                student.ProyectosFallidos = dalStudent.NumFailedProjects;

                List<string> technologyList = new List<string>();
                foreach (MyLearnDAL.Models.Technology technology in dalStudent.Technologies)
                {
                    technologyList.Add(technology.Name);
                }
                student.Tecnologias = technologyList;

                List<FinishedCourse> finishedCoursesList = new List<FinishedCourse>();
                FinishedCourse finishedCourse = new FinishedCourse();
                finishedCourse.CourseDescription = "";
                finishedCourse.CourseId = "";
                finishedCourse.course = "";
                student.FinishedCoursesList = finishedCoursesList;

                List<ActiveCourse> activeCoursesList = new List<ActiveCourse>();
                ActiveCourse activeCourses = new ActiveCourse();
                activeCourses.course = "";
                activeCourses.CourseId = "";
                activeCourses.CourseDescription = "";
                student.ActiveCoursesList = activeCoursesList;
                // THERE ARE MISSING FIELDS FOR THESE MODELS, ALSO, IT'S A LIST
                List<FinishedJobOffer> finishedJobOffersLists = new List<FinishedJobOffer>();
                FinishedJobOffer finishedJobOffers = new FinishedJobOffer();
                finishedJobOffers.JobOffer = "";
                finishedJobOffers.JobOfferId = "";
                student.FinishedJobOffersList = finishedJobOffersLists;

                List<ActiveJobOffer> activeJobOffersLists = new List<ActiveJobOffer>();
                List<ActiveJobOffer> activeJobOffers = new List<ActiveJobOffer>();
                //activeJobOffers.JobOffer = "";
                //activeJobOffers.JobOfferId = "";
                student.ActiveJobOffersList = activeJobOffersLists;
            }
            return student;
        }

        public InfoProfesor ProfessorLogin(string username, string password)
        {
            InfoProfesor professor = new InfoProfesor();
            ProfessorRepository professorRepo = new ProfessorRepository();
            Guid professorId = GetUserId(username);
            Professor dalProfessor = professorRepo.GetProfessorById(professorId);

            if (dalProfessor != null && dalProfessor.Password == password)
            {
                
                professor.UserId = dalProfessor.UserId.ToString();
                professor.NombreContacto = dalProfessor.Name;
                professor.ApellidoContacto = dalProfessor.Lastname;
                professor.Ubicacion = dalProfessor.Country.Name;
                professor.Email = dalProfessor.Email;
                professor.Telefono = dalProfessor.PhoneNum;
                professor.Fecha_Registro = dalProfessor.InDate.ToString();
                professor.Password = dalProfessor.Password;
                professor.TipoRepositorioArchivos = dalProfessor.TRepo.ToString();
                professor.Foto = dalProfessor.Photo.ToString();
                professor.Universidad = dalProfessor.University.Name;
                professor.UniversityId = dalProfessor.UniversityId.ToString();
                professor.HorarioAtencion = dalProfessor.Schedule;
                // THERE ARE MISSING FIELDS FOR THESE MODELS, ALSO, IT'S A LIST
                List<FinishedCourse> finishedCoursesList = new List<FinishedCourse>();
                FinishedCourse finishedCourse = new FinishedCourse();
                finishedCourse.CourseDescription = "";
                finishedCourse.CourseId = "";
                finishedCourse.course = "";
                professor.FinishedCoursesList = finishedCoursesList;

                List<ActiveCourse> activeCoursesList = new List<ActiveCourse>();
                ActiveCourse activeCourses = new ActiveCourse();
                activeCourses.course = "";
                activeCourses.CourseId = "";
                activeCourses.CourseDescription = "";
                professor.ActiveCoursesList = activeCoursesList;
            }

            
            return professor;
        }

        public InfoEmpleador EmployerLogin(string username, string password)
        {
         InfoEmpleador employer = new InfoEmpleador();
         EmployerRepository employerRepo = new EmployerRepository();
         Guid employerId = GetUserId(username);
         Employer dalEmployer = employerRepo.GetEmployerById(employerId);

            if (dalEmployer != null && dalEmployer.Password == password)
            {
                employer.UserId = dalEmployer.UserId.ToString();
                employer.NombreContacto = dalEmployer.ContactName;
                employer.ApellidoContacto = dalEmployer.ContactLastname;
                employer.Ubicacion = dalEmployer.Country.ToString();
                employer.Email = dalEmployer.Email;
                employer.Telefono = dalEmployer.PhoneNum;
                employer.Fecha_Registro = dalEmployer.InDate.ToString();
                employer.Password = dalEmployer.Password;
                employer.TipoRepositorioArchivos = dalEmployer.TRepo.ToString();
                employer.Foto = dalEmployer.Photo.ToString();
                employer.IdEmpleador = dalEmployer.id;
                employer.NombreEmpresarial = dalEmployer.CompanyName;
                employer.EnlaceSitioWeb = dalEmployer.Website;
                // THERE ARE MISSING FIELDS FOR THESE MODELS, ALSO, IT'S A LIST
                List<FinishedJobOffer> finishedJobOffersList = new List<FinishedJobOffer>();
                //FinishedJobOffer finishedJobOffers = new FinishedJobOffer();
                //finishedJobOffers.JobOffer = "";
                //finishedJobOffers.JobOfferId = "";
                employer.FinishedJobOffersList = finishedJobOffersList;

                List<ActiveJobOffer> activeJobOffersList = new List<ActiveJobOffer>();
                //ActiveJobOffersList activeJobOffers = new ActiveJobOffersList();
                //activeJobOffers.JobOffer = "";
                //activeJobOffers.JobOfferId = "";
                employer.ActiveJobOffersList = activeJobOffersList;
            }            

            return employer;      
        }

        public InfoAdmin AdminLogin(string username, string password)
        {
            InfoAdmin admin = new InfoAdmin();
            //subject to change

            admin.UserId = "";
            admin.UserName="";
            admin.Tecnologias = new List<string>();
            admin.Universidades = new List<string>();

            return admin;
        }

        public ReturnCode DisableAccount(string userId)
        {
            ReturnCode success = new ReturnCode();
            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetUserById(new Guid(userId));
            if (user != null)
            {
                user.IsActive = 0;
                success.ReturnStatus = 1;
            }
            return success;
        }
    }
}
