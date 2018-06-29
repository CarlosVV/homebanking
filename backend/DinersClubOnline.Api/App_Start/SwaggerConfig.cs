using System;
using System.Web.Http;
using DinersClubOnline.Api.Swagger;
using Swashbuckle.Application;

namespace DinersClubOnline.Api
{
    public class SwaggerConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("1", "Diners Club Online API");
                    c.GroupActionsBy(x => x.RelativePath.Split('?')[0].Split('/')[1]);
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    c.OperationFilter(()=> new AddAuthorizationHeaderParameterOperationFilter());
                })
                .EnableSwaggerUi();

        }

        protected static string GetXmlCommentsPath()
        {
            return string.Format(@"{0}\bin\DinersClubOnline.Api.XML", AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}