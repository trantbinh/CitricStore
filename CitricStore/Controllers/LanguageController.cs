using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CitricStore.Controllers
{
    public class LanguageController : Controller
    {
        // GET: Language
        public ActionResult Index(string language)
        {
            if (!String.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            HttpCookie cookie = new HttpCookie(name: "Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);
            return View();
        }

        public ActionResult Login(string language)
        {
            if (!String.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            HttpCookie cookie = new HttpCookie(name: "Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);
            return View();
        }

        public ActionResult Register(string language)
        {
            if (!String.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            HttpCookie cookie = new HttpCookie(name: "Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);
            return View();
        }

        public ActionResult FogotPass(string language)
        {
            if (!String.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            HttpCookie cookie = new HttpCookie(name: "Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);
            return View();
        }

        public ActionResult DetailsGame(string language)
        {
            if (!String.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            HttpCookie cookie = new HttpCookie(name: "Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);
            return View();
        }

        public ActionResult DetailsApp(string language)
        {
            if (!String.IsNullOrEmpty(language))
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
            }
            HttpCookie cookie = new HttpCookie(name: "Languages");
            cookie.Value = language;
            Response.Cookies.Add(cookie);
            return View();
        }
    }
}