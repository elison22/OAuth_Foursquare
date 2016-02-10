using Nancy;
using Nancy.ModelBinding;
using Nancy.Authentication.Forms;
using OAuth_Foursquare.Models;
using OAuth_Foursquare.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth_Foursquare.Foursquare;

namespace OAuth_Foursquare.Modules
{
    public class CreateModule : NancyModule
    {
        public CreateModule()
        {
            Get["/create"] = _ => View["create"];
            Post["/create"] = _ =>
            {
                CreateParams createParams = this.Bind<CreateParams>();
                if (UserManager.get().getUser(createParams.Username) != null)
                    return View["/error", new ErrorModel
                    {
                        Message = "A user with this username already exists.",
                        RedirectPage = "Create User",
                        RedirectURL = "/create"
                    }];

                string FS_token = FoursquareAccess.get().getFSToken(createParams.FS_Username, createParams.FS_Password);

                if (FS_token == null)
                    return View["/error", new ErrorModel
                    {
                        Message = "These Foursquare credentials are invalid.",
                        RedirectPage = "Create User",
                        RedirectURL = "/create"
                    }];

                User newUser = buildUser(createParams, FS_token);
                UserManager.get().addUser(newUser);

                return this.LoginAndRedirect(newUser.Id, fallbackRedirectUrl:"/account");
            };
        }

        private User buildUser(CreateParams userInfo, string FS_token)
        {
            User newUser = new User
            {
                FirstName = userInfo.Firstname,
                LastName = userInfo.Lastname,
                UserName = userInfo.Username,
                Id = Guid.NewGuid(),
                FS_Token = FS_token
            };
            return newUser;
        }

        private string getToken(string FS_login, string FS_password)
        {
            throw new NotImplementedException();
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