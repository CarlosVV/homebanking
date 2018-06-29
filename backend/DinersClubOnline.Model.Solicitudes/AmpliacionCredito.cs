namespace DinersClubOnline.Model.Solicitudes
{
    public class AmpliacionCredito : Solicitud
    {
        public string IdSocio { get; set; }

        public string IdTarjeta { get; set; }

        public string NumeroTarjeta { get; set; }

        public string NombreProducto { get; set; }

        public decimal CreditoActual { get; set; }

        public decimal CreditoSolicitado { get; set; }
    }
}