using System;
namespace DinersClubOnline.Model
{
    public class BloquearTarjetaResult
    {
        public bool Result { get; set; }
        public string Error { get; set; }
        public string Mensaje { get; set; }
        public int NumeroOperacion { get; set; }
        public DateTime FechaOperacion { get; set; }
    }
}
