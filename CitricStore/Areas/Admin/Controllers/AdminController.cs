using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CitricStore.Models;

namespace CitricStore.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        CitricStoreEntities db = new CitricStoreEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Order_KiemTraDonHang()
        {
            var or = db.ORDER_INFO.OrderBy(s => s.NgayOrder).ToList();
            return View(or);
        }
        public ActionResult Order_DonHang(int idor)
        {
            var or = db.ORDER_PRODUCT.Where(s => s.MaOrder == idor).ToList();
            return PartialView(or);
        }
        public ActionResult Order_InfoKhachHang(int idkh)
        {
            var kh = db.KHACHHANGs.Where(s => s.MaKH == idkh).ToList();
            return PartialView(kh);
        }
    }
}