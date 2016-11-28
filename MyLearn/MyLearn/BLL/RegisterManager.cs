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

            Student student = new Student();
            Country country = new Country();
            University university = new University();
            Role role = new Role();
            student.UserId = Guid.NewGuid();
            student.Name = newStudent.NombreContacto;
            student.LastName = newStudent.ApellidoContacto;
            student.Password = newStudent.Password;
            student.CardId = newStudent.Carnet;
            student.AvgProjects = 0;
            student.AvgCourses = 0;
            student.NumFailedCourses = 0;
            student.NumSuceedCourses = 0;
            student.NumFailedProjects = 0;
            student.NumSuceedProjects = 0;
            student.RepoLink = newStudent.EnlaceRepositorioCodigo;
            student.ResumeLink = newStudent.EnlaceACurriculum;
            student.Bids = new List<Bid>();
            student.Notifications = new List<Notification>();
            student.JobOffers = new List<JobOffer>();


            country = countryRepo.Get(Convert.ToInt32(newStudent.Ubicacion));
            student.Country = country;
            student.Courses = new List<Course>();
            student.Projects = new List<Project>();

            university = universityRepo.Get(Convert.ToInt32(newStudent.Universidad));

            student.University = university;
            student.Photo = System.Text.Encoding.UTF8.GetBytes(newStudent.Foto);
            student.UniversityId = student.University.UniversityId;
            student.Email = newStudent.Email;
            student.InDate = DateTime.Now;
            student.IsActive = 1;

            role.Description = "Estudiante";
            role.RoleId = 1;
  
            student.Role = role;
            student.TRepo = Convert.ToInt32(newStudent.TipoRepositorioArchivos);
            student.PhoneNum = newStudent.Telefono;
            
            List<Technology> listOfTechnologies = new List<Technology>();
            foreach (string tech in newStudent.Tecnologias)
            {
                Technology technology = techRepo.Get(Convert.ToInt32(tech));
                listOfTechnologies.Add(technology);
            }
            student.Technologies = listOfTechnologies;

            List<Language> languages = new List<Language>();
            foreach (string lang in newStudent.Idiomas)
            {
                Language language = langRepo.Get(Convert.ToInt32(lang));
                languages.Add(language);
            }
            student.Languages = languages;
            studentRepo.Add(student);
         
        }

        private void AddProfessorToDB(RegisterProfessorInfo newProfessor)
        {
            ProfessorRepository professorRepo = new ProfessorRepository();
            CountryRepository countryRepo = new CountryRepository();
            UniversityRepository universityRepo = new UniversityRepository();
     
            Professor professor = new Professor();
            Country country = new Country();
            University university = new University();
            Role role = new Role();

            professor.UserId = Guid.NewGuid();
            professor.Name = newProfessor.NombreContacto;
            professor.Lastname = newProfessor.ApellidoContacto;
            professor.Email = newProfessor.Email;
            professor.PhoneNum = newProfessor.Telefono;
            professor.Schedule = newProfessor.HorarioAtencion;
            professor.ProfessorId = newProfessor.IdProfesor;
            professor.Password = newProfessor.Password;
            professor.TRepo = Convert.ToInt32(newProfessor.TipoRepositorioArchivos);
            university = universityRepo.Get(Convert.ToInt32(newProfessor.Universidad));
            professor.University = university;
            professor.ProfessorId = newProfessor.IdProfesor;
            professor.InDate = DateTime.Now;

            role.Description = "Profesor";
            role.RoleId = 2;

            professor.Role = role;
            country = countryRepo.Get(Convert.ToInt32(newProfessor.Ubicacion));
            professor.Country = country;
            professor.Courses = new List<Course>();

            university = universityRepo.Get(Convert.ToInt32(newProfessor.Universidad));
            professor.University = university;
            professor.TRepo = Convert.ToInt32(newProfessor.TipoRepositorioArchivos);
            professor.UniversityId = professor.University.UniversityId;
            professor.IsActive = 1;
            professor.Photo = System.Text.Encoding.UTF8.GetBytes(newProfessor.Foto);
        
            professorRepo.Add(professor);

        }

        private void AddEmployerToDB(RegisterEmployerInfo newEmployer)
        {
            //Add new employer to DB
            /*dbobject.Add(newEmployer.NombreContacto);
            dbobject.Add(newEmployer.ApellidoContacto);
            dbobject.Add(newEmployer.Ubicacion);
            dbobject.Add(newEmployer.Email);
            dbobject.Add(newEmployer.Telefono);
            dbobject.Add(newEmployer.Password);
            dbobject.Add(newEmployer.TipoRepositorioArchivos);
            dbobject.Add(newEmployer.Foto);
            dbobject.Add(newEmployer.NombreEmpresarial);
            dbobject.Add(newEmployer.EnlaceSitioWeb);*/
        }



    }
}

 
 
 
 
 
 
 
 
 