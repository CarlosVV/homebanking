using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DinersClubOnline.Repositories.Solicitudes
{
    public class SolicitudContext : DbContext
    {
        public SolicitudContext()
            : base("DbSolicitud")
        { }

        public DbSet<Solicitud> Solicitudes { get; set; }

        public IQueryable<CargoAutomatico> CargosAutomaticos
        {
            get
            {
                return Solicitudes != null ? Solicitudes.OfType<CargoAutomatico>() : new List<CargoAutomatico>().AsQueryable();
            }
        }

        public IQueryable<DebitoAutomatico> DebitoAutomaticos
        {
            get
            {
                return Solicitudes != null ? Solicitudes.OfType<DebitoAutomatico>() : new List<DebitoAutomatico>().AsQueryable();
            }
        }

        public IQueryable<PrestamoPersonal> PrestamosPersonales
        {
            get
            {
                return Solicitudes != null ? Solicitudes.OfType<PrestamoPersonal>() : new List<PrestamoPersonal>().AsQueryable();
            }
        }

        public IQueryable<DineroEfectivo> DineroEfectivo
        {
            get
            {
                return Solicitudes != null ? Solicitudes.OfType<DineroEfectivo>() : new List<DineroEfectivo>().AsQueryable();
            }
        }

        public IQueryable<AmpliacionCredito> AmpliacionCreditos
        {
            get
            {
                return Solicitudes != null ? Solicitudes.OfType<AmpliacionCredito>() : new List<AmpliacionCredito>().AsQueryable();
            }
        }
        
		public IQueryable<Reclamo> Reclamos
        {
            get
            {
                return Solicitudes != null ? Solicitudes.OfType<Reclamo>() : new List<Reclamo>().AsQueryable();
            }
        }

		public IQueryable<TarjetaAdicional> TarjetasAdicionales
        {
            get
            {
                return Solicitudes != null ? Solicitudes.OfType<TarjetaAdicional>() : new List<TarjetaAdicional>().AsQueryable();
            }
        }

        #region Prevent References

        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded.
            //Make sure the provider assembly is available to the running application.
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.
            // TODO : dispose instance
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        #endregion Prevent References
    }
}