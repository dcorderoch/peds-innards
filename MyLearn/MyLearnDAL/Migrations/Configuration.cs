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

                "Afganistán", "Albania", "Alemania", "Andorra", "Angola", "Antigua y Barbuda", "Arabia Saudíta",
                "Argelia", "Argentina", "Armenia", "Australia", "Austria", "Azerbaiyán", "Bahamas", "Bangladés",
                "Barbados", "Baréin", "Bélgica", "Belice", "Benín", "Bielorrusia", "Bolivia", "Bosnia y Herzegovina",
                "Botsuana", "Brasil", "Brunéi", "Bulgaria", "Burkina Faso", "Burundi", "Bután", "Cabo Verde", "Camboya",
                "Camerún", "Canadá", "Catar", "Chad", "Chile", "China", "Chipre", "Colombia", "Comoras", "Congo",
                "Costa de Marfil", "Costa Rica", "Croacia Croacia", "Cuba", "Dinamarca", "Dominica", "Ecuador", "Egipto",
                "El Salvador", "Emiratos Árabes Unidos", "Eritrea", "Eslovaquia", "Eslovenia", "España",
                "Estados Unidos de América", "Estonia", "Etiopía", "Federación Rusa", "Filipinas", "Finlandia", "Fiyi",
                "Francia", "Gabón", "Gambia", "Georgia", "Ghana", "Granada", "Grecia", "Guatemala", "Guinea",
                "Guinea-Bisáu", "Guinea Ecuatorial", "Guyana", "Haití", "Honduras", "Hungría", "India", "Indonesia",
                "Irak", "Irán ", "Irlanda", "Islandia", "Marshall", "Salomón Islas Salomón", "Israel", "Italia",
                "Jamaica", "Japón", "Jordania", "Kazajistán", "Kenia", "Kirguistán", "Kiribati", "Kuwait", "Lesoto",
                "Letonia", "Líbano", "Libia", "Liberia", "Liechtenstein", "Lituania", "Luxemburgo", "Macedonia",
                "Madagascar", "Malasia", "Malaui", "Maldivas", "Malí", "Malta", "Marruecos", "Mauricio", "Mauritania",
                "México", "Micronesia", "Mónaco", "Montenegro", "Mongolia", "Mozambique", "Myanmar", "Namibia", "Nauru",
                "Nicaragua", "Níger", "Nigeria", "Noruega", "Zelanda", "Omán", "Países Bajos", "Pakistán", "Palaos",
                "Panamá", "Papúa Nueva Guinea", "Paraguay", "Perú", "Polonia", "Portugal", "Irlanda del Norte",
                "República Árabe Siria", "República Centroafricana", "República Checa", "Corea del Sur",
                "República de Moldavia", "República Democrática del Congo", "Lao", "República Dominicana", "Nepal",
                "Corea del Norte", "Tanzania", "Ruanda", "Rumania", "Samoa", "Cristóbal y Nieves", "San Marino",
                "Granadinas", "Lucía Santa Lucía", "Santo Tomé y Príncipe", "Senegal", "Serbia", "Seychelles",
                "Sierra Leona", "Singapur", "Somalia", "Sri Lanka", "Sudáfrica", "Sudán", "Sudán del Sur", "Suecia",
                "Suiza", "Surinam", "Suazilandia", "Tailandia", "Tayikistán", "Oriental", "Togo", "Tonga", "Tobago",
                "Túnez", "Turkmenistán", "Turquía", "Tuvalu", "Ucrania", "Uganda", "Uruguay", "Uzbekistán", "Vanuatu",
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
            


            var newLenguage0 = new Language() { LenguageId = 0, Name = "Español" };
            var newLenguage1 = new Language() { LenguageId = 1, Name = "English" };
            var newLenguage2 = new Language() { LenguageId = 2, Name = "Português" };
            var newLenguage3 = new Language() { LenguageId = 3, Name = "Français" };

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

            var myStudent = new Student() { UserId = Guid.NewGuid(), AvgCourses = 0, AvgProjects = 0, CardId = "201044569", Email = "pepito@gmail.com", InDate = DateTime.Today, IsActive = 1, LastName = "Gómez", Name = "Pepito", Password = "123456", NumFailedCourses = 0, NumFailedProjects = 0, NumSuceedCourses = 0, NumSuceedProjects = 0, TRepo = 1, RepoLink = "http://githum.com/pepito", PhoneNum = "22399043"};
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
