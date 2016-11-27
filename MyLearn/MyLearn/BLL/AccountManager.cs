using System;
using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

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
                student.Fecha_Registro = dalStudent.InDate.ToString();
                student.Password = dalStudent.Password;
                student.TipoRepositorioArchivos = dalStudent.TRepo.ToString();
                student.Foto = dalStudent.Photo.ToString();
                student.Universidad = dalStudent.University.Name;
                student.UniversityId = dalStudent.UniversityId.ToString();
                student.EnlaceRepositorioCodigo = dalStudent.RepoLink.ToString();
                student.EnlaceACurriculum = dalStudent.ResumeLink;
                student.PromedioProyectos = dalStudent.AvgProjects;
                student.PromedioCursos = 1;
                student.Idiomas = dalStudent.StudentLanguages;
                student.CursosAprobados = dalStudent.NumSuceedCourses;
                student.CursosReprobados = dalStudent.NumFailedCourses;
                student.ProyectosExitosos = dalStudent.NumSuceedProjects;
                student.ProyectosFallidos = dalStudent.NumFailedProjects;
                student.Tecnologias = dalStudent.StudentTechnologies;

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
            ProfessorRepository studentRepo = new StudentRepository();
            Guid professorId = GetUserId(username);
            Professor dalProfessor = studentRepo.GetStudentById(studentId);

            //     DBUser dbUserInstance = new DBUser();
            //subject to change

            professor.UserId = "";
            professor.NombreContacto = "";
            professor.ApellidoContacto = "";
            professor.Ubicacion = "";
            professor.Email = "";
            professor.Telefono = "";
            professor.Fecha_Registro = "";
            professor.Password = "";
            professor.TipoRepositorioArchivos = "";
            professor.Foto = "";
            professor.IdProfesor = "";
            professor.Universidad = "";
            professor.UniversityId = "";
            professor.HorarioAtencion = "";
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
            return professor;
        }

        public InfoEmpleador EmployerLogin(string username, string password)
        {
         InfoEmpleador employer = new InfoEmpleador();
            //     DBUser dbUserInstance = new DBUser();
            //subject to change
            employer.UserId = "";
            employer.NombreContacto = "";
            employer.ApellidoContacto = "";
            employer.Ubicacion = "";
            employer.Email = "";
            employer.Telefono = "";
            employer.Fecha_Registro = "";
            employer.Password = "";
            employer.TipoRepositorioArchivos = "";
            employer.Foto = "";
            employer.IdEmpleador = "";
            employer.NombreEmpresarial = "";
            employer.EnlaceSitioWeb = "";
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

            //code goes here

            success.ReturnStatus = 1;
            return success;
        }
    }
}
