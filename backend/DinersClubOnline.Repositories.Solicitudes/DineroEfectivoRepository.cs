using DinersClubOnline.Model.Solicitudes;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class DineroEfectivoRepository : IDineroEfectivoRepository
    {
        public async Task<Model.Solicitudes.DineroEfectivo> ObtenerAsync(string id)
        {
            using (var ctx = new SolicitudContext())
            {
                var data = await ctx.DineroEfectivo.FirstOrDefaultAsync(r => r.IdSolicitud == id);

                if (data == null)
                    throw new Exception("Not Found");

                return new Model.Solicitudes.DineroEfectivo
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

        public async Task<SolicitudResult> GuardarAsync(Model.Solicitudes.DineroEfectivo dineroEfectivo)
        {
            var solicitud = new DineroEfectivo
            {
                IdSolicitud = dineroEfectivo.IdSolicitud,
                IdTipoOferta = dineroEfectivo.IdTipoOferta,
                IdBanco = dineroEfectivo.IdBanco,
                IdTarjeta = dineroEfectivo.IdTarjeta,
                Banco = dineroEfectivo.Banco,
                NumeroCuentaDestino = dineroEfectivo.NumeroCuentaDestino,
                MontoPrestamo = dineroEfectivo.MontoPrestamo,
                Cuotas = dineroEfectivo.Cuotas,
                Tcea = dineroEfectivo.Tcea,
                MontoCuota = dineroEfectivo.MontoCuota,
                TipoCuenta = dineroEfectivo.TipoCuenta,
                TipoMoneda = dineroEfectivo.TipoMoneda,
                FechaCreacion = dineroEfectivo.FechaCreacion,
                FechaActualizacion = dineroEfectivo.FechaActualizacion,
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