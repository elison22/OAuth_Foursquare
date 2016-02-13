using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OAuth_Foursquare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace OAuth_Foursquare.Foursquare
{
    public class FoursquareAccess
    {
        private string client_id = "L3VL3K03WOPM1M2AC3Z2OIEU1HQXHNHQ44G31TGIOTAWWELM";
        private string client_secret = "E44YKRB4CYTQQ1VYJ5TOAC5AC4TE0DX3MSG44JY2XMNQ1IRM";

        private static FoursquareAccess instance = null;
        private FoursquareAccess() { }
        public static FoursquareAccess get()
        {
            if (instance == null)
                instance = new FoursquareAccess();
            return instance;
        }

        public string getAuthenticateUrl()
        {
            return "https://foursquare.com/oauth2/authenticate" +
                                "?client_id=" + client_id +
                                "&response_type=code" +
                                "&redirect_uri=https://p3.byubrandt.com/foursquare/code";
        }

        private string getTokenUrl(string code)
        {
            return "https://foursquare.com/oauth2/access_token" +
                                    "?client_id=" + client_id +
                                    "&client_secret=" + client_secret +
                                    "&grant_type=authorization_code" +
                                    "&redirect_uri=https://p3.byubrandt.com/home" +
                                    "&code=" + code;
        }

        private string getCheckinsUrl(string FS_Token)
        {
            return
                "https://api.foursquare.com/v2/users/self/checkins" +
                "?oauth_token=" + FS_Token +
                //"?client_id=" + client_id +
                //"&client_secret=" + client_secret +
                "&v=20160101&m=foursquare";
        }

        private async Task<string> makeAsyncRequest(string url)
        {
            // request the token
            HttpClient client = new HttpClient();
            HttpResponseMessage hrm = await client.GetAsync(url);

            // get the response
            string rawData = await hrm.Content.ReadAsStringAsync();
            Console.WriteLine("\nThe raw response content was:\n" + rawData);

            return rawData;
        }

        private string makeRequest(string url)
        {
            Task<string> responseTask = Task<string>.Run<string>(async () => { return await makeAsyncRequest(url); });
            responseTask.Wait();

            return responseTask.Result;
        }


        public string getAccessToken(string code)
        {
            string url = getTokenUrl(code);
            Console.WriteLine("\nMaking a request to Foursquare using the following URL:\n" + url);

           string token = makeRequest(url);

            // get the token
            JObject jobj = JObject.Parse(token);
            token = (string)jobj["access_token"];
            Console.WriteLine("\nThe final access token is:\n" + token);

            return token;

        }

        public CheckinsModel getCheckins(string FS_Token)
        {
            string url = getCheckinsUrl(FS_Token);
            Console.WriteLine("\nGetting checkings using the following URL:\n" + url);

            string checkinJson = makeRequest(url);
            Console.WriteLine("\nRaw response data for the checkings is as follows:\n" + checkinJson);

            JObject jobj = JObject.Parse(checkinJson);
            checkinJson = (jobj["response"]["checkins"]).ToString();

            CheckinsModel checkins = JsonConvert.DeserializeObject<CheckinsModel>(checkinJson);
            Console.WriteLine("\nObjects successfully converted!");

            return checkins;
        }

    }
}