using MyLearnDAL.Models;
using MyLearnDAL.Repositories;

namespace MyLearnDAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyLearnDAL.MyLearnContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MyLearnDAL.MyLearnContext context)
        {

            var newTechnology0 = new Technology()
            {
                Name = "HDL",
                TechnologyId = Guid.NewGuid()
            };
            var newTechnology1 = new Technology()
            {
                Name = "REST API",
                TechnologyId = Guid.NewGuid()
            };
            var newTechnology2 = new Technology()
            {
                Name = "OpenCV",
                TechnologyId = Guid.NewGuid()
            };
            var newTechnology3 = new Technology()
            {
                Name = "OpenCL",
                TechnologyId = Guid.NewGuid()
            };
            var newTechnology4 = new Technology()
            {
                Name = "CUDA",
                TechnologyId = Guid.NewGuid()
            };
            var newTechnology5 = new Technology()
            {
                Name = "MEAN Stack",
                TechnologyId = Guid.NewGuid()
            };
            var newTechnology6 = new Technology()
            {
                Name = "LAMP Stack",
                TechnologyId = Guid.NewGuid()
            };
            var newTechnology7 = new Technology()
            {
                Name = "OpenAC",
                TechnologyId = Guid.NewGuid()
            };
            var newTechnology8 = new Technology()
            {
                Name = "OpenMP",
                TechnologyId = Guid.NewGuid()
            };
            var newTechnology9 = new Technology()
            {
                Name = "Web Design",
                TechnologyId = Guid.NewGuid()
            };

            var newTechnology10 = new Technology()
            {
                Name = "FPGA",
                TechnologyId = Guid.NewGuid()
            };

            var techRepo = new TechnologyRepository(context);
            techRepo.Add(newTechnology0);
            techRepo.Add(newTechnology1);
            techRepo.Add(newTechnology2);
            techRepo.Add(newTechnology3);
            techRepo.Add(newTechnology4);
            techRepo.Add(newTechnology5);
            techRepo.Add(newTechnology6);
            techRepo.Add(newTechnology7);
            techRepo.Add(newTechnology8);
            techRepo.Add(newTechnology9);
            techRepo.Add(newTechnology10);

            techRepo.SaveChanges();

            var countries = new[]
            {

                "Afganist�n", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saud�ta",
                "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiy�n", "Bahamas", "Banglad�s",
                "Barbados", "Bar�in", "B�lgica", "Belice", "Ben�n", "Bielorrusia", "Bolivia", "Bosnia y Herzegovina",
                "Botsuana", "Brasil", "Brun�i", "Bulgaria", "Burkina Faso", "Burundi", "But�n", "Cabo Verde", "Camboya",
                "Camer�n", "Canad�", "Catar", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo",
                "Costa de Marfil", "Costa Rica", "Croacia Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto",
                "El Salvador", "Emiratos �rabes Unidos", "Eritrea", "Eslovaquia", "Eslovenia", "Espa�a",
                "Estados Unidos de Am�rica", "Estonia", "Etiop�a", "Federaci�n Rusa", "Filipinas", "Finlandia", "Fiyi",
                "Francia", "Gab�n", "Gambia", "Georgia", "Ghana", "Granada", "Grecia", "Guatemala", "Guinea",
                "Guinea-Bis�u", "Guinea Ecuatorial", "Guyana", "Hait�", "Honduras", "Hungr�a", "India", "Indonesia",
                "Irak", "Ir�n ", "Irlanda", "Islandia", "Marshall", "Salom�n Islas Salom�n", "Israel", "Italia",
                "Jamaica", "Jap�n", "Jordania", "Kazajist�n", "Kenia", "Kirguist�n", "Kiribati", "Kuwait", "Lesoto",
                "Letonia", "L�bano", "Libia", "Liberia", "Liechtenstein", "Lituania", "Luxemburgo", "Macedonia",
                "Madagascar", "Malasia", "Malaui", "Maldivas", "Mal�", "Malta", "Marruecos", "Mauricio", "Mauritania",
                "M�xico", "Micronesia", "M�naco", "Montenegro", "Mongolia", "Mozambique", "Myanmar", "Namibia", "Nauru",
                "Nicaragua", "N�ger", "Nigeria", "Noruega", "Zelanda", "Om�n", "Pa�ses Bajos", "Pakist�n", "Palaos",
                "Panam�", "Pap�a Nueva Guinea", "Paraguay", "Per�", "Polonia", "Portugal", "Irlanda del Norte",
                "Rep�blica �rabe Siria", "Rep�blica Centroafricana", "Rep�blica Checa", "Corea del Sur",
                "Rep�blica de Moldavia", "Rep�blica Democr�tica del Congo", "Lao", "Rep�blica Dominicana", "Nepal",
                "Corea del Norte", "Tanzania", "Ruanda", "Rumania", "Samoa", "Crist�bal y Nieves", "San Marino",
                "Granadinas", "Luc�a Santa Luc�a", "Santo Tom� y Pr�ncipe", "Senegal", "Serbia", "Seychelles",
                "Sierra Leona", "Singapur", "Somalia", "Sri Lanka", "Sud�frica", "Sud�n", "Sud�n del Sur", "Suecia",
                "Suiza", "Surinam", "Suazilandia", "Tailandia", "Tayikist�n", "Oriental", "Togo", "Tonga", "Tobago",
                "T�nez", "Turkmenist�n", "Turqu�a", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekist�n", "Vanuatu",
                "Venezuela", "Vietnam", "Yemen", "Yibuti", "Zambia", "Zimbabue"
            };

            var countryRepo = new CountryRepository(context);
            foreach (var country in countries)
            {
                countryRepo.Add(new Country() { CountryId = Guid.NewGuid(), Name = country });
            }
            countryRepo.SaveChanges();
            //countryRepo.Dispose();

            var newUniversity0 = new University()
            {
                Name = "ITCR",
                UniversityId = Guid.NewGuid()
            };
            var newUniversity1 = new University()
            {
                Name = "UCR",
                UniversityId = Guid.NewGuid()
            };
            var newUniversity2 = new University()
            {
                Name = "MIT",
                UniversityId = Guid.NewGuid()
            };
            var newUniversity3 = new University()
            {
                Name = "TU Munich",
                UniversityId = Guid.NewGuid()
            };
            var newUniversity4 = new University()
            {
                Name = "TU Delf",
                UniversityId = Guid.NewGuid()
            };
            var newUniversity5 = new University()
            {
                Name = "TEC Monterrey",
                UniversityId = Guid.NewGuid()
            };

            var universityRepo = new UniversityRepository(context);
            universityRepo.Add(newUniversity0);
            universityRepo.Add(newUniversity1);
            universityRepo.Add(newUniversity2);
            universityRepo.Add(newUniversity3);
            universityRepo.Add(newUniversity4);
            universityRepo.Add(newUniversity5);

            universityRepo.SaveChanges();
            


            var newLenguage0 = new Language() { LenguageId = 0, Name = "Espa�ol" };
            var newLenguage1 = new Language() { LenguageId = 1, Name = "English" };
            var newLenguage2 = new Language() { LenguageId = 2, Name = "Portugu�s" };
            var newLenguage3 = new Language() { LenguageId = 3, Name = "Fran�ais" };

            var languageRepo = new LanguageRepository(context);
            languageRepo.Add(newLenguage0);
            languageRepo.Add(newLenguage1);
            languageRepo.Add(newLenguage2);
            languageRepo.Add(newLenguage3);

            languageRepo.SaveChanges();



            var studentRole = new Role() { RoleId = 1, Description = "Estudiante" };
            var professorRole = new Role() { RoleId = 2, Description = "Profesor" };
            var employerRole = new Role() { RoleId = 3, Description = "Empleador" };

            var roleRepo = new RoleRepository(context);
            roleRepo.Add(studentRole);
            roleRepo.Add(professorRole);
            roleRepo.Add(employerRole);
            roleRepo.SaveChanges();
            //roleRepo.Dispose();

            var myStudent = new Student() { UserId = Guid.NewGuid(), AvgCourses = 0, AvgProjects = 0, CardId = "201044569", Email = "pepito@gmail.com", InDate = DateTime.Today, IsActive = 1, LastName = "G�mez", Name = "Pepito", Password = "123456", NumFailedCourses = 0, NumFailedProjects = 0, NumSuceedCourses = 0, NumSuceedProjects = 0, TRepo = 1, RepoLink = "http://githum.com/pepito", PhoneNum = "22399043"};
            myStudent.Languages.Add(newLenguage0);
            myStudent.UniversityId = newUniversity0.UniversityId;
            var countryList = countryRepo.GetAll();
            myStudent.CountryId = countryList[43].CountryId;
            myStudent.RoleId = 1;

            var myStudent2 = new Student() { UserId = Guid.NewGuid(), AvgCourses = 0, AvgProjects = 0, CardId = "ABC123", Email = "fulana@gmail.com", InDate = DateTime.Today, IsActive = 1, LastName = "Gonzalez", Name = "Fulana", Password = "654321", NumFailedCourses = 0, NumFailedProjects = 0, NumSuceedCourses = 0, NumSuceedProjects = 0, TRepo = 1, RepoLink = "http://githum.com/fulana", PhoneNum = "22113344" };
            myStudent2.Languages.Add(newLenguage1);
            myStudent2.UniversityId = newUniversity0.UniversityId;
            myStudent2.CountryId = countryList[25].CountryId;
            myStudent2.RoleId = 1;
            var studentRepo = new StudentRepository(context);
            studentRepo.Add(myStudent);
            studentRepo.Add(myStudent2);
            studentRepo.SaveChanges();

            var myProfesor = new Professor() {CountryId = countryList[32].CountryId, UserId = Guid.NewGuid(), Email = "srmadriz@gmail.com", InDate = DateTime.Today, Lastname = "Madriz", Name = "Daniel", IsActive = 1, Password = "ajaaaa", PhoneNum = "25565644",Photo = null, ProfessorId = "PROF123", RefreshToken = "", RoleId = 2,Schedule = "todos los dias :7am-9pm",TRepo = 0,UniversityId = newUniversity0.UniversityId};

            var professorRepo = new ProfessorRepository(context);
            professorRepo.Add(myProfesor);
            professorRepo.SaveChanges();

            var myEmployer = new Employer() {UserId = Guid.NewGuid(),CompanyName = "Pischel S.A", ContactLastname = "Rivera", Email = "marquitorivera@gmail.com", ContactName = "Marco", CountryId = countryList[50].CountryId, EmployerId = "EMP123", InDate = DateTime.Today, IsActive = 1, Password = "pischel", PhoneNum = "22345677",Photo = null, RoleId = 3,TRepo = 0,Website = "dualfarma.com",RefreshToken = ""};
            var emploRepo = new EmployerRepository(context);
            emploRepo.Add(myEmployer);
            emploRepo.SaveChanges();


        }
    }
}
