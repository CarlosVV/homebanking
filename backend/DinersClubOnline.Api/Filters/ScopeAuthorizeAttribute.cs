using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DinersClubOnline.Api.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ScopeAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly Scopes _scope;

        public ScopeAuthorizeAttribute(Scopes scope)
        {
            _scope = scope;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (!base.IsAuthorized(actionContext))
                return false;

            var claimsPrincipal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (claimsPrincipal == null)
                return false;

            var scopesInPrincipal = claimsPrincipal.Claims.Where(c => c.Type == "scope").Select(c => c.Value.ToLowerInvariant());

            var scopesInFilter = Enum.GetValues(typeof(Scopes))
                        .Cast<Enum>()
                        .Where(x => _scope.HasFlag(x))
                        .Cast<Scopes>()
                        .Select(x=> x.ToString().ToLowerInvariant());

            return scopesInPrincipal.Intersect(scopesInFilter).Any();
        }
    }
}