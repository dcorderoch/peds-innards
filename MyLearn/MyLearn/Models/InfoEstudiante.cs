using System.Collections.Generic;

namespace MyLearn.Models
{
    public class InfoEstudiante
    {
        public string NombreContacto { get; set; }
        public string ApellidoContacto { get; set; }
        public string Ubicacion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Fecha_Registro { get; set; }
        public string Password { get; set; }
        public string TipoRepositorioArchivos { get; set; }
        public string Foto { get; set; } // Opcional
        public string Carnet { get; set; }
        public string Universidad { get; set; } // hasta aquí son los mismos campos que profesor
        public string EnlaceRepositorioCodigo { get; set; } // Opcional
        public string EnlaceACurriculum { get; set; } // Opcional
        public string PromedioProyectos { get; set; }
        public string PromedioCursos { get; set; }
        public string Idiomas { get; set; }
        public List<string> CursosAprobados { get; set; }
        public List<string> CursosReprobados { get; set; }
        public List<string> ProyectosExitisos { get; set; }
        public List<string> ProyectosFallidos { get; set; }
        public List<string> Tecnologias { get; set; }
    }
}