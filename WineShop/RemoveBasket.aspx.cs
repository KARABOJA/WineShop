using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WineShop
{
    public partial class RemoveBasket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Form.AllKeys.Contains("UidArticle"))
                {
                    Guid uid = Guid.Parse(Request.Form["UidArticle"]);
                    WineShop.Models.Baskets.DelData(uid);
                    Response.Write("True");
                }
                else { Response.Write("False"); }
            }
            catch
            {
                Response.Write("False");
            }
        }
    }
}