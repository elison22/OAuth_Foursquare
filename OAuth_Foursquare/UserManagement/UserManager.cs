using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace OAuth_Foursquare.UserManagement
{
    public class UserManager
    {
        private string persistencePath = "@/Data/users.json";
        private static UserManager instance = null;
        private static List<User> users = null;
        private UserManager()
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
                        FirstName = "Brandt",
                        LastName = "Elison",
                        UserName = "elison22",
                        FS_Token = "TPJFCCV4AT5QMSM0IE1OOHEFKJACJJA2Y4UD32SEW3LJ3XIF"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Katrina",
                        LastName = "Elison",
                        UserName = "kdog0128"
                    },
                    new User
                    {
                        Id = Guid.NewGuid(),
                        FirstName = "Carl",
                        LastName = "Walsh",
                        UserName = "dwalsh"
                    }
                };
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static UserManager get()
        {
            if (instance == null)
                instance = new UserManager();
            return instance;
        }

        private List<User> readUsers()
        {
            throw new NotImplementedException();
        }
        private List<User> readUsers(string path)
        {
            throw new NotImplementedException();
        }
        private void writeUsers(string path)
        {
            throw new NotImplementedException();
        }

        // === Getter type things ===
        public User getUser(Guid id)
        {
            User match = users.FirstOrDefault(x => x.Id == id);
            return match;
        }

        public Guid getUserId(string username)
        {
            Guid match = users.FirstOrDefault(x => x.UserName == username).Id;
            return match;
        }

        public User getUser(string username)
        {
            User match = users.FirstOrDefault(x => x.UserName == username);
            return match;
        }

        public List<User> getUsers()
        {
            return users;
        }

        public List<string> getFullNames()
        {
            return (
                from u in users
                select u.FirstName + " " + u.LastName).ToList();
        }

        // === Setter/Adder type things ===
        public void addUser(User newUser)
        {
            users.Add(newUser);
            //writeUsers(persistencePath);
        }

        public void assignToken(string username, string token)
        {
            User user = getUser(username);
            user.FS_Token = token;
            //writeUsers(persistencePath);
        }

    }
}