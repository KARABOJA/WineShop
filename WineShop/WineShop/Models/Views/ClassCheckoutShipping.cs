using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WineShop.Models.Views
{
    public class ModelCheckoutShipping
    {
        [Required]
        public byte ShippingMode { get; set; }
    }
}