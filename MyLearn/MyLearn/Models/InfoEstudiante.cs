using System.Collections.Generic;

namespace MyLearn.Models
{
    public class InfoEstudiante
    {
        public string RefreshToken { get; set; }
        public int Active { get; set; }
        public string UserId { get; set; }
        public string NombreContacto { get; set; }
        public string ApellidoContacto { get; set; }
        public string Ubicacion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Fecha_Registro { get; set; }
        public string Password { get; set; }
        public string TipoRepositorioArchivos { get; set; }
        public string Foto { get; set; }
        public string Carnet { get; set; }
        public string Universidad { get; set; }
        public string UniversityId { get; set; }
        public string EnlaceRepositorioCodigo { get; set; }
        public string EnlaceACurriculum { get; set; }
        public decimal PromedioProyectos { get; set; }
        public decimal PromedioCursos { get; set; }
        public List<string> Idiomas { get; set; }
        public int CursosAprobados { get; set; }
        public int CursosReprobados { get; set; }
        public int ProyectosExitosos { get; set; }
        public int ProyectosFallidos { get; set; }
        public List<string> Tecnologias { get; set; }
        public List<FinishedCourse> FinishedCoursesList { get; set; }
        public List<ActiveCourse> ActiveCoursesList { get; set; }
        public List<FinishedJobOffer> FinishedJobOffersList { get; set; }
        public List<ActiveJobOffer> ActiveJobOffersList { get; set; }
    }
}