namespace MyLearn.Models
{
    public class RegisterProfessorInfo
    {
        public string NombreContacto { get; set; }
        public string ApellidoContacto { get; set; }
        public string Ubicacion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string Password { get; set; }
        public string TipoRepositorioArchivos { get; set; }
        public string Foto { get; set; } // Opcional
        public string Universidad { get; set; }
        public string HorarioAtencion { get; set; }
    }
}