using Nancy;
using OAuth_Foursquare.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare
{
    public class HomeModule : NancyModule
    {

        List<User> tempUsers = new List<User>
        {
            new User
            {
                FirstName = "Brandt",
                LastName = "Elison",
                Username = "elison22"
            },
            new User
            {
                FirstName = "Katrina",
                LastName = "Elison",
                Username = "kdog0128"
            },
            new User
            {
                FirstName = "Carl",
                LastName = "Walsh",
                Username = "darthwalsh"
            }
        };

        public HomeModule() 
        {
            Get["/"] = _ => "Hello Nancy from Pluralsight";
            Get["/user"] = _ => View["users", tempUsers];
            Get["/user/{name}"] = p => View["user", tempUsers.FirstOrDefault(x => MatchingUser(x, p.name))];
        }

        Func<User, string, bool> MatchingUser = (user, name) =>
              user.FirstName == name ||
              user.LastName == name ||
              user.Username == name;
    }
}