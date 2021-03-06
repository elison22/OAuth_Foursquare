﻿using Nancy;
using OAuth_Foursquare.Models;
using OAuth_Foursquare.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Modules
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => Response.AsRedirect("/home");
            Get["/home"] = _ =>
            {
                Console.WriteLine("\nWelcome to the home page!");
                List<User> users = UserManager.get().getUsers();
                return View["home", new ViewModel(this.Context, users)];
            };
        }

        
        
    }
}