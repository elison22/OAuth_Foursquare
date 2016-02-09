using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.UserManagement
{
    public class UserManager
    {

        public List<User> users { get; set; }

        public UserManager()
        {
            try
            {
                users = readUsers();
            }
            catch
            {
                users = new List<User>
                {
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "Brandt",
                        Lastname = "Elison",
                        Username = "elison22"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "Katrina",
                        Lastname = "Elison",
                        Username = "kdog0128"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        Firstname = "Carl",
                        Lastname = "Walsh",
                        Username = "dwalsh"
                    }
                };
            }
        }

        private List<User> readUsers()
        {
            throw new NotImplementedException();
        }
        private List<User> readUsers(string path)
        {
            throw new NotImplementedException();
        }
        private void writeUsers()
        {
            throw new NotImplementedException();
        }

        public User getUser(Guid id)
        {
            
            User match = users.FirstOrDefault(x => x.Id == id);

            return match;
        }
    }
}