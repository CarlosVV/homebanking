using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DinersClubOnline.Repositories.Solicitudes
{
    [Table("TarjetaAdicional")]
    public class TarjetaAdicional : Solicitud
    {
        public string IdTarjeta { get; set; }
        public virtual ICollection<TarjetaAdicionalDetalle> TarjetasAdicionales { get; set; }
    }
}
