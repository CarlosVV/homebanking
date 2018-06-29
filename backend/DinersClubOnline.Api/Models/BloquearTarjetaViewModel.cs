using DinersClubOnline.Api.Models.Solicitudes;
using System.Collections.Generic;

namespace DinersClubOnline.Api.Models
{
    public class BloquearTarjetaViewModel
    {
        public string IdMotivo { get; set; }
        public string NombreProducto { get; set; }
        public string NumeroTarjeta { get; set; }
        public string FormatoNombreTarjeta { get; set; }
        public bool EsTitular { get; set; }
        public string NombreTarjetaHabiente { get; set; }
        public string ApellidoPaternoTarjetaHabiente { get; set; }
    }
}