
using System;
using System.Collections.Generic;
using MyLearn.Models;


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
            int code = 0;


            //code goes here


            return userTypeCode;
        }
        /// <summary>
        /// Method that returns to the REST API the corresponding user's information if and only if the parameters given match a user in the DB.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns>User information.</returns>
        public InfoEstudiante StudentLogin(int username, string password)
        {
            InfoEstudiante student = new InfoEstudiante();
       //     DBUser dbUserInstance = new DBUser();
       // SUBJECT TO CHANGE
            student.UserId = "";
            student.NombreContacto = "";
            student.ApellidoContacto= "";
            student.Ubicacion= "";
            student.Email = "";
            student.Telefono = "";
            student.Fecha_Registro = "";
            student.Password ="";
            student.TipoRepositorioArchivos = "";
            student.Foto = "";
            student.Universidad = "";
            student.UniversityId = "";
            student.EnlaceRepositorioCodigo = "";
            student.EnlaceACurriculum = "";
            student.PromedioProyectos = 1;
            student.PromedioCursos = 1;
            student.Idiomas= new List<string>();
            student.CursosAprobados = 0;
            student.CursosReprobados=0;
            student.ProyectosExitosos = 0;
            student.ProyectosFallidos = 0;
            student.Tecnologias=new List<string>();

            List<FinishedCoursesList> finishedCoursesList = new List<FinishedCoursesList>();
            FinishedCoursesList finishedCourse = new FinishedCoursesList();
            finishedCourse.CourseDescription = "";
            finishedCourse.CourseId = "";
            finishedCourse.course = "";
            student.FinishedCoursesList= finishedCoursesList;
            
            List<ActiveCoursesList> activeCoursesList = new List<ActiveCoursesList>();
            ActiveCoursesList activeCourses = new ActiveCoursesList();
            activeCourses.course = "";
            activeCourses.CourseId = "";
            activeCourses.CourseDescription ="";
            student.ActiveCoursesList = activeCoursesList;

            List<FinishedJobOffersList> finishedJobOffersLists = new List<FinishedJobOffersList>();
            FinishedJobOffersList finishedJobOffers = new FinishedJobOffersList();
            finishedJobOffers.JobOffer = "";
            finishedJobOffers.JobOfferId = "";
            student.FinishedJobOffersList = finishedJobOffersLists;

            List<ActiveJobOffersList> activeJobOffersLists = new List<ActiveJobOffersList>();
            ActiveJobOffersList activeJobOffers = new ActiveJobOffersList();
            activeJobOffers.JobOffer = "";
            activeJobOffers.JobOfferId = "";
            student.ActiveJobOffersList = activeJobOffersLists;


            return student;
        }
    }
}
