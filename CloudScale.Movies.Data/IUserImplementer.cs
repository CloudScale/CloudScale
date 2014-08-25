using System;
using Microsoft.AspNet.Identity;

namespace CloudScale.Movies.Data
{
    public class IUserImplementer : IUser
    {
        public string Id
        {
            get { throw new NotImplementedException(); }
        }

        public string UserName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}