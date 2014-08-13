using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;

namespace CloudScale.Movies.Data
{
    public class IUserImplementer : IUser
    {
        public IUserImplementer()
        {

        }
        public string Id
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public string UserName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
