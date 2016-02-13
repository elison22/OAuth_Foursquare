using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.UserManagement
{
	public class UserIO
	{
        public static string readUserJson(string dir, string name)
        {
            string data = "";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            if (!File.Exists(dir + "/" + name))
                File.Create(dir + "/" + name);
            else
                data = File.ReadAllText(dir + "/" + name);
            return data;
        }

        public static void writeUserJson(string dir, string name, string json)
        {
            File.WriteAllText(dir + "/" + name, json);
        }
	}
}