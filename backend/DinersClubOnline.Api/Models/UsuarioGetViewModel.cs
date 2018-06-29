namespace DinersClubOnline.Api.Models
{
    public class UsuarioGetViewModel
    {
        public string IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string SegundoNombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string EmailPrincipal { get; set; }

        public string EmailAlternativo { get; set; }

        public string EmailSeleccionado { get; set; }

        public string IdOperadorTelefonico { get; set; }

        public string NumeroCelular { get; set; }

        public string IdImagen { get; set; }

        public string IdFacebook { get; set; }
        public string NumeroDocumentoIdentidad { get; set; }
        public string TipoDocumentoIdentidad { get; set; }

        public System.DateTime FechaNacimiento { get; set; }

        public string Sexo { get; set; }

        public string EstadoCivil { get; set; }

        public string Procedencia { get; set; }

        public string Alias { get; set; }
    }
}