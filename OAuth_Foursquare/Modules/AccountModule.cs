using Nancy;
using Nancy.Security;
using OAuth_Foursquare.Foursquare;
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

                if (currentUser.FS_Token == null || currentUser.FS_Token.Trim() == "")
                    return View["account_basic", new ViewModel(this.Context, currentUser)];

                CheckinsModel checkins = FoursquareAccess.get().getCheckins(currentUser.FS_Token);
                checkins.user = currentUser;
                //checkins.count++;
                //checkins.items.Add(new CheckinModel
                //{
                //    id = "This is the ID",
                //    createdAt = ConvertToUnixTimestamp(DateTime.Now.AddDays(-4).AddHours(11).AddMinutes(20)),
                //    timeZoneOffset = -600,
                //    venue = "Smiths",
                //    type = "Shopping"
                //});

                return View["account_adv", new ViewModel(this.Context, checkins)];
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