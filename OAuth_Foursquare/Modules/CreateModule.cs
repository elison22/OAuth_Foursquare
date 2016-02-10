using Nancy;
using Nancy.ModelBinding;
using OAuth_Foursquare.Models;
using OAuth_Foursquare.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Modules
{
    public class CreateModule : NancyModule
    {
        public CreateModule()
        {
            Get["/create"] = _ => View["create"];
            Post["/create"] = _ =>
            {
                CreateParams loginParams = this.Bind<CreateParams>();
                if (UserManager.get().getUser(loginParams.Username) != null)
                    return View["/error", new ErrorModel
                    {
                        Message = "A user with this username already exists.",
                        RedirectPage = "Create User",
                        RedirectURL = "/create"
                    }];
                UserManager.get().addUser(buildUser(loginParams));

                return null;    // === Implement this!
            };
        }

        private User buildUser(CreateParams userInfo)
        {
            // figure out the token
            String token = "";


            User newUser = new User
            {
                FirstName = userInfo.Firstname,
                LastName = userInfo.Lastname,
                UserName = userInfo.Username,
                Id = Guid.NewGuid(),
                FS_Token = token
            };
            return newUser;
        }

        private class CreateParams
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Username { get; set; }
            public string FS_Username { get; set; }
            public string FS_Password { get; set; }
        }

    }

}