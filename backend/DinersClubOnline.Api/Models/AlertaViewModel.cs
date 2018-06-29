using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Api.Models
{
    public class AlertaViewModel
    {
        public string IdAlerta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public IEnumerable<AlertaCondicionAdicionalViewModel> CondicionesAdicionales { get; set; }
    }

    public class AlertaCondicionAdicionalViewModel
    {
        public string IdCondicionAdicional { get; set; }
        public string Nombre { get; set; }
    }
}
