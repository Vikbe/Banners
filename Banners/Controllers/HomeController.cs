using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Banners.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        } 

        public ActionResult ViewHtml()
        {
            return View();
        }

        public ActionResult UploadHtml()
        {
            return View();
        }
    }
}
