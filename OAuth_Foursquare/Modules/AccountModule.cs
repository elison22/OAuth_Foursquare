using Nancy;
using Nancy.Security;
using OAuth_Foursquare.Models;
using OAuth_Foursquare.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Modules
{
    public class AccountModule : NancyModule
    {
        public AccountModule()
        {
            this.RequiresAuthentication();
            Get["/account"] = _ =>
            {
                User currentUser = UserManager.get().getUser(this.Context.CurrentUser.UserName);

                if(currentUser.FS_Token == null || currentUser.FS_Token == "")
                    return View["account", new ViewModel(this.Context, currentUser)];

                return null;
            };
        }
        
    }
}