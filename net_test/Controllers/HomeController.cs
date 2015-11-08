using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace net_test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? id)
        {
            return RedirectToAction("HomeSpryCateList", new { id = id.GetValueOrDefault(0) });
        }

        public ActionResult HomeSpryCateList(int? id)
        {
            ViewBag.viewID = id.GetValueOrDefault(0);
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}