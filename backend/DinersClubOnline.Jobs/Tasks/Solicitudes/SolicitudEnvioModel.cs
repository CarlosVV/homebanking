using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Jobs.Tasks.Solicitudes
{
    public class SolicitudEnvioModel
    {
        #region Solicitud

        public string IdSolicitud { get; set; }

        public string IdTipoOferta { get; set; }

        public int NumeroSolicitud { get; set; }

        public string DispositivoCreacion { get; set; }

        public string UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public string DispositivoActualizacion { get; set; }

        public string UsuarioActualizacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        #endregion

        #region Cargo Automatico
        public string IdSocio { get; set; }

        public string SocioNombres { get; set; }

        public string IdTarjeta { get; set; }

        public string TarjetaNumero { get; set; }

        public string IdEmpresa { get; set; }

        public string EmpresaNombres { get; set; }

        public string IdServicio { get; set; }

        public string ServicioNombre { get; set; }

        public decimal MontoTope { get; set; }

        #endregion

        #region Debito Automatico

        public string NumeroTarjeta { get; set; }

        public string TipoPagoaCargar { get; set; }

        public string NombreProducto { get; set; }

        public string IdBancoSoles { get; set; }

        public string BancoSoles { get; set; }

        public string TipoCuentaSoles { get; set; }

        public string MonedaDelaCtaSoles { get; set; }

        public string NumeroCuentaSoles { get; set; }

        public string IdBancoDolares { get; set; }

        public string BancoDolares { get; set; }

        public string TipoCuentaDolares { get; set; }

        public string MonedaDelaCtaDolares { get; set; }

        public string NumeroCuentaDolares { get; set; }
        #endregion

        #region Dinero Efectivo

        public string IdBanco { get; set; }

        public string NumeroCuentaDestino { get; set; }

        public decimal MontoPrestamo { get; set; }

        public int Cuotas { get; set; }

        public decimal Tcea { get; set; }

        public decimal MontoCuota { get; set; }

        public string TipoCuenta { get; set; }

        public string TipoMoneda { get; set; }
        #endregion

        #region Prestamo Personal


        #endregion
    }
}
