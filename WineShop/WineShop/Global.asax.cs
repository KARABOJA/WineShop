using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WineShop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_EndRequest()
        {
            if (Context.Response.StatusCode == 404)
            {
                Response.Clear();
                RouteData routeData = new RouteData();
                routeData.Values["controller"] = "Errors";
                routeData.Values["action"] = "ErrorHTTP";
                routeData.Values["language"] = "fr-FR";
                routeData.Values["id"] = "404";

                //Response.RedirectToRoute(routeData);
                Response.Redirect($"/fr-FR/Errors/ErrorHTTP/{Context.Response.StatusCode}", true);
            }
            else
            {
                if (Context.Request.RequestContext.RouteData.Values.ContainsKey("language")
                    && (Context.Request.RequestContext.RouteData.Values["language"].ToString().ToLower() != "fr-fr"))
                {
                    Response.Clear();
                    Response.Redirect($"/fr-FR/Errors/ErrorHTTP/404", true);
                }
            }
        }
    }
}
