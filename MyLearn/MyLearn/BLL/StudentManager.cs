using System;
using System.Collections.Generic;
using MyLearn.Models;
using MyLearn.InputModels;
using MyLearn.Utils;
using MyLearnDAL;
using MyLearnDAL.Repositories;

namespace MyLearn.BLL
{
    /// <summary>
    /// Class built in order to manage the student profile.
    /// </summary>
    public class StudentManager
    {
        private ModelMapper mapper;

        public StudentManager()
        {
            mapper = new ModelMapper();
        }

        /// <summary>
        /// Obtains a student's id for an employer's point of view.
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public StudentProfileAsEmployer GetProfile(StudentIdentifier studentId)
        {
            using (var context = new MyLearnContext())
            {
                var courseRepo = new CourseRepository(context);
                var studentRepo = new StudentRepository(context);
                var projRepo = new ProjectRepository(context);
                var student = studentRepo.GetStudentById(Guid.Parse(studentId.StudentUserId));
                var allprojects = projRepo.GetAll();
                var jobRepo = new JobOfferRepository(context);

                var finishedCourses = courseRepo.GetInactiveStudentCourses(student.UserId);
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

                var activeCoursesList = new List<ActiveCourse>();
                var activeCourses = courseRepo.GetActiveStudentCourses(student.UserId);
      
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
                
                var finishedJobs = jobRepo.GetStudentInactiveJobOffers(student.UserId);
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

                
                var activeJobs = jobRepo.GetStudentActiveJobOffers(student.UserId);
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
                var result = new StudentProfileAsEmployer();

                result.UserId = student.UserId.ToString();
                result.NombreContacto = student.Name;
                result.ApellidoContacto = student.LastName;
                result.Ubicacion = student.Country.Name;
                result.Email = student.Email;
                result.Telefono = student.PhoneNum;
                result.Fecha_Registro = student.InDate.ToString();
                result.TipoRepositorioArchivos = student.TRepo.ToString();
                result.Foto = student.Photo != null ? Convert.ToBase64String(student.Photo) : "";
                result.StudentUserId = student.CardId;
                result.Universidad = student.University.Name;
                result.EnlaceRepositorioCodigo = student.RepoLink;
                result.EnlaceACurriculum = student.ResumeLink;
                result.PromedioProyectos = student.AvgProjects;
                result.PromedioCursos = student.AvgCourses;
                result.Idiomas = mapper.LanguagesToString(student.Languages);
                result.CursosAprobados = student.NumSuceedCourses;
                result.CursosReprobados = student.NumFailedCourses;
                result.ProyectosExitosos = student.NumSuceedProjects;
                result.ProyectosFallidos = student.NumFailedProjects;
                result.Tecnologias = mapper.TechnologiesToString(student.Technologies);
                result.FinishedCoursesList = finishedCoursesList;
                result.ActiveCoursesList = mapper.ActiveCourseListMap(student.Courses);
                result.ActiveCoursesList = activeCoursesList;
                result.FinishedJobOffersList = finishedJobOffers;
                result.ActiveJobOffersList = activeJobOffers;
                return result;
            }
        }
        public StudentStats GetStats(StudentIdentifier studentId)
        {
            using (var context = new MyLearnContext())
            {
                var studentRepo = new StudentRepository(context);
                var student = studentRepo.GetStudentById(Guid.Parse(studentId.StudentUserId));

                var result = new StudentStats();

                result.PromedioProyectos = student.AvgProjects;
                result.PromedioCursos = student.AvgCourses;
                result.CursosAprobados = student.NumSuceedCourses;
                result.CursosReprobados = student.NumFailedCourses;
                result.ProyectosExitosos = student.NumSuceedProjects;
                result.ProyectosFallidos = student.NumFailedProjects;
                return result;
            }
        }
    }
}