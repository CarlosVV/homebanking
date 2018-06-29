using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories.Solicitudes
{
    [Table("Reclamo")]
    public class Reclamo : Solicitud
    {
        public string IdSocio { get; set; }
        public string SocioNombres { get; set; }
        public string SocioTipoDocumento { get; set; }
        public string SocioNumeroDocumento { get; set; }
        public string SocioCorreElectronico { get; set; }
        public string SocioCelular { get; set; }

        public string IdTarjeta { get; set; }
        public string TarjetaNumero { get; set; }

        [Required]
        public string Motivo { get; set; }

        public string Medio { get; set; }
        [Required]
        public string DireccionEnvio { get; set; }

        public string Descripcion { get; set; }
        public string SolucionEsperada { get; set; }
    }
}
