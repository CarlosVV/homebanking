using System.Collections.Generic;

namespace DinersClubOnline.Api.Models.Solicitudes
{
    public class DebitoAutomaticoViewModel
    {
        public string IdSocio { get; set; }

        public string IdTarjeta { get; set; }

        public string NumeroTarjeta { get; set; }

        public string TipoPagoaCargar { get; set; }

        public string NombreProducto { get; set; }

        public string IdTipoOferta { get; set; }

        public DebitoAutomaticoFacturacionViewModel FacturacionSoles { get; set; }

        public DebitoAutomaticoFacturacionViewModel FacturacionDolares { get; set; }

        public List<Diccionario> DatosCorreo { get; set; }
    }
}