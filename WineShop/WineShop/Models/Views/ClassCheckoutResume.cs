using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WineShop.Models.Views
{
    public class ModelCheckoutResume
    {
        public string HtlmsControls { get; set; }

        public CamatSoft.PaymentTools.Paybox.Module module { get; set; }
    }
}