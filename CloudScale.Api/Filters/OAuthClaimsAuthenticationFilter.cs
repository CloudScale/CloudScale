using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;

namespace CloudScale.Api.Filters
{
    public class OAuthClaimsAuthenticationFilter : IAutofacAuthenticationFilter
    {

        public void OnAuthenticate(HttpAuthenticationContext context)
        {
            if (context.Principal != null
                && context.Principal.Identity.IsAuthenticated
                && context.Principal is ClaimsPrincipal)
            {
                ClaimsPrincipal principal = (ClaimsPrincipal)context.Principal;
                Claim sidClaim = principal.Claims.FirstOrDefault(p => p.Type == ClaimTypes.Sid);
                if (sidClaim != null)
                {
                    Guid sid;
                    if (System.Guid.TryParse(sidClaim.Value, out sid))
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