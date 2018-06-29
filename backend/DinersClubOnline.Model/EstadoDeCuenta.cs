using System;

namespace DinersClubOnline.Model
{
    public class EstadoDeCuenta
    {
        //Periodo
        public DateTime FechaInicioPeriodo { get; set; }
        public DateTime FechaFinPeriodo { get; set; }

        public DateTime FechaVencimiento { get; set; }
        public bool Vencido { get; set; }

        //Resumen
        public decimal DeudaAnteriorSoles { get; set; }
        public decimal DeudaAnteriorDolares { get; set; }
        public decimal AbonosSoles { get; set; }
        public decimal AbonosDolares { get; set; }
        public decimal ConsumosSoles { get; set; }
        public decimal ConsumosDolares { get; set; }
        public decimal ComisionesSoles { get; set; }
        public decimal ComisionesDolares { get; set; }
        public decimal DeudaTotalSoles { get; set; }
        public decimal DeudaTotalDolares { get; set; }

        //Millas
        public int MillasSaldoAnterior { get; set; }
        public int MillasGanadas { get; set; }
        public int MillasVencidas { get; set; }
        public int MillasDisponibles { get; set; }
    }
}