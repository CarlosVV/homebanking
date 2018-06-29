using System.ComponentModel.DataAnnotations;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class AmpliacionCredito : Solicitud
    {
        [Required]
        public string IdSocio { get; set; }

        [Required]
        public string IdTarjeta { get; set; }

        [Required]
        public string NumeroTarjeta { get; set; }

        [Required]
        public decimal CreditoActual { get; set; }

        [Required]
        public decimal CreditoSolicitado { get; set; }
    }
}