using System;

namespace DinersClubOnline.Api.Models
{
    public class CorreoFiltrosViewModel
    {
        public string Correo { get; set; }
        public string Id { get; set; }
        public string IdTarjetaConsulta { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}