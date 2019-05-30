using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WineShop.Models
{
    public class Customer
    {
        public Guid Uid { get; set; }
        public string NameFirst { get; set; }
        public string NameLast { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool Enabled { get; set; }
    }
}


   