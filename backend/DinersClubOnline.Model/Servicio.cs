using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public class Servicio
    {
        public string IdServicio { get; set; }
        public string NombreServicio { get; set; }
        //todo: se puede quitar
        public string IdEmpresa { get; set; }
        public string ParametroServicio { get; set; }
    }
}
