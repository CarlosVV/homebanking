using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class Solicitud
    {
        [Key]
        public string IdSolicitud { get; set; }

        public string IdTipoOferta { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumeroSolicitud { get; set; }

        /// <summary>
        /// Enviado, No Enviado
        /// </summary>
        public bool Enviado { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}