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
        public ActionResult Index()
        {
            return View();
        }


        public List<OrderItem> GetOrder()
        {
            List<OrderItem> myOrder = Session["Order"] as List<OrderItem>;

            //Nếu giỏ hàng chưa tồn tại thì tạo mới và đưa vào Session
            if (myOrder == null)
            {
                myOrder = new List<OrderItem>();
                Session["Order"] = myOrder;
            }
            return myOrder;
        }


        public ActionResult Order_KiemTraDonHang(int? page)
        {
            if (page == null) page = 1;
            int pageSize = 3;
            int pageNumber = (page ?? 1);



            var or = db.ORDER_INFO.OrderBy(s => s.NgayOrder).ToList();
            return View(or.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Order_XuLyDonHang(int? idor)
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
            ViewBag.TrangThaiXuLy = new SelectList(db.TRANGTHAIDONHANGs, "Ma", "Ten", or.TrangThaiXuLy);

            return View(or);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Order_XuLyDonHang([Bind(Include = "MaOrder,NgayOrder,MaKH,TenOrder,SDTOrder,EmailOrder,MaNganHang,MaTaiKhoan,TenTaiKhoan,TongTien,TrangThaiXuLy")] ORDER_INFO or)
        {
            if (ModelState.IsValid)
            {
                db.Entry(or).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Order_KiemTraDonHang", "Admin");
            }
            return View(or);
        }

        public ActionResult Order_TungDonHang(int? id)
        {
            var or = db.ORDER_INFO.ToList();
            return PartialView(or);

        }

        //[HttpPost, ActionName("Order_TungDonHang")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed()
        //{

        //    List<OrderItem> myOrder = GetOrder();

        //    foreach (var or in myOrder)
        //    {
        //        var ar = new ARCHIVE_ORDER_INFO();
        //        ar.MaOrder = or.MaOr;
        //        ar.NgayOrder = (DateTime)or.NgayOr;
        //        ar.MaKH = or.MaKhachHang;
        //        ar.TenOrder = or.TenOr;
        //        ar.SDTOrder = or.SDTOr;
        //        ar.EmailOrder = or.EmailOr;
        //        ar.MaNganHang = or.BankID;
        //        ar.MaTaiKhoan = or.MaTK;
        //        ar.TenTaiKhoan = or.TenTK;
        //        ar.TongTien = or.TongTien;
        //        db.ARCHIVE_ORDER_INFO.Add(ar);

        //        ORDER_INFO aPP = db.ORDER_INFO.Find(or.MaOr);
        //        db.ORDER_INFO.Remove(aPP);
        //        db.SaveChanges();

        //    }
        //    return RedirectToAction("Order_KiemTraDonHang");
        //}

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
        public ActionResult Order_InfoUngDung(int idud)
        {
            var ud = db.OVERALLs.Where(s => s.Ma == idud);
            return PartialView(ud);
        }

        public ActionResult Extension_Order_TenTrangThai(int idtt)
        {
            var name = db.TRANGTHAIDONHANGs.Where(s => s.Ma == idtt).ToList();
            return PartialView(name);
        }

        public ActionResult Customer_ThongTinKhachHang()
        {
            var cus = db.KHACHHANGs.ToList();
            return View(cus);
        }
    }
}