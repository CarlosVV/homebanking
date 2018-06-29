namespace DinersClubOnline.Api.Models
{
    /// <summary>Usuario a registrar</summary>
    public class UsuarioPostViewModel
    {
        /// <summary>Clave digital a crear</summary>
        public string ClaveDigital { get; set; }

        /// <summary>Id de la imagen a registrar. Debe ser un número entre 0 y 8, si es 0 corresponde a la imagen de Facebook</summary>
        public string IdImagen { get; set; }

        /// <summary>Id de facebook del usuario. (Opcional)</summary>
        public string IdFacebook { get; set; }

        /// <summary>Email del usuario a registrar</summary>
        public string Email { get; set; }

        /// <summary>Id del operador telefónico del usuario a registrar</summary>
        public string IdOperadorTelefonico { get; set; }

        /// <summary>Numero de celular del usuario a registrar</summary>
        public string NumeroCelular { get; set; }
    }
}