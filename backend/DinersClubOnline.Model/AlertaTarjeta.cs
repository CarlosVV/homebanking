using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public class AlertaTarjeta
    {
        public string IdAlerta { get; set; }
        public bool Celular1Activo { get; set; }
        public bool Celular2Activo { get; set; }
        public bool Email1Activo { get; set; }
        public bool Email2Activo { get; set; }
        public string IdCondicionAdicional { get; set; }
    }
}
