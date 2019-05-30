using System;
using System.Collections.Generic;
using System.Drawing;
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

        public string InvoiceAddress { get; set; }
        public string InvoiceZipCode { get; set; }
        public string InvoiceCity { get; set; }
        public string InvoiceCountry { get; set; }

        public string DeliveryAddress { get; set; }
        public string DeliveryZipCode { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryCountry { get; set; }

        public byte[] PhotoBytes { get; set; }
        public string PhotoContentType { get; set; }
}
}


   