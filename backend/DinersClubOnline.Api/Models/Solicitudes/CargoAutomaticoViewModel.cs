using System.Collections.Generic;

namespace DinersClubOnline.Api.Models.Solicitudes
{
    public class CargoAutomaticoViewModel
    {
        /// <summary>
        /// Identificador de Tipo de Oferta
        /// </summary>
        public string IdTipoOferta { get; set; }

        /// <summary>
        /// Identificador de tarjeta
        /// </summary>
        public string IdTarjeta { get; set; }

        public string TarjetaNumero { get; set; }

        public string TarjetaProducto { get; set; }

        public string TarjetaVence { get; set; }

        public string IdCategoria { get; set; }

        public string CategoriaNombre { get; set; }

        /// <summary>
        /// Identificador de la empresa
        /// </summary>
        public string IdEmpresa { get; set; }

        public string EmpresaNombre { get; set; }

        /// <summary>
        /// Identificador del servicio ofrecido por la empresa
        /// </summary>
        public string IdServicio { get; set; }
        
        public string ServicioNombre { get; set; }

        public string DatoServicio { get; set; }

        /// <summary>
        /// Monto tope a cargar en la tarjeta
        /// </summary>
        public decimal MontoTope { get; set; }

        public List<Diccionario> DatosCorreo { get; set; }
    }
}