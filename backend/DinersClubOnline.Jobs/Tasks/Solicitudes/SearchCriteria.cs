using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinersClubOnline.Jobs.Tasks.Solicitudes
{
    public class SearchCriteria
    {
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? TipoSolicitud { get; set; }
    }
}
