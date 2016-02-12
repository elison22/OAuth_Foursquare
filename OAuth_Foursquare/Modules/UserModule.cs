using Nancy;
using OAuth_Foursquare.Models;
using OAuth_Foursquare.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Modules
{
	public class UserModule : NancyModule
	{
        public UserModule()
        {
            Get["/user/{username}"] = _ =>
            {
                User selected = UserManager.get().getUser((string)_.username);
                if (selected == null)
                    return View["error", new ErrorModel
                    {
                        Message = (string)_.username + " is not a valid user.",
                        RedirectPage = "home",
                        RedirectURL = "/home"
                    }];
                if (this.Context.CurrentUser != null &&
                    selected.UserName == this.Context.CurrentUser.UserName)
                    return Response.AsRedirect("/account");
                return View["user", new ViewModel(this.Context, selected)];
            };
        }
	}
}