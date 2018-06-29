using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DinersClubOnline.Repositories.Solicitudes
{
    [Table("DebitoAutomatico")]
    public class DebitoAutomatico : Solicitud
    {
        [Required]
        public string IdSocio { get; set; }

        [Required]
        public string IdTarjeta { get; set; }

        [Required]
        public string NumeroTarjeta { get; set; }

        [Required]
        public string TipoPagoaCargar { get; set; }

        public string NombreProducto { get; set; }

        public string IdBancoSoles { get; set; }

        public string BancoSoles { get; set; }

        public string TipoCuentaSoles { get; set; }

        public string MonedaDelaCtaSoles { get; set; }

        public string NumeroCuentaSoles { get; set; }

        public string IdBancoDolares { get; set; }

        public string BancoDolares { get; set; }

        public string TipoCuentaDolares { get; set; }

        public string MonedaDelaCtaDolares { get; set; }

        public string NumeroCuentaDolares { get; set; }
    }
}