using DinersClubOnline.Model.Solicitudes;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class PrestamoPersonalRepository : IPrestamoPersonalRepository
    {
        public async Task<Model.Solicitudes.PrestamoPersonal> ObtenerAsync(string id)
        {
            using (var ctx = new SolicitudContext())
            {
                var data = await ctx.PrestamosPersonales.FirstOrDefaultAsync(r => r.IdSolicitud == id);

                if (data == null)
                    throw new Exception("Not Found");

                return new Model.Solicitudes.PrestamoPersonal
                {
                    IdBanco = data.IdBanco,
                    IdTarjeta = data.IdTarjeta,
                    Banco = data.Banco,
                    NumeroCuentaDestino = data.NumeroCuentaDestino,
                    MontoPrestamo = data.MontoPrestamo,
                    Cuotas = data.Cuotas,
                    Tcea = data.Tcea,
                    MontoCuota = data.MontoCuota,
                    TipoCuenta = data.TipoCuenta,
                    TipoMoneda = data.TipoMoneda
                };
            }
        }

        public async Task<SolicitudResult> GuardarAsync(Model.Solicitudes.PrestamoPersonal prestamoPersonal)
        {
            var solicitud = new PrestamoPersonal
            {
                IdSolicitud = prestamoPersonal.IdSolicitud,
                IdTipoOferta = prestamoPersonal.IdTipoOferta,
                IdBanco = prestamoPersonal.IdBanco,
                IdTarjeta = prestamoPersonal.IdTarjeta,
                Banco = prestamoPersonal.Banco,
                NumeroCuentaDestino = prestamoPersonal.NumeroCuentaDestino,
                MontoPrestamo = prestamoPersonal.MontoPrestamo,
                Cuotas = prestamoPersonal.Cuotas,
                Tcea = prestamoPersonal.Tcea,
                MontoCuota = prestamoPersonal.MontoCuota,
                TipoCuenta = prestamoPersonal.TipoCuenta,
                TipoMoneda = prestamoPersonal.TipoMoneda,
                FechaCreacion = prestamoPersonal.FechaCreacion,
                FechaActualizacion = prestamoPersonal.FechaActualizacion,
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