using System.Collections.Generic;

namespace MyLearn.Models
{
    public class InfoProfesor
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
        public string IdProfesor { get; set; }
        public string Universidad { get; set; }
        public string HorarioAtencion { get; set; }
        public List<string> ListaCursosTerminados { get; set; }
        public List<string> ListaCursosActivos { get; set; }
        public List<string> ListaProyectosTerminados { get; set; }
        public List<string> ListaProyectosActivos { get; set; }
    }
}