using Autofac;
using DinersClubOnline.Services.Client.Common;

namespace DinersClubOnline.Repositories.Tests
{
    public class SetupIoC 
    {
        private static IContainer _autofacContainer;
        public static IContainer AutofacContainer
        {
            get
            {
                if (_autofacContainer != null) return _autofacContainer;

                var builder = new ContainerBuilder();
                
                //Clients
                builder.RegisterType<SeguridadServiceClientBase>().AsImplementedInterfaces();
                builder.RegisterType<TarjetaServiceClientBase>().AsImplementedInterfaces();

                //Repositories and services
                builder.RegisterType<TarjetaRepository>().AsImplementedInterfaces();
                builder.RegisterType<UsuarioRepository>().AsImplementedInterfaces();
                builder.RegisterType<EstadoDeCuentaRepository>().AsImplementedInterfaces();
                builder.RegisterType<LoginService>().AsImplementedInterfaces();

                _autofacContainer = builder.Build();

                return _autofacContainer;
            }
        }
    }
}
