using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Models
{
    public class CheckinModel
    {
        public string id { get; set; }
        public string type { get; set; }
        public int timeZoneOffset { get; set; }
        public long createdAt { get; set; }
        public string venue { get; set; }

        public string CreatedTime
        {
            get
            {
                DateTime checkinTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                checkinTime = checkinTime.AddSeconds(createdAt);
                checkinTime = checkinTime.AddMinutes(timeZoneOffset);
                return checkinTime.ToString("G");
            }
        }
        public string Venue
        {
            get
            {
                return venue;
            }
        }
        public string Type
        {
            get
            {
                return type;
            }
        }

    }
}