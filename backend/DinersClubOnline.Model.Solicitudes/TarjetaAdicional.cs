using System.Collections.Generic;

namespace DinersClubOnline.Model.Solicitudes
{
    public class TarjetaAdicional : Solicitud
    {
        public string IdTarjeta { get; set; }
        public List<TarjetaAdicionalDetalle> TarjetasAdicionales { get; set; }
    }
}
