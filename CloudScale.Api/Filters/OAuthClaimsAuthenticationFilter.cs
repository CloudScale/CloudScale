using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using StatsdClient;

namespace CloudScale.Api.Filters
{
    public class OAuthClaimsAuthenticationFilter : IAutofacAuthenticationFilter
    {
        private readonly IStatsd statsd;

        public OAuthClaimsAuthenticationFilter(IStatsd statsd)
        {
            this.statsd = statsd;
        }

        public void OnAuthenticate(HttpAuthenticationContext context)
        {
            if (statsd != null && (context.Principal == null || !context.Principal.Identity.IsAuthenticated))
                statsd.LogCount("action." + context.ActionContext.ActionDescriptor.ActionName.ToLower() + ".failedauth");

            if (context.Principal != null
                && context.Principal.Identity.IsAuthenticated
                && context.Principal is ClaimsPrincipal)
            {
                var principal = (ClaimsPrincipal) context.Principal;
                Claim sidClaim = principal.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Sid);
                if (sidClaim != null)
                {
                    Guid sid;
                    if (Guid.TryParse(sidClaim.Value, out sid))
                    {
                        context.ActionContext.Request.Properties["UserId"] = sid;
                    }
                }
            }
        }

        public void OnChallenge(HttpAuthenticationChallengeContext context)
        {
        }
    }
}