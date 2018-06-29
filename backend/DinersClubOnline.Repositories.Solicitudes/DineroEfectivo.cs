using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DinersClubOnline.Repositories.Solicitudes
{
    [Table("DineroEfectivo")]
    public class DineroEfectivo : Solicitud
    {
        [Required]
        public string IdBanco { get; set; }
        [Required]
        public string IdTarjeta { get; set; }
        [Required]
        public string Banco { get; set; }
        [Required]
        public string NumeroCuentaDestino { get; set; }
        [Required]
        public decimal MontoPrestamo { get; set; }
        [Required]
        public int Cuotas { get; set; }
        [Required]
        public decimal Tcea { get; set; }
        [Required]
        public decimal MontoCuota { get; set; }
        [Required]
        public string TipoCuenta { get; set; }
        [Required]
        public string TipoMoneda { get; set; }
    }
}