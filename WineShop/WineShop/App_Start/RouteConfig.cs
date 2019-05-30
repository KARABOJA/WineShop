using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Article",
                url: "{language}/Article/{id}",
                defaults: new { language = "fr-FR", controller = "Articles", action = "Article", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Articles",
                url: "{language}/Articles/{type}/{categorie}/{subcategorie}",
                defaults: new { language = "fr-FR", controller = "Articles", action = "Articles", type = UrlParameter.Optional, categorie = UrlParameter.Optional, subcategorie = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "{language}/{controller}/{action}/{id}",
                defaults: new { language = "fr-FR", controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
