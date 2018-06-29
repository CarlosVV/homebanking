using System;
using System.Configuration;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DinersClubOnline.Auth.Resolver;

namespace DinersClubOnline.Auth
{
    public static class AutofacConfig
    {
        public static void Configure()
        {
            var config = GlobalConfiguration.Configuration;

            var builder = new ContainerBuilder();

            builder.RegisterWebApiFilterProvider(config);

            var useSoapModuleSetting = ConfigurationManager.AppSettings["UseSoapModule"];
            bool useSoapModuleValue;
            if (Boolean.TryParse(useSoapModuleSetting, out useSoapModuleValue) && useSoapModuleValue)
            {
                builder.RegisterModule<LoginModule>();
            }
            else
            {
                builder.RegisterModule<FakeLoginModule>();
            }

            config.DependencyResolver = new AutofacWebApiDependencyResolver(builder.Build());
        }
    }
}