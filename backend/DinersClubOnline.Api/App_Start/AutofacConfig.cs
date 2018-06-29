using System;
using System.Configuration;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DinersClubOnline.Resolver;

namespace DinersClubOnline.Api
{
    public static class AutofacConfig
    {
        public static void Configure()
        {

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);


            var useSoapModuleSetting = ConfigurationManager.AppSettings["UseSoapModule"];
            bool useSoapModuleValue;
            if (Boolean.TryParse(useSoapModuleSetting, out useSoapModuleValue) && useSoapModuleValue)
            {
                builder.RegisterModule<RepositoryModule>();
            }
            else
            {
                builder.RegisterModule<FakeRepositoryModule>();
            }

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}