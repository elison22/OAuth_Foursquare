using Newtonsoft.Json;
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
        private string persistenceDir = "Data/";
        private string persistenceName = "users.json";
        //private string persistenceDir = @"Data\";
        //private string persistenceName = @"users.json";
        private static UserManager instance = null;
        private static List<User> users = null;
        private UserManager()
        {
            try
            {
                users = readUsers(persistenceDir, persistenceName);
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
                Console.WriteLine("\nInstantiating UserManager");
                instance = new UserManager();
            }
            else
            {
                Console.WriteLine("\nUsing the existing UserManager with " + users.Count + " Users");
            }
            return instance;
        }
        
        private List<User> readUsers(string dir, string name)
        {
            string json = UserIO.readUserJson(dir, name);
            Console.WriteLine("\nRead in the following Json:\n" + json);

            List<User> readInUsers = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
            Console.WriteLine("\nConverted it to " + readInUsers.Count + " Users.");

            return readInUsers;
        }
        private void writeUsers(string dir, string name)
        {
            Console.WriteLine("\nPreparing to persist " + users.Count + " users.");
            List<User> persisted = readUsers(dir, name);
            Console.WriteLine("\nFound " + persisted.Count + " users on disk.");
            users = combineLists(persisted, users);
            Console.WriteLine("\nCombined groups into " + users.Count +" users");
            string json = JsonConvert.SerializeObject(users);
            Console.WriteLine("\nSerialized users for writing into the following Json:\n" + json);
            UserIO.writeUserJson(dir, name, json);
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
            writeUsers(persistenceDir, persistenceName);
        }

        public void assignToken(string username, string token)
        {
            User user = getUser(username);
            user.FS_Token = token;
            writeUsers(persistenceDir, persistenceName);
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