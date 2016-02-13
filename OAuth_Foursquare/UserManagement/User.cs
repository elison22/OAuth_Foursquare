using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.UserManagement
{
    public class User : IUserIdentity, IComparable
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

        int IComparable.CompareTo(object obj)
        {
            if (obj == null)
                return -1;
            if (obj.GetType() != typeof(User))
                return -1;
            User uobj = (User)obj;
            if (this.UserName != uobj.UserName ||
                this.FS_Token != uobj.FS_Token)
                return -1;
            else return 0;
        }
    }
}