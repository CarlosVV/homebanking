using DinersClubOnline.Model.Solicitudes;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class TarjetaAdicionalRepository : ITarjetaAdicionalRepository
    {
        public async Task<Model.Solicitudes.TarjetaAdicional> ObtenerAsync(string id)
        {
            using (var ctx = new SolicitudContext())
            {
                var data = await ctx.TarjetasAdicionales.FirstOrDefaultAsync(r => r.IdSolicitud == id);
                if (data == null)
                    throw new Exception("Not Found");

                return new Model.Solicitudes.TarjetaAdicional 
                {
                    IdSolicitud = data.IdSolicitud
                };
            }
        }

        public async Task<SolicitudResult> GuardarAsync(Model.Solicitudes.TarjetaAdicional modelo)
        {
            var solicitud = new TarjetaAdicional
            {
                IdSolicitud = modelo.IdSolicitud,
                IdTipoOferta = modelo.IdTipoOferta,
                IdTarjeta = modelo.IdTarjeta,
                TarjetasAdicionales = modelo.TarjetasAdicionales.Select(r => new TarjetaAdicionalDetalle 
                {
                    IdSolicitud = modelo.IdSolicitud,
                    Nombre = r.Nombre,
                    SegundoNombre = r.SegundoNombre,
                    ApellidoPaterno = r.ApellidoPaterno,
                    ApellidoMaterno = r.ApellidoMaterno,
                    TipoDocumento = r.TipoDocumento,
                    NumeroDocumento = r.NumeroDocumento,
                    NumeroTelefono = r.NumeroTelefono,
                    FechaNacimiento = r.FechaNacimiento,
                    Correo = r.Correo,
                    NombreTarjeta = r.NombreTarjeta,
                    TopeConsumoMensual = r.TopeConsumoMensual
                }).ToList(),
                FechaCreacion = modelo.FechaCreacion
            };

            using (var ctx = new SolicitudContext())
            {
                try
                {
                    ctx.Solicitudes.Add(solicitud);
                    var result = await ctx.SaveChangesAsync() > 0;
                    return result
                    ? SolicitudResult.CreateSolicitudRegistradaResult(solicitud.NumeroSolicitud, solicitud.FechaCreacion)
                    : SolicitudResult.CreateErrorResult();
                }
                catch (Exception) { throw; }                
            }
        }
    }
}
