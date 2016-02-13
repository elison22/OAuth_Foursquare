using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.UserManagement
{
	public class UserIO
	{
        private static string root = AppDomain.CurrentDomain.BaseDirectory;
        public static string readUserJson(string dir, string name)
        {
            string data = "";

            if (!Directory.Exists(combine(dir)))
                Directory.CreateDirectory(combine(dir));
            if (!File.Exists(combine(dir)))
                File.Create(combine(dir, name));
            else
                data = File.ReadAllText(combine(dir, name));
            return data;
        }

        public static void writeUserJson(string dir, string name, string json)
        {
            File.WriteAllText(combine(dir, name), json);
        }

        private static string combine(params string[] parts)
        {
            string full = root;
            foreach(string part in parts)
            {
                full += part;
            }
            Console.WriteLine("Examing: " + full);
            return full;
        }
    }
}