using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WineShop.Controllers
{
    public class ErrorsController : Controller
    {
        // GET: Error
        public ActionResult ErrorHTTP()
        {
            return View();
        }
    }
}