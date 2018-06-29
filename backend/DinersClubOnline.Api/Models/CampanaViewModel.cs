using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinersClubOnline.Api.Models
{
    public class CampanaViewModel
    {
        public string Id { get; set; }
        public string Imagen { get; set; }
        public string Banner { get; set; }
        public int Prioridad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ActualizadoPor { get; set; }
        public DateTime FechaActualizacion { get; set; }
        public bool Activo { get; set; }
    }
}
