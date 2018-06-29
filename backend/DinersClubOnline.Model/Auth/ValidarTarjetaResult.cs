namespace DinersClubOnline.Model.Auth
{
    public class ValidarTarjetaResult
    {
        public bool EsValido { get; set; }
        public string IdTarjeta { get; set; }
        public bool TieneClaveDigital { get; set; }
        public string Nombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
    }
}