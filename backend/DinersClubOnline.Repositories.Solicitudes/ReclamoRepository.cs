using DinersClubOnline.Model.Solicitudes;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class ReclamoRepository : IReclamoRepository
    {
        public Task<Model.Solicitudes.Reclamo> ObtenerAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<SolicitudResult> GuardarAsync(Model.Solicitudes.Reclamo modelo)
        {
            var solicitud = new DinersClubOnline.Repositories.Solicitudes.Reclamo
            {
                IdSolicitud = modelo.IdSolicitud,
                IdTipoOferta = modelo.IdTipoOferta,

                IdSocio = modelo.IdSocio,
                SocioNombres = modelo.SocioNombres,
                SocioTipoDocumento = modelo.SocioTipoDocumento,
                SocioNumeroDocumento = modelo.SocioNumeroDocumento,
                SocioCorreElectronico = modelo.SocioCorreElectronico,
                SocioCelular = modelo.SocioCelular,

                IdTarjeta = modelo.IdTarjeta,
                TarjetaNumero = modelo.TarjetaNumero,

                Motivo = modelo.Motivo,
                Medio = modelo.Medio,
                DireccionEnvio = modelo.DireccionEnvio,
                Descripcion = modelo.Descripcion,
                SolucionEsperada = modelo.SolucionEsperada,

                FechaCreacion = modelo.FechaCreacion,
                FechaActualizacion = modelo.FechaActualizacion,
            };

            using (var ctx = new SolicitudContext())
            {
                ctx.Solicitudes.Add(solicitud);
                var result = await ctx.SaveChangesAsync() > 0;

                return result
                    ? SolicitudResult.CreateSolicitudRegistradaResult(solicitud.NumeroSolicitud, solicitud.FechaCreacion)
                    : SolicitudResult.CreateErrorResult();
            }
        }
    }
}
