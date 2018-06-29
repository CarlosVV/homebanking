using DinersClubOnline.Model.Solicitudes;
using System;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class AmpliacionCreditoRepository : IAmpliacionCreditoRepository
    {
        public async Task<Model.Solicitudes.AmpliacionCredito> ObtenerAsync(string id)
        {
            using (var ctx = new SolicitudContext())
            {
                var data = await ctx.AmpliacionCreditos.FirstOrDefaultAsync(r => r.IdSolicitud == id);
                if (data == null)
                {
                    return null;
                }

                return new Model.Solicitudes.AmpliacionCredito
                {
                    IdSolicitud = data.IdSolicitud,
                    IdSocio = data.IdSocio,
                    IdTarjeta = data.IdTarjeta,
                    NumeroTarjeta = data.NumeroTarjeta,
                    CreditoActual = data.CreditoActual,
                    CreditoSolicitado = data.CreditoSolicitado
                   
                };
            }
        }

        public async Task<SolicitudResult> GuardarAsync(Model.Solicitudes.AmpliacionCredito modelo)
        {
            var solicitud = new AmpliacionCredito
            {
                IdSolicitud = modelo.IdSolicitud,
                IdSocio = modelo.IdSocio,
                IdTarjeta = modelo.IdTarjeta,
                IdTipoOferta = modelo.IdTipoOferta,
                NumeroTarjeta = modelo.NumeroTarjeta,
                FechaCreacion = modelo.FechaCreacion,
                FechaActualizacion = modelo.FechaActualizacion,
                CreditoActual = modelo.CreditoActual,
                CreditoSolicitado = modelo.CreditoSolicitado
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