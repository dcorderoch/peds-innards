using System;
using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using Course = MyLearn.Models.Course;
using JobOffer = MyLearnDAL.Models.JobOffer;
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
                student.Carnet = dalStudent.CardId;
                student.Telefono = dalStudent.PhoneNum;
                student.Fecha_Registro = dalStudent.InDate.ToLongDateString();
                student.Password = dalStudent.Password;
                student.TipoRepositorioArchivos = dalStudent.TRepo.ToString();
                var studentPhoto =  dalStudent.Photo;
                student.Foto = studentPhoto!=null ? Convert.ToBase64String(studentPhoto) : "";
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

                CourseRepository courseRepo = new CourseRepository();
                List<MyLearnDAL.Models.Course> finishedCourses = courseRepo.GetInactiveStudentCourses(studentId);

                List<FinishedCourse> finishedCoursesList = new List<FinishedCourse>();
                foreach (MyLearnDAL.Models.Course course in finishedCourses)
                {
                    FinishedCourse finishedCourse = new FinishedCourse();
                    finishedCourse.CourseDescription = course.Description;
                    finishedCourse.CourseId = course.CourseId.ToString();
                    finishedCourse.course = course.Name;
                    finishedCoursesList.Add(finishedCourse);
                }
                student.FinishedCoursesList = finishedCoursesList;
                courseRepo.Dispose();
                
                List<ActiveCourse> activeCoursesList = new List<ActiveCourse>();
                List<MyLearnDAL.Models.Course> activeCourses = courseRepo.GetActiveStudentCourses(studentId);

                foreach (MyLearnDAL.Models.Course course in activeCourses)
                {
                    ActiveCourse activeCourse = new ActiveCourse();
                    activeCourse.CourseDescription = course.Description;
                    activeCourse.CourseId = course.CourseId.ToString();
                    activeCourse.course = course.Name;
                    activeCourse.accepted = course.IsActive;
                    activeCoursesList.Add(activeCourse);
                }

                student.ActiveCoursesList = activeCoursesList;

                JobOfferRepository jobRepo = new JobOfferRepository();
                List<JobOffer> finishedJobs = jobRepo.GetStudentInactiveJobOffers(studentId);
                List<FinishedJobOffer> finishedJobOffers = new List<FinishedJobOffer>();
                foreach (JobOffer jobOffer in finishedJobs)
                {
                    FinishedJobOffer finishedJobOffer = new FinishedJobOffer();
                    finishedJobOffer.JobOffer = jobOffer.Name;
                    finishedJobOffer.JobOfferId = jobOffer.JobOfferId.ToString();
                    finishedJobOffer.Description = jobOffer.StateDescription;
                    finishedJobOffer.EmployerName = jobOffer.Employer.CompanyName;
                    finishedJobOffers.Add(finishedJobOffer);
                }

                student.FinishedJobOffersList = finishedJobOffers;
                jobRepo.Dispose();
                List<JobOffer> activeJobs = jobRepo.GetStudentActiveJobOffers(studentId);
                List<ActiveJobOffer> activeJobOffers = new List<ActiveJobOffer>();
                foreach (JobOffer jobOffer in activeJobs)
                {
                    ActiveJobOffer activeJobOffer = new ActiveJobOffer();
                    activeJobOffer.JobOffer = jobOffer.Name;
                    activeJobOffer.JobOfferId = jobOffer.JobOfferId.ToString();
                    activeJobOffer.Description = jobOffer.StateDescription;
                    activeJobOffer.EmployerName = jobOffer.Employer.CompanyName;
                    activeJobOffers.Add(activeJobOffer);
                }

                student.ActiveJobOffersList = activeJobOffers;
                student.Active = IsActive(studentId.ToString());
            }
            studentRepo.Dispose();
            
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
   
                CourseRepository courseRepo = new CourseRepository();
                List<MyLearnDAL.Models.Course> finishedCourses = courseRepo.GetProfessorInctiveCourses(professorId);

                List<FinishedCourse> finishedCoursesList = new List<FinishedCourse>();
                foreach (MyLearnDAL.Models.Course course in finishedCourses)
                {
                    FinishedCourse finishedCourse = new FinishedCourse();
                    finishedCourse.CourseDescription = course.Description;
                    finishedCourse.CourseId = course.CourseId.ToString();
                    finishedCourse.course = course.Name;
                    finishedCoursesList.Add(finishedCourse);
                }

                professor.FinishedCoursesList = finishedCoursesList;

                List<MyLearnDAL.Models.Course> activeCourses = courseRepo.GetProfessorActiveCourses(professorId);
                List<ActiveCourse> activeCoursesList = new List<ActiveCourse>();

                foreach (MyLearnDAL.Models.Course course in activeCourses)
                {
                    ActiveCourse activeCourse = new ActiveCourse();
                    activeCourse.CourseDescription = course.Description;
                    activeCourse.CourseId = course.CourseId.ToString();
                    activeCourse.course = course.Name;
                    activeCourse.accepted = course.IsActive;
                    activeCoursesList.Add(activeCourse);
                }
                professor.ActiveCoursesList = activeCoursesList;
                courseRepo.Dispose();
                professor.Active = IsActive(professorId.ToString());
            }

            professorRepo.Dispose();
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
                employer.IdEmpleador = dalEmployer.EmployerId;
                employer.NombreEmpresarial = dalEmployer.CompanyName;
                employer.EnlaceSitioWeb = dalEmployer.Website;

                JobOfferRepository jobRepo = new JobOfferRepository();
                List<JobOffer> finishedJobs = jobRepo.GetStudentInactiveJobOffers(employerId);
                List<FinishedJobOffer> finishedJobOffers = new List<FinishedJobOffer>();
                foreach (JobOffer jobOffer in finishedJobs)
                {
                    FinishedJobOffer finishedJobOffer = new FinishedJobOffer();
                    finishedJobOffer.JobOffer = jobOffer.Name;
                    finishedJobOffer.JobOfferId = jobOffer.JobOfferId.ToString();
                    finishedJobOffer.Description = jobOffer.StateDescription;
                    finishedJobOffer.EmployerName = jobOffer.Employer.CompanyName;
                    finishedJobOffers.Add(finishedJobOffer);
                }

                employer.FinishedJobOffersList = finishedJobOffers;

                List<JobOffer> activeJobs = jobRepo.GetStudentActiveJobOffers(employerId);
                List<ActiveJobOffer> activeJobOffers = new List<ActiveJobOffer>();
                foreach (JobOffer jobOffer in activeJobs)
                {
                    ActiveJobOffer activeJobOffer = new ActiveJobOffer();
                    activeJobOffer.JobOffer = jobOffer.Name;
                    activeJobOffer.JobOfferId = jobOffer.JobOfferId.ToString();
                    activeJobOffer.Description = jobOffer.StateDescription;
                    activeJobOffer.EmployerName = jobOffer.Employer.CompanyName;
                    activeJobOffers.Add(activeJobOffer);
                }

                employer.ActiveJobOffersList = activeJobOffers;
                jobRepo.Dispose();
                employer.Active = IsActive(employerId.ToString());
            }            
            employerRepo.Dispose();
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

        public ReturnCode ToggleAccount(string userId)
        {
            ReturnCode success = new ReturnCode();
            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetUserById(new Guid(userId));
            if (user != null)
            {
                if (user.IsActive == 1)
                {
                    user.IsActive = 0;
                }
                else
                {
                    user.IsActive = 1;
                }
                success.ReturnStatus = 1;
            }
            userRepo.Dispose();
            return success;
        }

        public int IsActive(string userId)
        {
            UserRepository userRepo = new UserRepository();
            User user = userRepo.GetUserById(new Guid(userId));
            int active = 1;
            if (user != null)
            {
                active = user.IsActive;
            }
            userRepo.Dispose();
            return active;
        }
    }
}
