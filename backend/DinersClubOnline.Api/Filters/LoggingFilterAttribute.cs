using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Web.Http.Tracing;
using System.Web.Http;
using DinersClubOnline.Api.Helpers;
using System.Net.Http;

namespace DinersClubOnline.Api.Filters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(filterContext.Request, "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName + Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName, "JSON", filterContext.ActionArguments);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var objectContent = actionExecutedContext.Response.Content as ObjectContent;
            if (objectContent != null)
            {
                var type = objectContent.ObjectType;
                var value = objectContent.Value;
                GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
                var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
                trace.Info(actionExecutedContext.Request, "Response : " + actionExecutedContext.Response.StatusCode.ToString() + " Value:" + value, type.ToString());
            }           
        }

    }
}