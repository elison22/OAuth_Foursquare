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
                    return View["/error", new ViewModel(this.Context, new ErrorModel
                    {
                        Message = "A user with this username already exists.",
                        RedirectPage = "Create User",
                        RedirectURL = "/create"
                    })];

                //string FS_token = getToken(createParams.FS_Username, createParams.FS_Password);

                //if (FS_token == null)
                //    return View["/error", new ErrorModel
                //    {
                //        Message = "These Foursquare credentials are invalid.",
                //        RedirectPage = "Create User",
                //        RedirectURL = "/create"
                //    }];

                User newUser = buildUser(createParams);
                UserManager.get().addUser(newUser);

                return this.LoginAndRedirect(newUser.Id, fallbackRedirectUrl:"/account");
            };
        }

        private User buildUser(CreateParams userInfo)
        {
            User newUser = new User
            {
                FirstName = userInfo.Firstname,
                LastName = userInfo.Lastname,
                UserName = userInfo.Username,
                Id = Guid.NewGuid()
            };
            return newUser;
        }

        private string getToken(string FS_login, string FS_password)
        {
            string FS_token = null;
            try
            {
                FS_token = FoursquareAccess.get().getFSToken(FS_login, FS_password);
            }
            catch {}

            return FS_token;
        }

        private class CreateParams
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Username { get; set; }
        }

    }

}