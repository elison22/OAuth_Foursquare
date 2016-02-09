﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OAuth_Foursquare.Models;

namespace OAuth_Foursquare.UserManagement
{
    public class UserManager
    {
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
                        UserName = "elison22"
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
        private void writeUsers()
        {
            throw new NotImplementedException();
        }

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

    }
}