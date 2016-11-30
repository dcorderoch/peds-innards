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
                Name = "Verilog",
                TecnologyId = Guid.Parse("A1A1A1A1-A1A1-A1A1-A1A1-A1A1A1A1A1A1")
            };
            var newTechnology1 = new Technology()
            {
                Name = "REST API",
                TecnologyId = Guid.Parse("B1B1B1B1-B1B1-B1B1-B1B1-B1B1B1B1B1B1")
            };
            var newTechnology2 = new Technology()
            {
                Name = "OpenCV",
                TecnologyId = Guid.Parse("C1C1C1C1-C1C1-C1C1-C1C1-C1C1C1C1C1C1")
            };
            var newTechnology3 = new Technology()
            {
                Name = "OpenCL",
                TecnologyId = Guid.Parse("D1D1D1D1-D1D1-D1D1-D1D1-D1D1D1D1D1D1")
            };
            var newTechnology4 = new Technology()
            {
                Name = "CUDA",
                TecnologyId = Guid.Parse("E1E1E1E1-E1E1-E1E1-E1E1-E1E1E1E1E1E1")
            };
            var newTechnology5 = new Technology()
            {
                Name = "MEAN Stack",
                TecnologyId = Guid.Parse("F1F1F1F1-F1F1-F1F1-F1F1-F1F1F1F1F1F1")
            };
            var newTechnology6 = new Technology()
            {
                Name = "SQL",
                TecnologyId = Guid.Parse("A1B1A1B1-A1B1-A1B1-A1B1-A1B1A1B1A1B1")
            };

            var techRepo = new TechnologyRepository(context);
            techRepo.Add(newTechnology0);
            techRepo.Add(newTechnology1);
            techRepo.Add(newTechnology2);
            techRepo.Add(newTechnology3);
            techRepo.Add(newTechnology4);
            techRepo.Add(newTechnology5);
            techRepo.Add(newTechnology6);

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
                UniversityId = Guid.Parse("A2A2A2A2-A2A2-A2A2-A2A2-A2A2A2A2A2A2")
            };
            var newUniversity1 = new University()
            {
                Name = "UCR",
                UniversityId = Guid.Parse("B2B2B2B2-B2B2-B2B2-B2B2-B2B2B2B2B2B2")
            };
            var newUniversity2 = new University()
            {
                Name = "MIT",
                UniversityId = Guid.Parse("C2C2C2C2-C2C2-C2C2-C2C2-C2C2C2C2C2C2")
            };
            var newUniversity3 = new University()
            {
                Name = "TU Munich",
                UniversityId = Guid.Parse("D2D2D2D2-D2D2-D2D2-D2D2-D2D2D2D2D2D2")
            };
            var newUniversity4 = new University()
            {
                Name = "TU Delf",
                UniversityId = Guid.Parse("E2E2E2E2-E2E2-E2E2-E2E2-E2E2E2E2E2E2")
            };

            var universityRepo = new UniversityRepository(context);
            universityRepo.Add(newUniversity0);
            universityRepo.Add(newUniversity1);
            universityRepo.Add(newUniversity2);
            universityRepo.Add(newUniversity3);
            universityRepo.Add(newUniversity4);

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

            var myStudent = new Student() { UserId = Guid.NewGuid(), AvgCourses = 0, AvgProjects = 0, CardId = "201044569", Email = "pepito@gmail.com", InDate = DateTime.Today, IsActive = 1, LastName = "Gómez", Name = "Pepito", Password = "123456", NumFailedCourses = 0, NumFailedProjects = 0, NumSuceedCourses = 0, NumSuceedProjects = 0, TRepo = 1, RepoLink = "http://githum.com/pepito", PhoneNum = "22399043" };
            myStudent.Languages.Add(newLenguage0);
            myStudent.UniversityId = newUniversity0.UniversityId;
            var countryList = countryRepo.GetAll();
            myStudent.CountryId = countryList[43].CountryId;
            myStudent.RoleId = 1;
            var studentRepo = new StudentRepository(context);
            studentRepo.Add(myStudent);
            studentRepo.SaveChanges();
            /*countryRepo.Dispose();
            studentRepo.Dispose();
            techRepo.Dispose();
            universityRepo.Dispose();
            roleRepo.Dispose();
            languageRepo.Dispose();*/


        }
    }
}
