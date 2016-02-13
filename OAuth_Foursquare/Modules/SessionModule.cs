using Nancy;
using Nancy.ModelBinding;
using Nancy.Authentication.Forms;
using OAuth_Foursquare.UserManagement;
using OAuth_Foursquare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Modules
{
    public class SessionModule : NancyModule
    {
        public SessionModule()
        {
            Get["/login"] = _ => View["login", new ViewModel(this.Context, null)];
            Post["/login"] = _ =>
            {
                LoginParams loginParams = this.Bind<LoginParams>();
                User user = UserManager.get().getUser(loginParams.Username);
                if (user == null)
                    return View["error", new ViewModel(this.Context, new ErrorModel
                    {
                        Message = loginParams.Username + " is not a valid user.",
                        RedirectPage = "login",
                        RedirectURL = "/login"
                    })];
                return this.LoginAndRedirect(user.Id, fallbackRedirectUrl: "/account");
            };

            Get["/logout"] = _ =>
            {
                return this.LogoutAndRedirect("/home");
            };
        }

        private class LoginParams
        {
            public string Username { get; set; }
        }

    }

}