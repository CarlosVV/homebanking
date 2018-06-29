using Autofac;
using DinersClubOnline.FakeRepositories;

namespace DinersClubOnline.Auth.Resolver
{
    public class FakeLoginModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<LoginFakeService>().AsImplementedInterfaces();
        }
    }
}