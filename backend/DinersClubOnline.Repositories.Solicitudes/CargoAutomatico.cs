using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DinersClubOnline.Repositories.Solicitudes
{
    [Table("CargoAutomatico")]
    public class CargoAutomatico : Solicitud
    {
        [Required]
        public string IdSocio { get; set; }

        public string SocioNombres { get; set; }
        public string SocioTipoDocumento { get; set; }
        public string SocioNumeroDocumento { get; set; }
        public string SocioCorreElectronico { get; set; }
        public string SocioCelular { get; set; }

        [Required]
        public string IdTarjeta { get; set; }
        public string TarjetaNumero { get; set; }
        public string TarjetaProducto { get; set; }
        public string TarjetaVence { get; set; }

        public string IdCategoria { get; set; }
        public string CategoriaNombre { get; set; }

        [Required]
        public string IdEmpresa { get; set; }
        public string EmpresaNombre { get; set; }

        [Required]
        public string IdServicio { get; set; }
        public string ServicioNombre { get; set; }

        public string DatoServicio { get; set; }

        [Required]
        public decimal MontoTope { get; set; }
    }
}