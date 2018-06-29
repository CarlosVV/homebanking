using Autofac;
using DinersClubOnline.FakeRepositories;
using DinersClubOnline.Repositories.Campanas;
using DinersClubOnline.Repositories.Solicitudes;

namespace DinersClubOnline.Resolver
{
    public class FakeRepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TarjetaFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<UsuarioFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<OperadorTelefonicoFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<MovimientoFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<EstadoDeCuentaFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<BancoFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<TipoPagoFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<CargoAutomaticoRepository>().AsImplementedInterfaces();
            builder.RegisterType<PrestamoPersonalRepository>().AsImplementedInterfaces();
            builder.RegisterType<CategoriaFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<EmpresaFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<ServicioFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<DebitoAutomaticoRepository>().AsImplementedInterfaces();
            builder.RegisterType<DineroEfectivoRepository>().AsImplementedInterfaces();
            builder.RegisterType<OfertaFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<AmpliacionCreditoRepository>().AsImplementedInterfaces();
            builder.RegisterType<ReclamoRepository>().AsImplementedInterfaces();
            builder.RegisterType<TarjetaAdicionalRepository>().AsImplementedInterfaces();
            builder.RegisterType<TipoDocumentoFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<BancoAfiliadoFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<PrivilegioFakeRepository>().AsImplementedInterfaces();
			builder.RegisterType<AlertaFakeRepository>().AsImplementedInterfaces();
            builder.RegisterType<CampanaRepository>().AsImplementedInterfaces();
        }
    }
}