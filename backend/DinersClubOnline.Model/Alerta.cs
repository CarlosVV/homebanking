using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public class Alerta
    {
        public string IdAlerta { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<AlertaCondicionAdicional> CondicionesAdicionales { get; set; }
    }

    public class AlertaCondicionAdicional
    {
        public string IdCondicionAdicional { get; set; }
        public string Nombre { get; set; }
    }
}
