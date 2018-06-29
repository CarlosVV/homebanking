using System;

namespace DinersClubOnline.Model.Solicitudes
{
    public class TarjetaAdicionalDetalle
    {
        public string Nombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroTelefono { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Correo { get; set; }
        public string NombreTarjeta { get; set; }
        public decimal TopeConsumoMensual { get; set; }
    }
}
