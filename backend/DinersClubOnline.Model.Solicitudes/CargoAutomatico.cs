using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model.Solicitudes
{
    public class CargoAutomatico : Solicitud
    {        
        public string IdSocio { get; set; }
        public string SocioNombres { get; set; }
        public string SocioTipoDocumento { get; set; }
        public string SocioNumeroDocumento { get; set; }
        public string SocioCorreElectronico { get; set; }
        public string SocioCelular { get; set; }

        public string IdTarjeta { get; set; }
        public string TarjetaNumero { get; set; }
        public string TarjetaProducto { get; set; }
        public string TarjetaVence { get; set; }

        public string IdCategoria { get; set; }
        public string CategoriaNombre { get; set; }

        public string IdEmpresa { get; set; }
        public string EmpresaNombre { get; set; }
        
        public string IdServicio { get; set; }
        public string ServicioNombre { get; set; }

        public string DatoServicio { get; set; }
        public decimal MontoTope { get; set; }
    }
}