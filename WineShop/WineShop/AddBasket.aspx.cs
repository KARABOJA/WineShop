using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WineShop
{
    public partial class AddBasket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.Form.AllKeys.Contains("UidArticle"))
                {
                    try
                    {
                        Guid uid = Guid.Parse(Request.Form["UidArticle"]);
                        Models.Article article = Models.Articles.GetDataByUid(uid);
                        WineShop.Models.Baskets.SetData(article.Uid, article.CodeProduct, article.Name, article.Description, article.UnitPrice, article.UidColor, article.UidSize, article.Size, article.Color);
                        Response.Write("True");
                    }
                    catch { Response.Write("False"); }
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