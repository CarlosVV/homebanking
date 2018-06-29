using System.Collections.Generic;

namespace DinersClubOnline.Api.Models
{
    public class ResumenMovimientoViewModel
    {
        public decimal TotalConsumidoSoles { get; set; }
        public decimal TotalConsumidoDolares { get; set; }
        public decimal AbonosSoles { get; set; }
        public decimal AbonosDolares { get; set; }
        public int TotalMovimientos { get; set; }
        public IEnumerable<MovimientoViewModel> Movimientos { get; set; }
    }
}