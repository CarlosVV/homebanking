namespace DinersClubOnline.Api.Models.Solicitudes
{
    public class DebitoAutomaticoFacturacionViewModel
    {
        public string IdBanco { get; set; }

        public string Banco { get; set; }

        public string TipoCuenta { get; set; }

        public string MonedaDelaCta { get; set; }

        public string NumeroCuenta { get; set; }
    }
}