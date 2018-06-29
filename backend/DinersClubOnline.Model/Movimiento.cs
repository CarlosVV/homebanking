using System;

namespace DinersClubOnline.Model
{
    public class Movimiento
    {
        public DateTime Fecha { get; set; }
        public string Descripcion { get; set; }
        public string Lugar { get; set; }
        public int Cuotas { get; set; }
        public decimal ImporteSoles { get; set; }
        public decimal ImporteDolares { get; set; }
        public bool PendienteProcesamiento { get; set; }


        public Tarjeta Tarjeta { get; set; }
    }
}