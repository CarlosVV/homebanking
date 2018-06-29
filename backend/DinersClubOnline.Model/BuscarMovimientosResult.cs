using System.Collections.Generic;

namespace DinersClubOnline.Model
{
    public class BuscarMovimientosResult
    {
        public int TotalMovimientos { get; set; }
        public List<Movimiento> Resultados { get; set; }
    }
}