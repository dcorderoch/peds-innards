using System;
using MyLearn.Models;
using MyLearnDAL;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using MyLearn.GoogleService;

namespace MyLearn.BLL
{
    /// <summary>
    /// Class built in order to register students, professors and employers.
    /// </summary>
    public class RegisterManager
    {
        /// <summary>
        /// Method in charge of registering a new student in MyLearn.
        /// </summary>
        /// <param name="newStudent"></param>
        /// <returns>Student information if registered successfully.</returns>
        public InfoEstudiante StudentRegister(RegisterEstudianteInfo newStudent)
        {
            var accountSession = new AccountManager();
            AddStudentToDB(newStudent);
            var retStudent = accountSession.StudentLogin(newStudent.Email, newStudent.Password);
            return retStudent;
         }
        /// <summary>
        /// Handles the professor register in MyLearn.
        /// </summary>
        /// <param name="newProfessor"></param>
        /// <returns>Professor's information.</returns>
        public InfoProfesor ProfessorRegister(RegisterProfessorInfo newProfessor)
        {
            var accountSession = new AccountManager();
            AddProfessorToDB(newProfessor);
            var retProfessor = accountSession.ProfessorLogin(newProfessor.Email, newProfessor.Password);
            return retProfessor;

        }
        /// <summary>
        /// Registers given employer in MyLearn.
        /// </summary>
        /// <param name="newEmployer"></param>
        /// <returns>Employer's information.</returns>
        public InfoEmpleador EmployerRegister(RegisterEmployerInfo newEmployer)
        {
            var accountSession = new AccountManager();
            AddEmployerToDB(newEmployer);
            var retEmployer= accountSession.EmployerLogin(newEmployer.Email, newEmployer.Password);
            return retEmployer;

        }
        /// <summary>
        /// Auxiliary method that adds new student to database.
        /// </summary>
        /// <param name="newStudent"></param>
        private void AddStudentToDB(RegisterEstudianteInfo newStudent)
        {
            using (MyLearnContext context = new MyLearnContext())
            {
                var studentRepo = new StudentRepository(context);
                var techRepo = new TechnologyRepository(context);
                var langRepo = new LanguageRepository(context);
                var gauthenticator = new GoogleAuthenticator();
                string refreshtoken = gauthenticator.GetRefreshToken(newStudent.AuthToken);
                var student = new Student
                {
                    UserId = Guid.NewGuid(),
                    Name = newStudent.NombreContacto,
                    LastName = newStudent.ApellidoContacto,
                    Password = newStudent.Password,
                    CardId = newStudent.Carnet,
                    AvgProjects = 0,
                    AvgCourses = 0,
                    NumFailedCourses = 0,
                    NumSuceedCourses = 0,
                    NumFailedProjects = 0,
                    NumSuceedProjects = 0,
                    RepoLink = newStudent.EnlaceRepositorioCodigo,
                    ResumeLink = newStudent.EnlaceACurriculum,
                    CountryId = Guid.Parse(newStudent.Ubicacion),
                    UniversityId = Guid.Parse(newStudent.Universidad),
                    Photo = newStudent.Foto.Equals("") ? null : Convert.FromBase64String(newStudent.Foto),
                    Email = newStudent.Email,
                    InDate = DateTime.Now,
                    IsActive = 1,
                    RoleId = 1,
                    TRepo = Convert.ToInt32(newStudent.TipoRepositorioArchivos),
                    PhoneNum = newStudent.Telefono,
                    RefreshToken = refreshtoken
                };


                foreach (var tech in newStudent.Tecnologias)
                {
                    var technology = techRepo.GetTechnologybyId(Guid.Parse(tech));
                    student.Technologies.Add(technology);
                }

                foreach (var lang in newStudent.Idiomas)
                {
                    var language = langRepo.Get(Convert.ToInt32(lang));
                    student.Languages.Add(language);
                }
                studentRepo.Add(student);
                studentRepo.SaveChanges();
                studentRepo.Dispose();
                techRepo.Dispose();
                langRepo.Dispose();
            }
        }
        /// <summary>
        /// Adds a new professor to database.
        /// </summary>
        /// <param name="newProfessor"></param>
        private void AddProfessorToDB(RegisterProfessorInfo newProfessor)
        {
            using (var context = new MyLearnContext())
            {
                var professorRepo = new ProfessorRepository(context);
                var countryRepo = new CountryRepository(context);
                var universityRepo = new UniversityRepository(context);
                var gauthenticator = new GoogleAuthenticator();
                string refreshtoken = gauthenticator.GetRefreshToken(newProfessor.AuthToken);
                var professor = new Professor
                {
                    UserId = Guid.NewGuid(),
                    Name = newProfessor.NombreContacto,
                    Lastname = newProfessor.ApellidoContacto,
                    Email = newProfessor.Email,
                    PhoneNum = newProfessor.Telefono,
                    Schedule = newProfessor.HorarioAtencion,
                    ProfessorId = newProfessor.IdProfesor,
                    Password = newProfessor.Password,
                    TRepo = Convert.ToInt32(newProfessor.TipoRepositorioArchivos),
                    RefreshToken = refreshtoken
                };

                var university = universityRepo.GetUniversityById(Guid.Parse(newProfessor.Universidad));
                professor.UniversityId = university.UniversityId;
                professor.ProfessorId = newProfessor.IdProfesor;
                professor.InDate = DateTime.Now;
                professor.RoleId = 2;
                var country = countryRepo.GetCountryById(Guid.Parse(newProfessor.Ubicacion));
                professor.CountryId = country.CountryId;

                professor.TRepo = Convert.ToInt32(newProfessor.TipoRepositorioArchivos);
                professor.IsActive = 1;
                professor.Photo = newProfessor.Foto.Equals("") ? null : Convert.FromBase64String(newProfessor.Foto);

                professorRepo.Add(professor);
                professorRepo.SaveChanges();
                professorRepo.Dispose();
                countryRepo.Dispose();
                universityRepo.Dispose();
            }
        }
        /// <summary>
        /// Adds a new employer to database.
        /// </summary>
        /// <param name="newEmployer"></param>
        private void AddEmployerToDB(RegisterEmployerInfo newEmployer)
        {
            using (var context = new MyLearnContext())
            {
                var employerRepo = new EmployerRepository(context);
                var countryRepo = new CountryRepository(context);
                var gauthenticator = new GoogleAuthenticator();
                string refreshtoken = gauthenticator.GetRefreshToken(newEmployer.AuthToken);
                var employer = new Employer
                {
                    UserId = Guid.NewGuid(),
                    ContactName = newEmployer.NombreContacto,
                    ContactLastname = newEmployer.ApellidoContacto,
                    CompanyName = newEmployer.NombreEmpresarial,
                    EmployerId = newEmployer.IdEmpresa,
                    Website = newEmployer.EnlaceSitioWeb,
                    Photo = newEmployer.Foto.Equals("") ? null : Convert.FromBase64String(newEmployer.Foto),
                    TRepo = Convert.ToInt32(newEmployer.TipoRepositorioArchivos),
                    RefreshToken = refreshtoken
                };
                var country = countryRepo.GetCountryById(Guid.Parse(newEmployer.Ubicacion));
                employer.CountryId = country.CountryId;
                employer.Email = newEmployer.Email;
                employer.Password = newEmployer.Password;
                employer.PhoneNum = newEmployer.Telefono;
                employer.InDate = DateTime.Now;
                employer.IsActive = 1;
                employer.RoleId = 3;
                employerRepo.Add(employer);
                employerRepo.SaveChanges();
                employerRepo.Dispose();
                countryRepo.Dispose();
            }
        }
    }
}