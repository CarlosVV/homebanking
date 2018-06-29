namespace DinersClubOnline.Model.Solicitudes
{
    public class PrestamoPersonal : Solicitud
    {
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
    }
}
