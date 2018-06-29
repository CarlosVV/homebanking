//using System.ServiceModel;
using Autofac;
using DinersClubOnline.Repositories;
using DinersClubOnline.Repositories.Campanas;
using DinersClubOnline.Repositories.Solicitudes;

//using Autofac.Integration.Wcf;
//using DinersClubOnline.Services.Contracts.Seguridad;
//using DinersClubOnline.Services.Contracts.Tarjetas;
//using DinersClubOnline.Services.Contracts.Usuarios;

namespace DinersClubOnline.Resolver
{
    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TarjetaRepository>().AsImplementedInterfaces();
            builder.RegisterType<UsuarioRepository>().AsImplementedInterfaces();
            builder.RegisterType<OperadorTelefonicoRepository>().AsImplementedInterfaces();
            builder.RegisterType<EstadoDeCuentaRepository>().AsImplementedInterfaces();
            builder.RegisterType<MovimientoRepository>().AsImplementedInterfaces();
            builder.RegisterType<BancoRepository>().AsImplementedInterfaces();
            builder.RegisterType<TipoPagoRepository>().AsImplementedInterfaces();

            builder.RegisterType<CargoAutomaticoRepository>().AsImplementedInterfaces();
            builder.RegisterType<PrestamoPersonalRepository>().AsImplementedInterfaces();
            builder.RegisterType<DebitoAutomaticoRepository>().AsImplementedInterfaces();
            builder.RegisterType<OfertaRepository>().AsImplementedInterfaces();
            builder.RegisterType<DineroEfectivoRepository>().AsImplementedInterfaces();
            builder.RegisterType<AmpliacionCreditoRepository>().AsImplementedInterfaces();
            builder.RegisterType<CampanaRepository>().AsImplementedInterfaces();
            //builder.Register(c => new ChannelFactory<ISeguridadService>("BasicHttpBinding_ISeguridadService")).SingleInstance();
            //builder.Register(c => c.Resolve<ChannelFactory<ISeguridadService>>().CreateChannel()).As<ISeguridadService>().UseWcfSafeRelease();

            //builder.Register(c => new ChannelFactory<ITarjetasService>("BasicHttpBinding_ITarjetasService")).SingleInstance();
            //builder.Register(c => c.Resolve<ChannelFactory<ITarjetasService>>().CreateChannel()).As<ITarjetasService>().UseWcfSafeRelease();

            //builder.Register(c => new ChannelFactory<IUsuariosService>("BasicHttpBinding_IUsuariosService")).SingleInstance();
            //builder.Register(c => c.Resolve<ChannelFactory<IUsuariosService>>().CreateChannel()).As<IUsuariosService>().UseWcfSafeRelease();
        }
    }
}