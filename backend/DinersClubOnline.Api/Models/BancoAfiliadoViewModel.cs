namespace DinersClubOnline.Api.Models
{
    public class BancoAfiliadoViewModel
    {
        public string IdBanco { get; set; }
        public string NombreBanco { get; set; }
        public bool PagoVentanilla { get; set; }
        public bool PagoInternet { get; set; }
        public bool PagoCargoEnCuenta { get; set; }
        public string UrlArchivo { get; set; }
    }
}