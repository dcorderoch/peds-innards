using System.Collections.Generic;

namespace MyLearn.Models
{
    public class RegisterEstudianteInfo
    {
        public string NombreContacto { get; set; }
        public string ApellidoContacto { get; set; }
        public string Ubicacion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public string TipoRepositorioArchivos { get; set; }
        public string Foto { get; set; }
        public string Carnet { get; set; }
        public string Universidad { get; set; }
        public string EnlaceRepositorioCodigo { get; set; }
        public string EnlaceACurriculum { get; set; }
        public List<string> Idiomas { get; set; }
        public List<string> Tecnologias { get; set; }
        public string AuthToken { get; set; }
    }
}