using Nancy;
using OAuth_Foursquare.UserManagement;
using OAuth_Foursquare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Modules.OAuth_Foursquare
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ =>
            {
                List<User> users = UserManager.get().getUsers();
                return View["index.html", users];
            };
        }
    }
}