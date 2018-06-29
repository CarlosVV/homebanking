using DinersClubOnline.Jobs.Tasks.Solicitudes.Interfaces;
using DinersClubOnline.Repositories.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DinersClubOnline.Jobs.Tasks.Solicitudes
{
    public class DataHandler : IDataHandler, IDisposable
    {
        private readonly SolicitudContext context = new SolicitudContext();

        public IEnumerable<SolicitudEnvioModel> GetData(SearchCriteria criteria)
        {
            var filterByToday = false;
            var result = new List<SolicitudEnvioModel>();

            var solCargosAutomaticos = from item in context.CargosAutomaticos
                                       where (!filterByToday || item.FechaCreacion >= DateTime.Today)
                                       select new SolicitudEnvioModel
                                       {
                                           IdSolicitud = item.IdSolicitud,
                                           IdTipoOferta = item.IdTipoOferta,
                                           NumeroSolicitud = item.NumeroSolicitud,
                                           IdSocio = item.IdSocio,
                                           SocioNombres = item.SocioNombres,
                                           IdTarjeta = item.IdTarjeta,
                                           TarjetaNumero = item.TarjetaNumero,
                                           IdEmpresa = item.IdEmpresa,
                                           EmpresaNombres = item.EmpresaNombre,
                                           IdServicio = item.IdServicio,
                                           ServicioNombre = item.ServicioNombre,
                                           MontoTope = item.MontoTope
                                       };

            var solDebitosAutomaticos = from item in context.DebitoAutomaticos
                                        where (!filterByToday || item.FechaCreacion >= DateTime.Today)
                                        select new SolicitudEnvioModel
                                        {
                                            IdSolicitud = item.IdSolicitud,
                                            IdTipoOferta = item.IdTipoOferta,
                                            NumeroSolicitud = item.NumeroSolicitud,
                                            IdSocio = item.IdSocio,
                                            IdTarjeta = item.IdTarjeta,
                                            NumeroTarjeta = item.NumeroTarjeta,
                                            TipoPagoaCargar = item.TipoPagoaCargar,
                                            NombreProducto = item.NombreProducto,
                                            IdBancoSoles = item.IdBancoSoles,
                                            BancoSoles = item.BancoSoles,
                                            MonedaDelaCtaSoles = item.MonedaDelaCtaSoles,
                                            NumeroCuentaSoles = item.NumeroCuentaSoles,
                                            TipoCuentaSoles = item.TipoCuentaSoles,
                                            IdBancoDolares = item.IdBancoDolares,
                                            BancoDolares = item.BancoDolares,
                                            TipoCuentaDolares = item.TipoCuentaDolares,
                                            MonedaDelaCtaDolares = item.MonedaDelaCtaDolares,
                                            NumeroCuentaDolares = item.NumeroCuentaDolares
                                        };

            var solDineroEfectivo = from item in context.DineroEfectivo
                                    where (!filterByToday || item.FechaCreacion >= DateTime.Today)
                                    select new SolicitudEnvioModel
                                    {
                                        IdSolicitud = item.IdSolicitud,
                                        IdTipoOferta = item.IdTipoOferta,
                                        NumeroSolicitud = item.NumeroSolicitud,
                                        IdBanco = item.IdBanco,
                                        IdTarjeta = item.IdTarjeta,
                                        NumeroCuentaDestino = item.NumeroCuentaDestino,
                                        MontoPrestamo = item.MontoPrestamo,
                                        Cuotas = item.Cuotas,
                                        Tcea = item.Tcea,
                                        MontoCuota = item.MontoCuota,
                                        TipoCuenta = item.TipoCuenta,
                                        TipoMoneda = item.TipoMoneda
                                    };

            var solPrestamosPersonales = from item in context.PrestamosPersonales
                                         where (!filterByToday || item.FechaCreacion >= DateTime.Today)
                                         select new SolicitudEnvioModel
                                         {
                                             IdSolicitud = item.IdSolicitud,
                                             IdTipoOferta = item.IdTipoOferta,
                                             NumeroSolicitud = item.NumeroSolicitud,
                                             IdBanco = item.IdBanco,
                                             IdTarjeta = item.IdTarjeta,
                                             NumeroCuentaDestino = item.NumeroCuentaDestino,
                                             MontoPrestamo = item.MontoPrestamo,
                                             Cuotas = item.Cuotas,
                                             Tcea = item.Tcea,
                                             MontoCuota = item.MontoCuota,
                                             TipoCuenta = item.TipoCuenta,
                                             TipoMoneda = item.TipoMoneda
                                         };

            result.AddRange(solCargosAutomaticos);
            result.AddRange(solDebitosAutomaticos);
            result.AddRange(solDineroEfectivo);
            result.AddRange(solPrestamosPersonales);

            return result;
        }

        public bool UpdateSentData(IEnumerable<SolicitudEnvioModel> data)
        {
            var sols = (from item in data select new { item.IdSolicitud }).ToArray();
            var solicitudes = new List<Solicitud>();
            foreach (var sol in sols)
            {
                var item = context.Solicitudes.Find(sol);
                item.Enviado = true;
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                solicitudes.Add(item);
            }

            context.SaveChanges();

            return true;
        }

        #region IDisposable Support

        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        ~DataHandler()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}