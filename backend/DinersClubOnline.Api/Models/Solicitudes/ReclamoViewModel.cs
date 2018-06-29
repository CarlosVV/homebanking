using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Api.Models.Solicitudes
{
    public class ReclamoViewModel
    {
        public string IdTipoOferta { get; set; }
        public string IdTarjeta { get; set; }
        public string TarjetaNumero { get; set; }

        public string Motivo { get; set; }

        public string Medio { get; set; }
        public string DireccionEnvio { get; set; }
        
        public string SolucionEsperada { get; set; }
        public string Descripcion { get; set; }
        
        public  List<Diccionario> DatosCorreo  { get; set; }

    }
}
