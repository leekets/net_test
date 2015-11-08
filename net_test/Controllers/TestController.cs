using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace net_test.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index(int? id)
        {
            return View();
        }

        public ActionResult TestList()
        {
            return View();
        }
    }
}