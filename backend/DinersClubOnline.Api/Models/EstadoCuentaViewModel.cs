using System;

namespace DinersClubOnline.Api.Models
{
    public class EstadoCuentaViewModel
    {
        public DateTime FechaInicioPeriodo { get; set; }

        public DateTime FechaFinPeriodo { get; set; }

        public DateTime FechaVencimiento { get; set; }

        public bool EstaVencido { get; set; }

        public decimal DeudaAnteriorSoles { get; set; }

        public decimal DeudaAnteriorDolares { get; set; }

        public decimal AbonoSoles { get; set; }

        public decimal AbonoDolares { get; set; }

        public decimal ConsumosSoles { get; set; }

        public decimal ConsumosDolares { get; set; }

        public decimal ComisionesInteresesPenalidadesGastosSoles { get; set; }

        public decimal ComisionesInteresesPenalidadesGastosDolares { get; set; }

        public decimal DeudaTotalSoles { get; set; }

        public decimal DeudaTotalDolares { get; set; }

        public decimal MontoAPagarMinimoSoles { get; set; }

        public decimal MontoAPagarMinimoDolares { get; set; }

        public decimal MontoAPagarTotalSoles { get; set; }

        public decimal MontoAPagarTotalDolares { get; set; }

        public int MillasSaldoAnterior { get; set; }

        public int MillasGanadas { get; set; }

        public int MillasVencidas { get; set; }

        public int MillasDisponibles { get; set; }

        public string IdTarjeta { get; set; }

        public bool AcumulaMillas { get; set; }
    }
}