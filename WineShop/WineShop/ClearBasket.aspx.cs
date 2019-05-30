using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WineShop
{
    public partial class ClearBasket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                WineShop.Models.Baskets.ClearData();
                Response.Write("True");
            }
            catch
            {
                Response.Write("False");
            }
        }
    }
}