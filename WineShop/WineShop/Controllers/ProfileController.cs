using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WineShop.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profile
        public ActionResult Profile()
        {
            if (WineShop.Tools.Security.Customer.GetCurrent() != null) { return View(WineShop.Tools.Security.Customer.GetCurrent()); }
            else { return RedirectToAction("Index","Home"); }
        }
        public ActionResult Commands()
        {
            if (WineShop.Tools.Security.Customer.GetCurrent() != null) { return View(WineShop.Tools.Security.Customer.GetCurrent()); }
            else { return RedirectToAction("Index", "Home"); }
        }
        public ActionResult Address()
        {
            return View();
        }
    }
}