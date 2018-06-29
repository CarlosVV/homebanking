using System;

namespace DinersClubOnline.Model
{
    public class Oferta
    {
        public string IdTipoOferta { get; set; }
        public string IdTarjeta { get; set; }
        public decimal MontoLineaNueva { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal MontoOferta { get; set; }
        public decimal TEA { get; set; }
        public decimal TCEA { get; set; }
    }
}