using System;
using System.Collections.Generic;

namespace DinersClubOnline.Api.Models
{
    public class TarjetaGetViewModel
    {
        public string IdTarjeta { get; set; }
        public string NumeroTarjeta { get; set; }
        public string Alias { get; set; }
        public string NombreProducto { get; set; }
        public decimal LineaCreditoSoles { get; set; }
        public decimal LineaCreditoDolares { get; set; }
        public decimal LineaDisponibleSoles { get; set; }
        public decimal LineaDisponibleDolares { get; set; }
        public int? MillasDisponibles { get; set; }
        public decimal MontoTotalMesSoles { get; set; }
        public decimal MontoTotalMesDolares { get; set; }
        public decimal MontoMinimoMesSoles { get; set; }
        public decimal MontoMinimoMesDolares { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public bool Pagado { get; set; }
        public SocioViewModel SocioTarjeta { get; set; }
        public bool AcumulaMillas { get; set; }
        public List<TarjetaGetViewModel> Adicionales { get; set; }
        public bool TieneAdicionales { get; set; }
        public EstadoTarjeta EstadoTarjeta { get; set; }
        public int NumeroOperacion { get; set; }
        public DateTime FechaOperacion { get; set; }
    }
}