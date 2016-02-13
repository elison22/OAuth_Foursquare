using Nancy;
using Nancy.Security;
using OAuth_Foursquare.Foursquare;
using OAuth_Foursquare.UserManagement;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth_Foursquare.Models;

namespace OAuth_Foursquare.Modules
{
    public class FoursquareModule : NancyModule
    {

        public FoursquareModule()
        {
            this.RequiresAuthentication();
            this.RequiresHttps();

            Get["/foursquare"] = _ => View["foursquare", new ViewModel(this.Context, null)];
            Get["/foursquare/connect"] = _ =>
            {

                string url = FoursquareAccess.get().getAuthenticateUrl();

                Console.WriteLine("\nFirst connection to Foursquare using this URL:\n" + url);

                return this.Response.AsRedirect(url);
            };

            Get["/foursquare/code"] = _ =>
            {
                string code = (string)this.Request.Query["code"];

                string token = FoursquareAccess.get().getAccessToken(code);

                // add it to the user
                string currentUsername = this.Context.CurrentUser.UserName;
                UserManager.get().assignToken(currentUsername, token);
                
                return this.Response.AsRedirect("/account");
            };
        }

    }
}