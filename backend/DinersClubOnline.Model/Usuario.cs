namespace DinersClubOnline.Model
{
    /// <summary>Usuario del sistema Diners Club Online</summary>
    public class Usuario
    {
        public string Id { get; set; }

        //Datos adicionales
        public string Alias { get; set; }
        public string NumeroCelular { get; set; }
        public string IdImagen { get; set; }
        public string IdFacebook { get; set; }

        //Alertas
        public string EmailPrincipal { get; set; }
        public string EmailAlternativo { get; set; }
        public string EmailSeleccionado { get; set; }

        //Seguridad

        public string ClaveDigital { get; set; }

        //Relaciones
        public Socio Socio { get; set; }
        public OperadorTelefonico OperadorTelefonico { get; set; }
    }
}