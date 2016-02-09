using Nancy.Authentication.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.Security;

namespace OAuth_Foursquare.UserManagement
{
    public class UserMapper : IUserMapper
    {
        UserManager manager = new UserManager();

        public IUserIdentity GetUserFromIdentifier(Guid identifier, NancyContext context)
        {
            return manager.getUser(identifier);
        }
    }
}