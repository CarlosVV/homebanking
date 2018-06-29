namespace DinersClubOnline.Api.Models
{
    public class UsuarioPatchViewModel
    {
        /// <summary>Email del usuario a registrar</summary>
        public string Email { get; set; }

        /// <summary>Id del operador telefónico del usuario a registrar</summary>
        public string IdOperadorTelefonico { get; set; }

        /// <summary>Numero de celular del usuario a registrar</summary>
        public string NumeroCelular { get; set; }
        /// <summary>
        /// Email Alternativo o Secundario
        /// </summary>
        public string emailAlternativo { get; set; }
        /// <summary>
        /// Email Principal o Primario
        /// </summary>
        public string emailPrincipal { get; set; }
    }
}