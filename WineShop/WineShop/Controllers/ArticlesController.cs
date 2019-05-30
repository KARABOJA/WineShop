using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WineShop.Models;


namespace WineShop.Controllers
{
    public class ArticlesController : Controller
    {
        // GET: Articles
        public ActionResult Articles(string language, string type, string categorie, string subcategorie)
        {
            //TODO : A Refaire1
            List<Article> articlesList = new List<Models.Article>();
            if (subcategorie != null)
            {
                SubCategory subCategory = SubCategories.GetDataByName(subcategorie);
                if (subCategory != null) { articlesList = WineShop.Models.Articles.GetDataByUidCategorySub(subCategory.Uid); }
            }
            else if (categorie != null)
            {
                Category category = Categories.GetDataByName(categorie);
                if (category != null) { articlesList = WineShop.Models.Articles.GetDataByUidCategory(category.Uid); }
            }
            else if (type != null)
            {
                TypeCategory typeCategory = TypeCategories.GetDataByName(type);
                if (typeCategory != null) { articlesList = WineShop.Models.Articles.GetDataByUidCategoryType(typeCategory.Uid); }
            }

            return View(articlesList);
        }

        public ActionResult TmpList()
        {
            return View();
        }

        // GET: Articles
        public ActionResult Article(string language, string id)
        {
            if (id != null)
            {
                Article article = new Article()
                {
                    Uid = Guid.Empty,
                    CodeProduct = id.ToString(),
                    Name = "Monton Cadet",
                    Description = "C'est du bon pif",
                    UnitPrice = 10.32d,
                    CurrencySymbol = "€",
                    Notes = 4d
                };
                return View(WineShop.Models.Articles.GetDataByCode(id));
            }
            else { return RedirectToAction("Articles", "Articles"); }
        }
        public ActionResult TmpSingleArticles()
        {
            return View();
        }     
    }
}