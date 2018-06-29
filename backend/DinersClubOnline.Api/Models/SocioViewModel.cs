namespace DinersClubOnline.Api.Models
{
    public class SocioViewModel
    {
        /// <summary>Nombre del socio</summary>
        public string Nombre { get; set; }

        /// <summary>Segundo nombre del socio</summary>
        public string SegundoNombre { get; set; }

        /// <summary>Apellido paterno del socio</summary>
        public string ApellidoPaterno { get; set; }

        /// <summary>Apellido materno del socio</summary>
        public string ApellidoMaterno { get; set; }

        public string IdTarjeta { get; set; }

        public string FechaNacimiento { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string Procedencia { get; set; }
    }
}