using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Web.UI;
using MyLearn.InputModels;
using MyLearn.Models;
using MyLearnDAL.Models;
using MyLearnDAL.Repositories;
using Country = MyLearnDAL.Models.Country;
using Course = MyLearnDAL.Models.Course;
using JobOffer = MyLearnDAL.Models.JobOffer;
using Language = MyLearnDAL.Models.Language;
using Technology = MyLearnDAL.Models.Technology;
using University = MyLearnDAL.Models.University;

namespace MyLearn.BLL
{
    public class RegisterManager
    {
        public InfoEstudiante StudentRegister(RegisterEstudianteInfo newStudent)
        {
            InfoEstudiante retStudent = new InfoEstudiante();
            AccountManager accountSession = new AccountManager();
            AddStudentToDB(newStudent);
            retStudent = accountSession.StudentLogin(newStudent.Email, newStudent.Password);
            return retStudent;
         }

        public InfoProfesor ProfessorRegister(RegisterProfessorInfo newProfessor)
        {
            InfoProfesor retProfessor = new InfoProfesor();
            AccountManager accountSession = new AccountManager();
            AddProfessorToDB(newProfessor);
            retProfessor = accountSession.ProfessorLogin(newProfessor.Email, newProfessor.Password);
            return retProfessor;

        }

        public InfoEmpleador EmployerRegister(RegisterEmployerInfo newEmployer)
        {
            InfoEmpleador retEmployer = new InfoEmpleador();
            AccountManager accountSession = new AccountManager();
            AddEmployerToDB(newEmployer);
            retEmployer = accountSession.EmployerLogin(newEmployer.Email, newEmployer.Password);
            return retEmployer;

        }

        private void AddStudentToDB(RegisterEstudianteInfo newStudent)
        {
            StudentRepository studentRepo = new StudentRepository();
            CountryRepository countryRepo = new CountryRepository();
            TechnologyRepository techRepo = new TechnologyRepository();
            UniversityRepository universityRepo = new UniversityRepository();
            LanguageRepository langRepo= new LanguageRepository();

            Student student = new Student
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
                ResumeLink = newStudent.EnlaceACurriculum
            };

            var country = countryRepo.Get(Convert.ToInt32(newStudent.Ubicacion));
            student.CountryId = country.CountryId;
            var university =universityRepo.Get(Convert.ToInt32(newStudent.Universidad));

            student.University = university;
            student.Photo = newStudent.Foto.Equals("") ? null : Convert.FromBase64String(newStudent.Foto);
            student.UniversityId = student.University.UniversityId;
            student.Email = newStudent.Email;
            student.InDate = DateTime.Now;
            student.IsActive = 1;
            student.RoleId = 1;
            student.TRepo = Convert.ToInt32(newStudent.TipoRepositorioArchivos);
            student.PhoneNum = newStudent.Telefono;
            
            foreach (string tech in newStudent.Tecnologias)
            {
                Technology technology = techRepo.Get(Convert.ToInt32(tech));
                student.Technologies.Add(technology);
            }

            foreach (string lang in newStudent.Idiomas)
            {
                Language language = langRepo.Get(Convert.ToInt32(lang));
                student.Languages.Add(language);
            }
            studentRepo.Add(student);
            studentRepo.SaveChanges();
            studentRepo.Dispose();
            countryRepo.Dispose();
            techRepo.Dispose();
            langRepo.Dispose();
            universityRepo.Dispose();
        }

        private void AddProfessorToDB(RegisterProfessorInfo newProfessor)
        {
            ProfessorRepository professorRepo = new ProfessorRepository();
            CountryRepository countryRepo = new CountryRepository();
            UniversityRepository universityRepo = new UniversityRepository();

            Professor professor = new Professor
            {
                UserId = Guid.NewGuid(),
                Name = newProfessor.NombreContacto,
                Lastname = newProfessor.ApellidoContacto,
                Email = newProfessor.Email,
                PhoneNum = newProfessor.Telefono,
                Schedule = newProfessor.HorarioAtencion,
                ProfessorId = newProfessor.IdProfesor,
                Password = newProfessor.Password,
                TRepo = Convert.ToInt32(newProfessor.TipoRepositorioArchivos)
            };

            var university = universityRepo.Get(Convert.ToInt32(newProfessor.Universidad));
            professor.UniversityId = university.UniversityId;
            professor.ProfessorId = newProfessor.IdProfesor;
            professor.InDate = DateTime.Now;
            professor.RoleId = 2;
            var country = countryRepo.Get(Convert.ToInt32(newProfessor.Ubicacion));
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

        private void AddEmployerToDB(RegisterEmployerInfo newEmployer)
        {
            var employerRepo = new EmployerRepository();
            var countryRepo = new CountryRepository();
            Employer employer = new Employer();
            employer.UserId = Guid.NewGuid();
            employer.ContactName = newEmployer.NombreContacto;
            employer.ContactLastname = newEmployer.ApellidoContacto;
            employer.CompanyName = newEmployer.NombreEmpresarial;
            employer.EmployerId = newEmployer.IdEmpresa;
            employer.Website = newEmployer.EnlaceSitioWeb;
            employer.Photo = newEmployer.Foto.Equals("") ? null : Convert.FromBase64String(newEmployer.Foto);
            employer.TRepo = Convert.ToInt32(newEmployer.TipoRepositorioArchivos);
            var country = countryRepo.Get(Convert.ToInt32(newEmployer.Ubicacion));
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
        }
    }
}

 
 
 
 
 
 
 
 
 