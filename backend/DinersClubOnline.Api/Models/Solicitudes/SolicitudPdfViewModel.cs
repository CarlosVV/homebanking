using System.Collections.Generic;

namespace DinersClubOnline.Api.Models.Solicitudes
{
    public class SolicitudPdfViewModel
    {
        public string Titulo { get; set; }
        public string Socio { get; set; }
        public List<List<string>> Datos { get; set; }
    }
}