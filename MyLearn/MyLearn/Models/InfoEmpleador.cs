using System.Collections.Generic;

namespace MyLearn.Models
{
    public class InfoEmpleador
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
        public string IdEmpleador { get; set; }
        public string NombreEmpresarial { get; set; }
        public string EnlaceSitioWeb { get; set; }
        public List<FinishedJobOffer> FinishedJobOffersList { get; set; }
        public List<ActiveJobOffer> ActiveJobOffersList { get; set; }
    }
}