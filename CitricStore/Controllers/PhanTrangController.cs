using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CitricStore.Controllers
{
    public class PhanTrangController : Controller
    {
        [HttpGet]
        // GET: /Link/
        public ActionResult Index(int page = 1,int pageSise = 10)
        {
            return View();
        }
    }
}