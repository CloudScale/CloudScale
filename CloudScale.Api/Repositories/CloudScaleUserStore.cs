using CloudScale.Api.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using CloudScale.Movies.Data;
using System.Data.Entity;

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
