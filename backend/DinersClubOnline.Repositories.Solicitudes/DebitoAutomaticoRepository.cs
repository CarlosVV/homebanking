using DinersClubOnline.Model.Solicitudes;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class DebitoAutomaticoRepository : IDebitoAutomaticoRepository
    {
        public async Task<Model.Solicitudes.DebitoAutomatico> ObtenerAsync(string id)
        {
            using (var ctx = new SolicitudContext())
            {
                var data = await ctx.DebitoAutomaticos.FirstOrDefaultAsync(r => r.IdSolicitud == id);
                if (data == null)
                {
                    return null;
                }

                return new Model.Solicitudes.DebitoAutomatico
                {
                    IdSolicitud = data.IdSolicitud,
                    IdSocio = data.IdSocio,
                    IdTarjeta = data.IdTarjeta,
                    NumeroTarjeta = data.NumeroTarjeta,
                    TipoPagoaCargar = data.TipoPagoaCargar,
                    NombreProducto = data.NombreProducto,
                    IdBancoSoles = data.IdBancoSoles,
                    BancoSoles = data.BancoSoles,
                    TipoCuentaSoles = data.TipoCuentaSoles,
                    MonedaDelaCtaSoles = data.MonedaDelaCtaSoles,
                    NumeroCuentaSoles = data.NumeroCuentaSoles,
                    IdBancoDolares = data.IdBancoDolares,
                    BancoDolares = data.BancoDolares,
                    TipoCuentaDolares = data.TipoCuentaDolares,
                    MonedaDelaCtaDolares = data.MonedaDelaCtaDolares,
                    NumeroCuentaDolares = data.NumeroCuentaDolares
                };
            }
        }

        public async Task<SolicitudResult> GuardarAsync(Model.Solicitudes.DebitoAutomatico modelo)
        {
            var solicitud = new DebitoAutomatico
            {
                IdSolicitud = modelo.IdSolicitud,
                IdSocio = modelo.IdSocio,
                IdTarjeta = modelo.IdTarjeta,
                IdTipoOferta = modelo.IdTipoOferta,
                NumeroTarjeta = modelo.NumeroTarjeta,
                TipoPagoaCargar = modelo.TipoPagoaCargar,
                NombreProducto = modelo.NombreProducto,
                IdBancoSoles = modelo.IdBancoSoles,
                BancoSoles = modelo.BancoSoles,
                TipoCuentaSoles = modelo.TipoCuentaSoles,
                MonedaDelaCtaSoles = modelo.MonedaDelaCtaSoles,
                NumeroCuentaSoles = modelo.NumeroCuentaSoles,
                IdBancoDolares = modelo.IdBancoDolares,
                BancoDolares = modelo.BancoDolares,
                TipoCuentaDolares = modelo.TipoCuentaDolares,
                MonedaDelaCtaDolares = modelo.MonedaDelaCtaDolares,
                NumeroCuentaDolares = modelo.NumeroCuentaDolares,
                FechaCreacion = modelo.FechaCreacion,
                FechaActualizacion = modelo.FechaActualizacion
            };

            using (var ctx = new SolicitudContext())
            {
                ctx.Solicitudes.Add(solicitud);

                var result = await ctx.SaveChangesAsync() > 0;

                return result ?
                    SolicitudResult.CreateSolicitudRegistradaResult(solicitud.NumeroSolicitud, solicitud.FechaCreacion) :
                    SolicitudResult.CreateErrorResult();
            }
        }
    }
}