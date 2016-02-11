using Nancy;
using Nancy.Security;
using OAuth_Foursquare.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace OAuth_Foursquare.Modules
{
    public class FoursquareModule : NancyModule
    {
        private static string client_id = "L3VL3K03WOPM1M2AC3Z2OIEU1HQXHNHQ44G31TGIOTAWWELM";
        private static string client_secret = "E44YKRB4CYTQQ1VYJ5TOAC5AC4TE0DX3MSG44JY2XMNQ1IRM";

        public FoursquareModule()
        {
            this.RequiresAuthentication();

            Get["/foursquare"] = _ => this.Response.AsRedirect("/foursquare/connect"); ;
            Get["/foursquare/connect"] = _ =>
            {


                string url = "https://foursquare.com/oauth2/authenticate" +
                                "?client_id=" + client_id +
                                "&response_type=code" +
                                "&redirect_uri=p3.byubrandt.com/foursquare/code";

                return this.Response.AsRedirect(url);
            };

            Get["/foursquare/code", true] = async (_, ct) =>
            {
                string code = (string)this.Request.Query["code"];

                string getTokenUrl = "https://foursquare.com/oauth2/access_token" +
                                    "?client_id=" + client_id +
                                    "&client_secret=" + client_secret +
                                    "&grant_type=authorization_code" +
                                    "&redirect_uri=p3.byubrandt.com/home" +
                                    "&code=" + code;

                HttpClient client = new HttpClient();
                HttpResponseMessage hrm = await client.GetAsync(getTokenUrl);

                string access_token = hrm.Content.ToString();
                string tokenKey = "access_token:";

                int tokenStart = access_token.IndexOf(tokenKey) + tokenKey.Length;
                int tokenEnd = access_token.IndexOfAny(new char[] { ',', '}' }, tokenStart);

                access_token = access_token.Substring(tokenStart, tokenEnd - tokenStart);
                access_token = access_token.Trim();

                string currentUsername = this.Context.CurrentUser.UserName;
                UserManager.get().assignToken(currentUsername, access_token);

                return this.Response.AsRedirect("/account");
            };
        }

    }
}