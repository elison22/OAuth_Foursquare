using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OAuth_Foursquare.Models
{
    public class ErrorModel
    {
        public string Message { get; set; }
        public string RedirectPage { get; set; }
        public string RedirectURL { get; set; }
    }
}