using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare
{
    public class AccountModule : NancyModule
    {
        public AccountModule()
        {
            this.RequiresAuthentication();
            Get["/account"] = _ =>
            {
                return "I'm securely logged in as: " + this.Context.CurrentUser.UserName;
            };
        }
    }
}