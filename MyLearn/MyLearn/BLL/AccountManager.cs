using System;
using System.Collections.Generic;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
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
            using (var context = new MyLearnContext())
            {
                //Admin:0 Student:1 Professor:2 Employer:3 Error: -1
                UserCode userTypeCode = new UserCode();
                var userRepo = new UserRepository(context);
                var user = userRepo.GetUserByEmail(username);
                if (user != null && user.Password == password)
                {
                    var code = user.RoleId;
                    userTypeCode.UserTypeCode = code;
                }
                else
                {
                    userTypeCode.UserTypeCode = -1;
                }
                userRepo.Dispose();
                return userTypeCode;
            }
        }

        private Guid GetUserId(string username)
        {
            using (var context = new MyLearnContext())
            {
                var userRepo = new UserRepository(context);
                var user = userRepo.GetUserByEmail(username);
                userRepo.Dispose();
                return user.UserId;
            }
        }
        /// <summary>
        /// Method that returns to the REST API the corresponding user's information if and only if the parameters given match a user in the DB.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>User information.</returns>
        public InfoEstudiante StudentLogin(string username, string password)
        {
            using (var context = new MyLearnContext())
            {
                var student = new InfoEstudiante();
                var studentRepo = new StudentRepository(context);
                var projRepo = new ProjectRepository(context);
                var studentId = GetUserId(username);
                var dalStudent = studentRepo.GetStudentById(studentId);
                var allprojects = projRepo.GetAll();

                if (dalStudent != null && dalStudent.Password == password)
                {
                    student.RefreshToken = dalStudent.RefreshToken;
                    student.UserId = studentId.ToString();
                    student.NombreContacto = dalStudent.Name;
                    student.ApellidoContacto = dalStudent.LastName;
                    student.Ubicacion = dalStudent.Country.Name;
                    student.Email = dalStudent.Email;
                    student.Carnet = dalStudent.CardId;
                    student.Telefono = dalStudent.PhoneNum;
                    student.Fecha_Registro = dalStudent.InDate.ToLongDateString();
                    student.Password = dalStudent.Password;
                    student.TipoRepositorioArchivos = dalStudent.TRepo.ToString();
                    var studentPhoto = dalStudent.Photo;
                    student.Foto = studentPhoto != null ? Convert.ToBase64String(dalStudent.Photo) : "";
                    student.Universidad = dalStudent.University.Name;
                    student.UniversityId = dalStudent.UniversityId.ToString();
                    student.EnlaceRepositorioCodigo = dalStudent.RepoLink;
                    student.EnlaceACurriculum = dalStudent.ResumeLink;
                    student.PromedioProyectos = (float)dalStudent.AvgProjects;
                    student.PromedioCursos = (float)dalStudent.AvgCourses;

                    var languageList = new List<string>();
                    foreach (var language in dalStudent.Languages)
                    {
                        languageList.Add(language.Name);
                    }
                    student.Idiomas = languageList;
                    student.CursosAprobados = dalStudent.NumSuceedCourses;
                    student.CursosReprobados = dalStudent.NumFailedCourses;
                    student.ProyectosExitosos = dalStudent.NumSuceedProjects;
                    student.ProyectosFallidos = dalStudent.NumFailedProjects;

                    var technologyList = new List<string>();
                    foreach (var technology in dalStudent.Technologies)
                    {
                        technologyList.Add(technology.Name);
                    }
                    student.Tecnologias = technologyList;

                    var courseRepo = new CourseRepository(context);
                    var finishedCourses = courseRepo.GetInactiveStudentCourses(studentId);

                    var finishedCoursesList = new List<FinishedCourse>();
                    foreach (var course in finishedCourses)
                    {
                        var finishedCourse = new FinishedCourse
                        {
                            CourseDescription = course.Description,
                            CourseId = course.CourseId.ToString(),
                            CourseName = course.Name,
                            Accepted = (allprojects.Find(p => p.CourseId.ToString().Equals(course.CourseId.ToString())) != null) ? 1 : 0
                        };
                        finishedCoursesList.Add(finishedCourse);
                    }
                    student.FinishedCoursesList = finishedCoursesList;

                    var activeCoursesList = new List<ActiveCourse>();
                    var activeCourses = courseRepo.GetActiveStudentCourses(studentId);

                    foreach (var course in activeCourses)
                    {
                        var activeCourse = new ActiveCourse
                        {
                            CourseDescription = course.Description,
                            CourseId = course.CourseId.ToString(),
                            CourseName = course.Name,
                            Accepted = (allprojects.Find(p => p.CourseId.ToString().Equals(course.CourseId.ToString())) != null) ? 1 : 0
                        };
                        activeCoursesList.Add(activeCourse);
                    }

                    student.ActiveCoursesList = activeCoursesList;

                    var jobRepo = new JobOfferRepository(context);
                    var finishedJobs = jobRepo.GetStudentInactiveJobOffers(studentId);
                    var finishedJobOffers = new List<FinishedJobOffer>();
                    foreach (var jobOffer in finishedJobs)
                    {
                        var finishedJobOffer = new FinishedJobOffer
                        {
                            JobOffer = jobOffer.Name,
                            JobOfferId = jobOffer.JobOfferId.ToString(),
                            Description = jobOffer.StateDescription,
                            EmployerName = jobOffer.Employer.CompanyName
                        };
                        finishedJobOffers.Add(finishedJobOffer);
                    }

                    student.FinishedJobOffersList = finishedJobOffers;
                    var activeJobs = jobRepo.GetStudentActiveJobOffers(studentId);
                    var activeJobOffers = new List<ActiveJobOffer>();
                    foreach (var jobOffer in activeJobs)
                    {
                        var activeJobOffer = new ActiveJobOffer
                        {
                            JobOffer = jobOffer.Name,
                            JobOfferId = jobOffer.JobOfferId.ToString(),
                            Description = jobOffer.StateDescription,
                            EmployerName = jobOffer.Employer.CompanyName
                        };
                        activeJobOffers.Add(activeJobOffer);
                    }
                    student.ActiveJobOffersList = activeJobOffers;
                    student.Active = IsActive(studentId.ToString());
                    jobRepo.Dispose();
                    courseRepo.Dispose();

                }
                studentRepo.Dispose();

                return student;
            }
        }

        public InfoProfesor ProfessorLogin(string username, string password)
        {
            using (var context = new MyLearnContext())
            {
                var professor = new InfoProfesor();
                var professorRepo = new ProfessorRepository(context);
                var projecRepo = new ProjectRepository(context);
                var professorId = GetUserId(username);
                var dalProfessor = professorRepo.GetProfessorById(professorId);
                var allprojects = projecRepo.GetAll();

                if (dalProfessor != null && dalProfessor.Password == password)
                {
                    professor.RefreshToken = dalProfessor.RefreshToken;
                    professor.UserId = dalProfessor.UserId.ToString();
                    professor.NombreContacto = dalProfessor.Name;
                    professor.ApellidoContacto = dalProfessor.Lastname;
                    professor.Ubicacion = dalProfessor.Country.Name;
                    professor.Email = dalProfessor.Email;
                    professor.Telefono = dalProfessor.PhoneNum;
                    professor.Fecha_Registro = dalProfessor.InDate.ToString();
                    professor.Password = dalProfessor.Password;
                    professor.IdProfesor = dalProfessor.ProfessorId;
                    professor.TipoRepositorioArchivos = dalProfessor.TRepo.ToString();
                    var professorPhoto = dalProfessor.Photo;
                    professor.Foto = professorPhoto != null ? Convert.ToBase64String(professorPhoto) : "";
                    professor.Universidad = dalProfessor.University.Name;
                    professor.UniversityId = dalProfessor.UniversityId.ToString();
                    professor.HorarioAtencion = dalProfessor.Schedule;

                    var courseRepo = new CourseRepository(context);
                    var finishedCourses = courseRepo.GetProfessorInctiveCourses(professorId);

                    var finishedCoursesList = new List<FinishedCourse>();
                    foreach (var course in finishedCourses)
                    {
                        var finishedCourse = new FinishedCourse
                        {
                            CourseDescription = course.Description,
                            CourseId = course.CourseId.ToString(),
                            CourseName = course.Name,
                            Accepted = (allprojects.Find(p => p.CourseId.ToString().Equals(course.CourseId.ToString())) != null) ? 1 : 0
                        };
                        finishedCoursesList.Add(finishedCourse);
                    }

                    professor.FinishedCoursesList = finishedCoursesList;

                    var activeCourses = courseRepo.GetProfessorActiveCourses(professorId);
                    var activeCoursesList = new List<ActiveCourse>();

                    foreach (var course in activeCourses)
                    {
                        var activeCourse = new ActiveCourse
                        {
                            CourseDescription = course.Description,
                            CourseId = course.CourseId.ToString(),
                            CourseName = course.Name,
                            Accepted = (allprojects.Find(p => p.CourseId.ToString().Equals(course.CourseId.ToString())) != null) ? 1 : 0
                        };
                        activeCoursesList.Add(activeCourse);
                    }
                    professor.ActiveCoursesList = activeCoursesList;
                    professor.Active = IsActive(professorId.ToString());
                    projecRepo.Dispose();
                    courseRepo.Dispose();
                    professorRepo.Dispose();
                }
                return professor;
            }
        }

        public InfoEmpleador EmployerLogin(string username, string password)
        {
            using (var context = new MyLearnContext())
            {
                var employer = new InfoEmpleador();
                var employerRepo = new EmployerRepository(context);
                var employerId = GetUserId(username);
                var dalEmployer = employerRepo.GetEmployerById(employerId);

                if (dalEmployer != null && dalEmployer.Password == password)
                {
                    employer.RefreshToken = dalEmployer.RefreshToken;
                    employer.UserId = dalEmployer.UserId.ToString();
                    employer.NombreContacto = dalEmployer.ContactName;
                    employer.ApellidoContacto = dalEmployer.ContactLastname;
                    employer.Ubicacion = dalEmployer.Country.Name;
                    employer.Email = dalEmployer.Email;
                    employer.Telefono = dalEmployer.PhoneNum;
                    employer.Fecha_Registro = dalEmployer.InDate.ToString();
                    employer.Password = dalEmployer.Password;
                    employer.TipoRepositorioArchivos = dalEmployer.TRepo.ToString();
                    var employerPhoto = dalEmployer.Photo;
                    employer.Foto = employerPhoto != null ? Convert.ToBase64String(employerPhoto) : "";
                    employer.IdEmpleador = dalEmployer.EmployerId;
                    employer.NombreEmpresarial = dalEmployer.CompanyName;
                    employer.EnlaceSitioWeb = dalEmployer.Website;

                    var jobRepo = new JobOfferRepository(context);
                    var finishedJobs = jobRepo.GetEmployerInactiveJobOffers(employerId);
                    var finishedJobOffers = new List<FinishedJobOffer>();
                    foreach (var jobOffer in finishedJobs)
                    {
                        var finishedJobOffer = new FinishedJobOffer
                        {
                            JobOffer = jobOffer.Name,
                            JobOfferId = jobOffer.JobOfferId.ToString(),
                            Description = jobOffer.StateDescription,
                            EmployerName = jobOffer.Employer.CompanyName
                        };
                        finishedJobOffers.Add(finishedJobOffer);
                    }

                    employer.FinishedJobOffersList = finishedJobOffers;

                    var activeJobs = jobRepo.GetEmployerActiveJobOffers(employerId);
                    var activeJobOffers = new List<ActiveJobOffer>();
                    foreach (var jobOffer in activeJobs)
                    {
                        var activeJobOffer = new ActiveJobOffer
                        {
                            JobOffer = jobOffer.Name,
                            JobOfferId = jobOffer.JobOfferId.ToString(),
                            Description = jobOffer.StateDescription,
                            EmployerName = jobOffer.Employer.CompanyName
                        };
                        activeJobOffers.Add(activeJobOffer);
                    }

                    employer.ActiveJobOffersList = activeJobOffers;
                    jobRepo.Dispose();
                    employer.Active = IsActive(employerId.ToString());
                }
                employerRepo.Dispose();
                return employer;
            }
         
        }

        public InfoAdmin AdminLogin(string username, string password)
        {
            var admin = new InfoAdmin
            {
                UserId = "",
                UserName = "",
                Tecnologias = new List<string>(),
                Universidades = new List<string>()
            };
            //subject to change


            return admin;
        }

        public ReturnCode ToggleAccount(string userId)
        {
            using (var context = new MyLearnContext())
            {
                var success = new ReturnCode();
                var userRepo = new UserRepository(context);
                var user = userRepo.GetUserById(new Guid(userId));
                if (user != null)
                {
                    user.IsActive = user.IsActive == 1 ? 0 : 1;
                    success.ReturnStatus = 1;
                }
                userRepo.Dispose();
                return success;
            }
        }

        public int IsActive(string userId)
        {
            using (var context = new MyLearnContext())
            {
                var userRepo = new UserRepository(context);
                var user = userRepo.GetUserById(new Guid(userId));
                var active = 1;
                if (user != null)
                {
                    active = user.IsActive;
                }
                userRepo.Dispose();
                return active;
            }
        }
    }
}
