using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitricStore.Models;
using PagedList;

namespace CitricStore.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        CitricStoreEntities db = new CitricStoreEntities();
        public ActionResult AdminPage()
        {
            return View();
        }
        public List<OrderItem> GetOrder()
        {
            List<OrderItem> myOrder = Session["Order"] as List<OrderItem>;
            if (myOrder == null)
            {
                myOrder = new List<OrderItem>();
                Session["Order"] = myOrder;
            }
            return myOrder;
        }
        public ActionResult CheckCusOrder(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var or = db.ORDER_INFO.OrderBy(s => s.DateOrder).ToList();
            return View(or.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CheckCusOrder_Processing(int? idor)
        {
            if (idor == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER_INFO or = db.ORDER_INFO.Find(idor);
            if (or == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDSttOrder = new SelectList(db.ORDER_STATUS, "IDStt", "NameStt", or.IDSttOrder);
            return View(or);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckCusOrder_Processing([Bind(Include = "IDOrder,DateOrder,IDCus,NameOrder,PhoneOrder,EmailOrder,IDBank,IDPayAccount,NamePayAccount,TotalPrice,IDSttOrder")] ORDER_INFO or)
        {
            if (ModelState.IsValid)
            {
                db.Entry(or).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CheckCusOrder", "Admin");
            }
            return View(or);
        }
        public ActionResult CheckCusOrder_Processing_EachProduct(int idor)
        {
            var or = db.ORDER_PRODUCT.Where(s => s.IDOrder == idor).ToList();
            return PartialView(or);
        }

        public ActionResult CheckCusOrder_EachProduct(int idor)
        {
            var or = db.ORDER_PRODUCT.Where(s => s.IDOrder == idor).ToList();
            return PartialView(or);
        }
        public ActionResult CheckCusOrder_InfoCus(int idor)
        {
            var or = db.ORDER_INFO.FirstOrDefault(s => s.IDOrder == idor);
            return PartialView(or);
        }
        public ActionResult CheckCusOrder_AppInfo(int idud)
        {
            var ud = db.OVERALLs.Where(s => s.IDOverall == idud);
            return PartialView(ud);
        }

        public ActionResult Extension_GetNameStatus(int idtt)
        {
            var name = db.ORDER_STATUS.Where(s => s.IDStt == idtt).ToList();
            return PartialView(name);
        }

        public ActionResult CheckCusInfo()
        {
            var cus = db.CUSTOMERs.ToList();
            return View(cus);
        }
    }
}