using OAuth_Foursquare.UserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Models
{
    public class CheckinsModel
    {
        public int count { get; set; }
        public User user { get; set; }
        public List<CheckinModel> items { get; set; }

        public User User
        {
            get
            {
                return user;
            }
        }
        public List<CheckinModel> Items
        {
            get
            {
                return (
                    from i in items
                    orderby i.createdAt descending
                    select i).Take(10).ToList<CheckinModel>();
            }
        }
        public CheckinModel MostRecent
        {
            get
            {
                return Items.FirstOrDefault<CheckinModel>();
            }
        }
        public int MaxShown
        {
            get
            {
                return Items.Count;
            }
        }
        public int TotalItems
        {
            get
            {
                return count;
            }
        }
    }
}