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

            var myStudent = new Student() { UserId = Guid.NewGuid(), AvgCourses = 0, AvgProjects = 0, CardId = "201044569", Email = "pepito@gmail.com", InDate = DateTime.Today, IsActive = 1, LastName = "G�mez", Name = "Pepito", Password = "123456", NumFailedCourses = 0, NumFailedProjects = 0, NumSuceedCourses = 0, NumSuceedProjects = 0, TRepo = 1, RepoLink = "http://githum.com/pepito", PhoneNum = "22399043" };
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
