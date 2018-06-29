using System.Collections.Generic;
namespace DinersClubOnline.Api.Models.Solicitudes
{
    public class DineroEfectivoViewModel
    {
        public string IdTipoOferta { get; set; }
        public string IdBanco { get; set; }
        public string IdTarjeta { get; set; }
        public string Banco { get; set; }
        public string NumeroCuentaDestino { get; set; }
        public decimal MontoPrestamo { get; set; }
        public int Cuotas { get; set; }
        public decimal Tcea { get; set; }
        public decimal MontoCuota { get; set; }
        public string TipoCuenta { get; set; }
        public string TipoMoneda { get; set; }
        public string NombreProducto { get; set; }
        public string NumeroTarjeta { get; set; }
        public List<Diccionario> DatosCorreo { get; set; }
    }
}