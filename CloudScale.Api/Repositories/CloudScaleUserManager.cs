using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CloudScale.Api.Repositories
{
    public class CloudScaleUserManager : UserManager<IdentityUser>
    {
        public CloudScaleUserManager(CloudScaleUserStore store)
            : base(store)
        {
        }
    }
}