using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CitricStore.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult HomePage()
        {
            return View();
        }
        public ActionResult AppPage()
        {
            return View();
        }
        public ActionResult Test()
        {
            return View();
        }
        public ActionResult Classify()
        {
            return View();
        }
        public ActionResult Question()
        {
            return View();
        }
    }
}