using DinersClubOnline.Model.Solicitudes;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class CargoAutomaticoRepository : ICargoAutomaticoRepository
    {
        public async Task<Model.Solicitudes.CargoAutomatico> ObtenerAsync(string id)
        {
            using (var ctx = new SolicitudContext())
            {
                var data = await ctx.CargosAutomaticos.FirstOrDefaultAsync(r => r.IdSolicitud == id);
                if (data == null)
                    throw new Exception("Not Found");

                return new Model.Solicitudes.CargoAutomatico
                {
                    IdSolicitud = data.IdSolicitud,
                    IdTipoOferta = data.IdTipoOferta,
                    IdSocio = data.IdSocio,
                    IdEmpresa = data.IdEmpresa,
                    IdServicio = data.IdServicio,
                    IdTarjeta = data.IdTarjeta,
                    MontoTope = data.MontoTope
                };
            }
        }

        public async Task<SolicitudResult> GuardarAsync(Model.Solicitudes.CargoAutomatico modelo)
        {
            var solicitud = new CargoAutomatico
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
                TarjetaProducto = modelo.TarjetaProducto,
                TarjetaVence = modelo.TarjetaVence,

                IdCategoria = modelo.IdCategoria,
                CategoriaNombre = modelo.CategoriaNombre,

                IdEmpresa = modelo.IdEmpresa,
                EmpresaNombre = modelo.EmpresaNombre,

                IdServicio = modelo.IdServicio,
                ServicioNombre = modelo.ServicioNombre,

                DatoServicio = modelo.DatoServicio,
                MontoTope = modelo.MontoTope,

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