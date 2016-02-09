using Nancy;
using Nancy.ModelBinding;
using OAuth_Foursquare.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Modules
{
    public class LoginModule : NancyModule
    {
        public LoginModule()
        {
            Get["/login"] = _ => View["login"];
            Post["/login"] = _ =>
            {
                var loginParams = this.Bind<LoginParams>();
                var user = UserManager.get().getUser(loginParams.Username);
                if (user == null)
                    return "Fail...";
                else return "Success!";
            };
        }

        public class LoginParams
        {
            public string Username { get; set; }
        }

    }

}