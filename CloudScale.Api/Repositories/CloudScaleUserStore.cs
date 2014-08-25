using CloudScale.Movies.Data;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CloudScale.Api.Repositories
{
    public class CloudScaleUserStore : UserStore<IdentityUser>
    {
        public CloudScaleUserStore(MoviesDataContext context)
            : base(context)
        {
        }
    }
}