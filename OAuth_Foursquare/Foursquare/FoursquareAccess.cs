using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Foursquare
{
    public class FoursquareAccess
    {
        private static FoursquareAccess instance = null;
        private FoursquareAccess() { }
        public static FoursquareAccess get()
        {
            if (instance == null)
                instance = new FoursquareAccess();
            return instance;
        }

        public string getFSToken(string fS_Username, string fS_Password)
        {
            throw new NotImplementedException();
        }
    }
}