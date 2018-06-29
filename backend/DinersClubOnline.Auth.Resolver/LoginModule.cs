using Autofac;
//using Autofac.Integration.Wcf;
using DinersClubOnline.Repositories;

namespace DinersClubOnline.Auth.Resolver
{
    public class LoginModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register(c => new ChannelFactory<ISeguridadService>("BasicHttpBinding_ISeguridadService")).SingleInstance();
            //builder.Register(c => c.Resolve<ChannelFactory<ISeguridadService>>().CreateChannel()).As<ISeguridadService>().UseWcfSafeRelease();

            builder.RegisterType<LoginService>().AsImplementedInterfaces();
        }
    }
}