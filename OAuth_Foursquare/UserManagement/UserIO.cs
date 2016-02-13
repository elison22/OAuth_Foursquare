using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.UserManagement
{
	public class UserIO
	{
        public static string readUserJson(string path)
        {
            string data = "";
            if (!File.Exists(path))
                File.Create(path);
            else
                data = File.ReadAllText(path);
            return data;
        }

        public static void writeUserJson(string path, string json)
        {
            File.WriteAllText(path, json);
        }
	}
}