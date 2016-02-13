﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace OAuth_Foursquare.UserManagement
{
    public class UserManager
    {
        private string persistencePath = @"/Data/users.json";
        private static UserManager instance = null;
        private static List<User> users = null;
        private UserManager()
        {
            try
            {
                users = readUsers(persistencePath);
            }
            catch(NotImplementedException)
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
            {
                instance = new UserManager();
            }
            return instance;
        }
        
        private List<User> readUsers(string path)
        {
            string json = UserIO.readUserJson(path);

            List<User> readInUsers;
            try
            {
                readInUsers = JsonConvert.DeserializeObject<List<User>>(json);
            }
            catch
            {
                readInUsers = new List<User>();
            }

            return readInUsers;
        }
        private void writeUsers(string path)
        {
            List<User> persisted = readUsers(path);
            users = combineLists(persisted, users);

            string json = JsonConvert.SerializeObject(users);
            UserIO.writeUserJson(path, json);
        }

        // === Getter type things ===
        public User getUser(Guid id)
        {
            User match = users.FirstOrDefault(x => x.Id == id);
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

        // === Setter/Adder type things ===
        public void addUser(User newUser)
        {
            users.Add(newUser);
            writeUsers(persistencePath);
        }

        public void assignToken(string username, string token)
        {
            User user = getUser(username);
            user.FS_Token = token;
            writeUsers(persistencePath);
        }

        private List<User> combineLists(List<User> fromFile, List<User> inMemory)
        {
            foreach(User memUser in inMemory)
            {
                if (fromFile.Contains(memUser))
                    continue;
                User usernameMatchInFile = (
                    from fileUser in fromFile
                    where fileUser.UserName == memUser.UserName
                    select fileUser).FirstOrDefault();
                if (usernameMatchInFile == null)
                    fromFile.Add(memUser);
                else if (memUser.FS_Token != null && memUser.FS_Token != "")
                    usernameMatchInFile.FS_Token = memUser.FS_Token;
            }
            return fromFile;
        }

    }
}