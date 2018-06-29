using System.Collections.Generic;

namespace DinersClubOnline.Api.Models.Solicitudes
{
    public class TarjetaAdicionalViewModel
    {
        public string IdTipoOferta { get; set; }
        public string IdTarjeta { get; set; }
        public List<TarjetaAdicionalDetalleViewModel> TarjetasAdicionalesDetalle { get; set; }
        public List<Diccionario> DatosCorreo { get; set; }
        public string NombreProducto { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal LineaCreditoSoles { get; set; }
    }
}