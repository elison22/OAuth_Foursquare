﻿using Nancy;
using OAuth_Foursquare.Foursquare;
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
                
                // if it's not a valid user, go back home
                if (selected == null)
                    return View["error", new ErrorModel
                    {
                        Message = (string)_.username + " is not a valid user.",
                        RedirectPage = "home",
                        RedirectURL = "/home"
                    }];

                // if user is logged in, go to the account page
                if (this.Context.CurrentUser != null &&
                    selected.UserName == this.Context.CurrentUser.UserName)
                    return Response.AsRedirect("/account");

                // if the user isn't connected to foursquare, display the basic page
                if (selected.FS_Token == null || selected.FS_Token.Trim() == "")
                    return View["user_basic", new ViewModel(this.Context, selected)];

                // otherwise display the page with a checkin
                CheckinsModel checkins = FoursquareAccess.get().getCheckins(selected.FS_Token);

                checkins.user = selected;
                //checkins.items.Add(new CheckinModel
                //{
                //    id = "This is the ID",
                //    createdAt = ConvertToUnixTimestamp(DateTime.Now.AddDays(-4).AddHours(11).AddMinutes(20)),
                //    timeZoneOffset = -600,
                //    venue = "Smiths",
                //    type = "Shopping"
                //});

                return View["user_adv", new ViewModel(this.Context, checkins)];
            };
        }
        
        private long ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return (long)Math.Floor(diff.TotalSeconds);
        }

    }
}