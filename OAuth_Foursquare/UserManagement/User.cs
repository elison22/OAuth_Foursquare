using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.UserManagement
{
    public class User : IUserIdentity
    {
        private List<string> UserClaims = new List<string>();
        private string Username { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid Id { get; set; }
        public string FS_Token { get; set; }

        public IEnumerable<string> Claims
        {
            get
            {
                return UserClaims;
            }
        }
        public string UserName
        {
            get { return Username; }
            set { Username = value; }
        }
    }
}